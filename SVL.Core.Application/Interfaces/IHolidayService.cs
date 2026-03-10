using SVL.Core.Application.DTOs.Feriados;

namespace SVL.Core.Application.Interfaces;

public interface IHolidayService
{
    Task<FeriadoDto> RegistrarAsync(CrearFeriadoDto dto);
    Task<IEnumerable<FeriadoDto>> ListarPorAnioAsync(int anio);
    Task<FeriadoDto?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<FeriadoDto>> ListarTodosAsync();
    Task<IEnumerable<FeriadoDto>> ListarProximosAsync(int dias = 30);
    Task<bool> ActualizarAsync(Guid id, CrearFeriadoDto dto);
    Task<bool> EliminarAsync(Guid id);
}
