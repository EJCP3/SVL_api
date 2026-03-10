using Microsoft.EntityFrameworkCore;
using SVL.Core.Domain.Entities;

namespace SVL.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<SolicitudVacaciones> SolicitudVacaciones { get; set; }
    public DbSet<Licencia> Licencias { get; set; }
    public DbSet<Holiday> Holidays { get; set; }
    public DbSet<CodigoVerificacion> CodigoVerificacion { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Mapeo para la tabla existente
        modelBuilder.Entity<Employee>(entity =>
        {
            // Si la tabla está en el esquema 'public' de la base de datos 'Cafeteria_tech'
            entity.ToTable("Employees", "public");

            entity.HasKey(e => e.Id);

            // Ignorar las propiedades que no existen en la tabla
            entity.Ignore(e => e.CreatedDate);
            entity.Ignore(e => e.ModifiedDate);
            entity.Ignore(e => e.Status);
            // Esto es lo que evita que EF intente "crear" la tabla Employees
            // Solo creará las tablas de Vacaciones, Licencias, etc.
            entity.Metadata.SetIsTableExcludedFromMigrations(true);
        });



        modelBuilder.Entity<SolicitudVacaciones>().ToTable("SolicitudVacaciones", "rrhh");
        modelBuilder.Entity<Licencia>().ToTable("Licencias", "rrhh");
        modelBuilder.Entity<Holiday>().ToTable("Holidays", "rrhh");
        modelBuilder.Entity<CodigoVerificacion>().ToTable("CodigosVerificacion", "rrhh");
        // La de empleados se queda donde está

        // 2. Relación: SolicitudVacaciones
        modelBuilder.Entity<SolicitudVacaciones>()
            .HasOne(s => s.Empleado)
            .WithMany(e => e.SolicitudesVacaciones)
            .HasForeignKey(s => s.EmpleadoId);

        // 3. Relación: Licencia
        modelBuilder.Entity<Licencia>()
            .HasOne(l => l.Empleado)
            .WithMany(e => e.Permisos)
            .HasForeignKey(l => l.EmpleadoId);
    }
}