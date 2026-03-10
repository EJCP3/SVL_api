using FluentEmail.Core; // Necesitas instalar el paquete FluentEmail.Core
using SVL.Core.Application.Interfaces;
using System.Threading.Tasks;

namespace SVL.Core.Application.Services;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task EnviarCodigoVerificacionAsync(string correoDestino, string codigo)
    {
        var response = await _fluentEmail
         .SetFrom("euddy.ejcp@gmail.com", "Sistema SVL")
         .To(correoDestino)
         .Subject("Código de Verificación - SVL")
         .Body($"Su código de seguridad es: {codigo}")
         .SendAsync();

        if (!response.Successful)
        {
            // Pasa el mouse sobre 'response.ErrorMessages' para ver por qué falló
            var errorDeGoogle = string.Join(", ", response.ErrorMessages);
            throw new Exception(errorDeGoogle); // Esto hará que Swagger te muestre el error real
        }
    }

    public async Task EnviarCorreoAsync(string correoDestino, string asunto, string cuerpo)
    {
        await _fluentEmail
            .To(correoDestino)
            .Subject(asunto)
            .Body(cuerpo)
            .SendAsync();
    }
}