using SVL.Core.Domain.Common;

namespace SVL.Core.Domain.Entities;

public class Employee : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string CodigoEmpleado { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }
    public string Departamento { get; set; } = string.Empty;
    public string Puesto { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    // Propiedades de navegación
    public virtual ICollection<SolicitudVacaciones> SolicitudesVacaciones { get; set; } = [];
    public virtual ICollection<Licencia> Permisos { get; set; } = [];
}
