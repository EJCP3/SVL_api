using SVL.Core.Application.DTOs.Licencias;

namespace SVL.Core.Application.Interfaces;

public interface ILicenciaService
{
    Task<LicenciaDto> RegistrarAsync(CrearLicenciaDto dto);
    Task<IEnumerable<LicenciaDto>> ListarPorEmpleadoAsync(Guid empleadoId);
    Task<LicenciaDto?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<LicenciaDto>> ListarTodosAsync();
    Task<bool> AprobarAsync(Guid id, string aprobadoPor);
    Task<bool> RechazarAsync(Guid id, string rechazadoPor);
    Task<bool> CargarSoporteMedicoAsync(Guid id, string rutaArchivo);
}
