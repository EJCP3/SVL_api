using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SVL.Core.Application.DTOs.Auth;
using SVL.Core.Application.Interfaces;
using SVL.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SVL.Core.Application.Services;

public class AuthService : IAuthService
{
    private readonly IGenericRepository<Employee> _employeeRepo;
    private readonly IConfiguration _config;

    public AuthService(IGenericRepository<Employee> employeeRepo, IConfiguration config)
    {
        _employeeRepo = employeeRepo;
        _config = config;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto login)
    {
        // 1. Buscar al empleado por correo
        var empleados = await _employeeRepo.GetAllAsync();
        var empleado = empleados.FirstOrDefault(e => e.CorreoElectronico == login.Correo);

        // 2. Validar (Aquí deberías usar Hash para el password, esto es un ejemplo)
        if (empleado == null || empleado.Password != login.Password)
        {
            return new AuthResponseDto { TokenValido = false, Mensaje = "Credenciales incorrectas" };
        }

        // 3. Generar el Token JWT
        var token = GenerarJwtToken(empleado);

        return new AuthResponseDto
        {
            TokenValido = true,
            Token = token,
            Mensaje = "Login exitoso"
        };
    }

    private string GenerarJwtToken(Employee empleado)

    {

        string puestoLower = empleado.Puesto.ToLower();

        string rolAsignado = "Empleado"; // Rol por defecto

        if (puestoLower.Contains("rrhh") || puestoLower.Contains("recursos humanos") || puestoLower.Contains("personal"))
        {
            rolAsignado = "RRHH";
        }


        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, empleado.Id.ToString()),
            new Claim(ClaimTypes.Email, empleado.CorreoElectronico),
            new Claim(ClaimTypes.Role, rolAsignado) 
        };

        var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key no configurada");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:DurationInMinutes"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}