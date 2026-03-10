using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.DTOs.Licencias;
using SVL.Core.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SVL.WebApi.Controllers
{
   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LicenciasController : ControllerBase
    {
        private readonly ILicenciaService _licenciaService;

        public LicenciasController(ILicenciaService licenciaService) => _licenciaService = licenciaService;

        [HttpPost("solicitar")]
        public async Task<IActionResult> Solicitar([FromBody] CrearLicenciaDto dto)
        {
            var result = await _licenciaService.RegistrarAsync(dto);
            return Ok(result);
        }

        [Authorize(Roles = "RRHH")]
        [HttpPost("aprobar/{id}")]
        public async Task<IActionResult> Aprobar(Guid id)
        {
            var adminName = User.Identity?.Name ?? "Admin";
            var result = await _licenciaService.AprobarAsync(id, adminName);
            return Ok(result);
        }

        [HttpPost("{id}/subir-soporte")]
        public async Task<IActionResult> SubirSoporte(Guid id, IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0) return BadRequest("No se seleccionó un archivo.");

            // 1. Generar nombre único y ruta
            var extension = Path.GetExtension(archivo.FileName).ToLower();
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/licencias");

            if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            // 2. Guardar archivo físico
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // 3. Pasar la URL al servicio para guardarla en PostgreSQL
            var urlRelativa = $"/uploads/licencias/{nombreArchivo}";
            var resultado = await _licenciaService.CargarSoporteMedicoAsync(id, urlRelativa);

            return resultado ? Ok(new { url = urlRelativa }) : BadRequest("Error al actualizar la base de datos.");
        }
}
}