using System;
using System.Collections.Generic;
using System.Text;

namespace SVL.Core.Application.Interfaces // Cámbialo a la carpeta de Interfaces
{
    public interface IEmailService
    {
        // Envía el código de 6 dígitos al Gmail del empleado
        Task EnviarCodigoVerificacionAsync(string correoDestino, string codigo);

        // Envía una notificación general (ej. a RRHH cuando llega una solicitud)
        Task EnviarCorreoAsync(string correoDestino, string asunto, string cuerpo);
    }
}