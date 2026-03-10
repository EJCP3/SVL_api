using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.Interfaces;

namespace SVL.WebApi.Controllers;

[Authorize(Roles = "Admin")] // Solo RRHH/Admin puede ver esto
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen()
    {
        var result = await _dashboardService.ObtenerResumenAsync();
        return Ok(result);
    }
}