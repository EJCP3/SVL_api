using Microsoft.Extensions.DependencyInjection;
using SVL.Core.Application.Interfaces;
using SVL.Core.Application.Mappings;
using SVL.Core.Application.Services;
using System.Reflection;

namespace SVL.Core.Application;

public static class ServiceRegistration
{
    public static void AddApplicationRegistration(this IServiceCollection services)
    {
        // Registro automático de AutoMapper usando el perfil que ya tienes
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Registro de tus servicios de negocio
        services.AddScoped<IVacationService, VacationService>();
        services.AddScoped<ILicenciaService, LicenciaService>();
        services.AddScoped<IHolidayService, HolidayService>();
        services.AddScoped<IDashboardService, DashboardService>();

        services.AddScoped<IAuthService, AuthService>();
    }
}