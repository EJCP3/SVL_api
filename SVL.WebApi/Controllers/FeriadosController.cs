using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.DTOs.Feriados;
using SVL.Core.Application.Interfaces;

namespace SVL.WebApi.Controllers;

[Authorize] // Todos pueden ver los feriados
[ApiController]
[Route("api/[controller]")]
public class HolidaysController : ControllerBase
{
    private readonly IHolidayService _holidayService;

    public HolidaysController(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    // Obtener todos los feriados (para el calendario del frontend)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var feriados = await _holidayService.ListarTodosAsync();
        return Ok(feriados);
    }

    // Obtener feriados por año específico
    [HttpGet("anio/{anio}")]
    public async Task<IActionResult> GetByYear(int anio)
    {
        var feriados = await _holidayService.ListarPorAnioAsync(anio);
        return Ok(feriados);
    }

    // Solo RRHH puede agregar nuevos feriados (ej. feriados movibles)
    [Authorize(Roles = "RRHH")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CrearFeriadoDto dto)
    {
        var result = await _holidayService.RegistrarAsync(dto);
        return Ok(result);
    }

    [Authorize(Roles = "RRHH")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _holidayService.EliminarAsync(id);
        return NoContent();
    }
}