using System;
using System.Collections.Generic;
using System.Text;

namespace SVL.Core.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool TokenValido { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }
}
