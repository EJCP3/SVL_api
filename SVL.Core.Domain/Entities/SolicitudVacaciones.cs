using SVL.Core.Domain.Common;
using SVL.Core.Domain.Enums;

namespace SVL.Core.Domain.Entities;

public class SolicitudVacaciones : BaseEntity
{
    public Guid EmpleadoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int TotalDias { get; set; }
    public string? Comentarios { get; set; }
    public RequestStatus EstadoSolicitud { get; set; } = RequestStatus.Pendiente;
    public DateTime? FechaAprobacion { get; set; }
    public string? AprobadoPor { get; set; }

    public DateTime FechaCreacion { get; set; }


    // Propiedad de navegación
    public virtual Employee Empleado { get; set; } = null!;
}
