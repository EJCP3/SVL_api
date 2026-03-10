using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.Interfaces;
using SVL.Core.Application.Services;
using System;
using System.Threading.Tasks;

namespace SVL.WebApi.Controllers
{
    [Authorize(Roles = "RRHH")]
    [ApiController]
    [Route("api/[controller]")]
    public class VacacionesController : ControllerBase
    {
        // Nota: En tu explorador de soluciones el archivo se llama IVacationsService (con S)
        private readonly IVacationService _vacationsService;

        public VacacionesController(IVacationService vacationsService)
        {
            _vacationsService = vacationsService;
        }

        [HttpGet("pendientes")]
        public async Task<IActionResult> GetPendientes()
        {
            var result = await _vacationsService.ListarTodasPendientesAsync();
            return Ok(result);
        }

        [HttpPost("aprobar/{id}")]
        public async Task<IActionResult> Aprobar(Guid id)
        {
            var result = await _vacationsService.AprobarAsync(id);
            return Ok(result);
        }
    }
}