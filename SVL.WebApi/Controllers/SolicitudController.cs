using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.DTOs.Vacaciones;
using SVL.Core.Application.DTOs.Licencias; // Asegúrate de tener este DTO
using SVL.Core.Application.Interfaces;
using System.Security.Claims;

namespace SVL.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly IVacationService _vacationService;
    private readonly ILicenciaService _licenciaService;

    public SolicitudesController(IVacationService vacationService, ILicenciaService licenciaService)
    {
        _vacationService = vacationService;
        _licenciaService = licenciaService;
    }

    // --- SECCIÓN DE VACACIONES ---
    [HttpPost("vacaciones")]
    public async Task<IActionResult> SolicitarVacaciones([FromBody] CrearVacacionesDto dto)
    {
        return Ok(await _vacationService.RegistrarAsync(dto));
    }

    // --- SECCIÓN DE LICENCIAS (Lo que faltaba) ---
    [HttpPost("licencia")]
    public async Task<IActionResult> SolicitarLicencia([FromForm] CrearLicenciaDto dto)
    {
        // Aquí el usuario promedio sube su solicitud de permiso o licencia
        return Ok(await _licenciaService.RegistrarAsync(dto));
    }

    // --- SECCIÓN DE CONSULTAS ---
    [HttpGet("mis-solicitudes-recientes")]
    public async Task<IActionResult> GetHistorial()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        // Puedes devolver un objeto que contenga ambas listas
        var historial = new
        {
            Vacaciones = await _vacationService.ListarPorEmpleadoAsync(Guid.Parse(userId)),
            Licencias = await _licenciaService.ListarPorEmpleadoAsync(Guid.Parse(userId))
        };

        return Ok(historial);
    }
}