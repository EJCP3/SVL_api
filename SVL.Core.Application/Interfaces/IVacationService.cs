using SVL.Core.Application.DTOs.Vacaciones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SVL.Core.Application.Interfaces;

public interface IVacationService
{
    Task<VacacionesDto> RegistrarAsync(CrearVacacionesDto dto);
    Task<IEnumerable<VacacionesDto>> ListarPorEmpleadoAsync(Guid empleadoId);

    // AGREGA ESTE: Para que el controlador de RRHH pueda ver los pendientes
    Task<IEnumerable<VacacionesDto>> ListarTodasPendientesAsync();

    Task<DiasDisponiblesDto> CalcularDiasDisponiblesAsync(Guid empleadoId);
    Task<VacacionesDto?> ObtenerPorIdAsync(Guid id);
    Task<IEnumerable<VacacionesDto>> ListarTodosAsync();

    // AJUSTA ESTOS: Pon el segundo parámetro como opcional (= null) para que no dé error
    Task<bool> AprobarAsync(Guid id, string? aprobadoPor = null);
    Task<bool> RechazarAsync(Guid id, string? rechazadoPor = null);
}