using System;
using System.Collections.Generic;
using System.Text;

namespace SVL.Core.Domain.Entities
{
    public class CodigoVerificacion
    {
        public Guid Id { get; set; }

        // Correo del empleado que solicita la verificación
        public string Correo { get; set; } = string.Empty;

        // El código de 6 dígitos generado
        public string Codigo { get; set; } = string.Empty;

        // Fecha en la que se generó el código
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Fecha de expiración (ej. +15 minutos)
        public DateTime FechaExpiracion { get; set; }

        // Indica si el código ya fue validado para una solicitud
        public bool EstaUsado { get; set; } = false;
    }
}
