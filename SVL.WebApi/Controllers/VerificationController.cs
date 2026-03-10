using Microsoft.AspNetCore.Mvc;
using SVL.Core.Application.DTOs.Vacaciones;
using SVL.Core.Application.Interfaces;
using SVL.Core.Domain.Entities;

namespace SVL.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerificationController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IVacationService _vacationService;
    private readonly IGenericRepository<CodigoVerificacion> _codigoRepo;

    public VerificationController(
        IEmailService emailService,
        IVacationService vacationService,
        IGenericRepository<CodigoVerificacion> codigoRepo)
    {
        _emailService = emailService;
        _vacationService = vacationService;
        _codigoRepo = codigoRepo;
    }

    [HttpPost("enviar-codigo")]
    public async Task<IActionResult> Enviar([FromBody] string correo)
    {
        var codigo = new Random().Next(100000, 999999).ToString();

        // Guardar en la tabla que creamos en Domain
        var verificacion = new CodigoVerificacion
        {
            Correo = correo,
            Codigo = codigo,
            FechaExpiracion = DateTime.Now.AddMinutes(15)
        };
        await _codigoRepo.AddAsync(verificacion);

        // Enviar por Gmail usando el servicio que configuramos
        await _emailService.EnviarCodigoVerificacionAsync(correo, codigo);

        return Ok(new { mensaje = "Código enviado a tu Gmail" });
    }

    [HttpPost("validar-y-registrar")]
    public async Task<IActionResult> Validar(string correo, string codigo, [FromBody] CrearVacacionesDto solicitud)
    {
        var codigos = await _codigoRepo.GetAllAsync();
        var esValido = codigos.FirstOrDefault(x =>
            x.Correo == correo &&
            x.Codigo == codigo &&
            x.FechaExpiracion > DateTime.Now &&
            !x.EstaUsado);

        if (esValido == null) return BadRequest("Código incorrecto o expirado");

        esValido.EstaUsado = true;
        await _codigoRepo.UpdateAsync(esValido);

        // Si el código es real, guardamos la solicitud definitiva
        var resultado = await _vacationService.RegistrarAsync(solicitud);
        return Ok(resultado);
    }
}