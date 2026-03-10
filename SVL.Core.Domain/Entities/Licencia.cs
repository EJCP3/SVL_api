using SVL.Core.Domain.Common;
using SVL.Core.Domain.Enums;

namespace SVL.Core.Domain.Entities;

public class Licencia : BaseEntity
{
    public Guid EmpleadoId { get; set; }
    public string TipoLicencia { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int TotalDias { get; set; }
    public string? Motivo { get; set; }
    public bool RequiereSoporteMedico { get; set; }
    public string? UrlSoporte { get; set; } 
                                           
    public RequestStatus EstadoSolicitud { get; set; } = RequestStatus.Pendiente;
    public DateTime? FechaAprobacion { get; set; }
    public string? AprobadoPor { get; set; }

    // Propiedad de navegación
    public virtual Employee Empleado { get; set; } = null!;
}
