using FluentEmail.MailKitSmtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVL.Core.Application.Interfaces;
using SVL.Core.Application.Services;
using SVL.Infrastructure.Persistence.Contexts;
using SVL.Infrastructure.Persistence.Repositories;

namespace SVL.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuramos PostgreSQL aquí
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Connection"),
            m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IEmailService, EmailService>();
        services.AddFluentEmail(configuration["EmailSettings:User"])
        .AddMailKitSender(new SmtpClientOptions
        {
            Server = configuration["EmailSettings:SmtpServer"],
            Port = int.Parse(configuration["EmailSettings:Port"]),
            User = configuration["EmailSettings:User"],
            Password = configuration["EmailSettings:Password"],
            RequiresAuthentication = true,
            UseSsl = false // Para puerto 587
        });
    }
}