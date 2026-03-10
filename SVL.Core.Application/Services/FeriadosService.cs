using AutoMapper;
using SVL.Core.Application.DTOs.Feriados;
using SVL.Core.Application.Interfaces;
using SVL.Core.Domain.Entities;

namespace SVL.Core.Application.Services;

public class HolidayService : IHolidayService
{
    private readonly IGenericRepository<Holiday> _repository;
    private readonly IMapper _mapper;

    public HolidayService(IGenericRepository<Holiday> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<FeriadoDto> RegistrarAsync(CrearFeriadoDto dto)
    {
        var entity = _mapper.Map<Holiday>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<FeriadoDto>(entity);
    }

    public async Task<IEnumerable<FeriadoDto>> ListarPorAnioAsync(int anio)
    {
        var feriados = await _repository.GetAllAsync();
        var filtrados = feriados.Where(h => h.Fecha.Year == anio);
        return _mapper.Map<IEnumerable<FeriadoDto>>(filtrados);
    }

    public async Task<bool> EliminarAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;
        await _repository.DeleteAsync(entity);
        return true;
    }

    public async Task<IEnumerable<FeriadoDto>> ListarProximosAsync(int dias = 30)
    {
        var hoy = DateTime.Now.Date;
        var limite = hoy.AddDays(dias);

        var feriados = await _repository.GetAllAsync();
        var proximos = feriados.Where(h => h.Fecha.Date >= hoy && h.Fecha.Date <= limite)
                               .OrderBy(h => h.Fecha);

        return _mapper.Map<IEnumerable<FeriadoDto>>(proximos);
    }

    public async Task<bool> ActualizarAsync(Guid id, CrearFeriadoDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        _mapper.Map(dto, entity); // Actualiza los campos de la entidad con el DTO
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<FeriadoDto?> ObtenerPorIdAsync(Guid id) => _mapper.Map<FeriadoDto>(await _repository.GetByIdAsync(id));
    public async Task<IEnumerable<FeriadoDto>> ListarTodosAsync() => _mapper.Map<IEnumerable<FeriadoDto>>(await _repository.GetAllAsync());
    
}