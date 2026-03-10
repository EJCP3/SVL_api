using AutoMapper;
using SVL.Core.Application.Interfaces;
using SVL.Core.Application.DTOs.Vacaciones;
using SVL.Core.Domain.Entities;
using SVL.Core.Domain.Enums;

namespace SVL.Core.Application.Services;

public class VacationService : IVacationService
{
    // Cambiado a SolicitudVacaciones para que coincida con el constructor
    private readonly IGenericRepository<SolicitudVacaciones> _repository;
    private readonly IGenericRepository<Holiday> _holidayRepository;
    private readonly IMapper _mapper;

    public VacationService(IGenericRepository<SolicitudVacaciones> repository,
                           IGenericRepository<Holiday> holidayRepository,
                           IMapper mapper)
    {
        _repository = repository;
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    // Nota: He cambiado la firma para que coincida con la interfaz que pusiste abajo
    public async Task<int> CalcularDiasHabilesAsync(DateTime start, DateTime end)
    {
        var holidays = await _holidayRepository.GetAllAsync();
        int businessDays = 0;

        for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
        {
            bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            bool isHoliday = holidays.Any(h => h.Fecha.Date == date.Date);

            if (!isWeekend && !isHoliday)
            {
                businessDays++;
            }
        }
        return businessDays;
    }

    public async Task<VacacionesDto> RegistrarAsync(CrearVacacionesDto dto)
    {
        var entity = _mapper.Map<SolicitudVacaciones>(dto);

        // Corregido: Usar FechaInicio y FechaFin del DTO
        // Asumiendo que TotalDias es la propiedad en tu entidad SolicitudVacaciones
        entity.TotalDias = await CalcularDiasHabilesAsync(dto.FechaInicio, dto.FechaFin);

        await _repository.AddAsync(entity);
        return _mapper.Map<VacacionesDto>(entity);
    }

    public async Task<IEnumerable<VacacionesDto>> ListarPorEmpleadoAsync(Guid empleadoId)
    {
        var todas = await _repository.GetAllAsync();
        var filtradas = todas.Where(x => x.EmpleadoId == empleadoId)
                             .OrderByDescending(x => x.FechaCreacion);
        return _mapper.Map<IEnumerable<VacacionesDto>>(filtradas);
    }

    // El método que le faltaba a tu controlador de RRHH
    public async Task<IEnumerable<VacacionesDto>> ListarTodasPendientesAsync()
    {
        var todas = await _repository.GetAllAsync();
        // Usamos el Enum RequestStatus.Pending (asegúrate que coincida con tu Enums/RequestStatus.cs)
        var pendientes = todas.Where(x => x.EstadoSolicitud == RequestStatus.Pendiente);
        return _mapper.Map<IEnumerable<VacacionesDto>>(pendientes);
    }

    public async Task<bool> AprobarAsync(Guid id, string aprobadoPor)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.EstadoSolicitud = RequestStatus.Aprobado;
        entity.AprobadoPor = aprobadoPor;
        entity.FechaAprobacion = DateTime.Now;

        await _repository.UpdateAsync(entity);
        return true;
    }

    // Rechazar la solicitud
    public async Task<bool> RechazarAsync(Guid id, string rechazadoPor)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.EstadoSolicitud = RequestStatus.Rechazado;
        entity.AprobadoPor = rechazadoPor; // Guardamos quién lo rechazó en el mismo campo o uno de auditoría

        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<IEnumerable<VacacionesDto>> ListarTodosAsync()
    {
        var lista = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<VacacionesDto>>(lista);
    }

    public async Task<VacacionesDto?> ObtenerPorIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<VacacionesDto>(entity);
    }

    // Este método requiere lógica de negocio según las reglas de la DGA
    public async Task<DiasDisponiblesDto> CalcularDiasDisponiblesAsync(Guid empleadoId)
    {
        // 1. Obtener todas las vacaciones aprobadas de este empleado
        var vacacionesAprobadas = await _repository.GetAllAsync();
        var totalTomados = vacacionesAprobadas
            .Where(v => v.EmpleadoId == empleadoId && v.EstadoSolicitud == RequestStatus.Aprobado)
            .Sum(v => v.TotalDias);

        // 2. Definir el tope legal (Esto podría venir de la tabla Employee según su antigüedad)
        int topeLegal = 15;

        // 3. Calcular la diferencia
        int restantes = topeLegal - totalTomados;

        return new DiasDisponiblesDto
        {
            EmpleadoId = empleadoId,
            DiasRestantes = restantes < 0 ? 0 : restantes
        };
    }
}