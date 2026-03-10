using AutoMapper;
using SVL.Core.Application.DTOs.Licencias;
using SVL.Core.Application.Interfaces;
using SVL.Core.Domain.Entities;
using SVL.Core.Domain.Enums;

namespace SVL.Core.Application.Services;

public class LicenciaService : ILicenciaService
{
    private readonly IGenericRepository<Licencia> _repository;
    private readonly IMapper _mapper;

    public LicenciaService(IGenericRepository<Licencia> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<LicenciaDto> RegistrarAsync(CrearLicenciaDto dto)
    {
        var entity = _mapper.Map<Licencia>(dto);
        entity.EstadoSolicitud = RequestStatus.Pendiente;
        await _repository.AddAsync(entity);
        return _mapper.Map<LicenciaDto>(entity);
    }

    public async Task<LicenciaDto?> ObtenerPorIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<LicenciaDto>(entity);
    }

    public async Task<IEnumerable<LicenciaDto>> ListarTodosAsync()
    {
        var lista = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<LicenciaDto>>(lista);
    }

    public async Task<IEnumerable<LicenciaDto>> ListarPorEmpleadoAsync(Guid empleadoId)
    {
        var lista = await _repository.GetAllAsync();
        var filtrados = lista.Where(l => l.EmpleadoId == empleadoId);
        return _mapper.Map<IEnumerable<LicenciaDto>>(filtrados);
    }

    // Estos métodos son obligatorios según tu interfaz image_094654.png
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

    public async Task<bool> RechazarAsync(Guid id, string rechazadoPor)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.EstadoSolicitud = RequestStatus.Rechazado;
        await _repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> CargarSoporteMedicoAsync(Guid id, string rutaArchivo)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        // Guardamos la ruta que el controlador ya generó
        entity.UrlSoporte = rutaArchivo;

        await _repository.UpdateAsync(entity);
        return true;
    }
}