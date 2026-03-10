using SVL.Core.Domain.Enums;

namespace SVL.Core.Application.DTOs.Licencias;

public class LicenciaDto
{
    public Guid Id { get; set; }
    public Guid EmpleadoId { get; set; }
    public string NombreEmpleado { get; set; } = string.Empty;
    public string TipoLicencia { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int TotalDias { get; set; }
    public string? Motivo { get; set; }
    public bool RequiereSoporteMedico { get; set; }
    public string? DocumentoSoporteMedico { get; set; }
    public DateTime? FechaCargaSoporteMedico { get; set; }
    public RequestStatus EstadoSolicitud { get; set; }
    public DateTime? FechaAprobacion { get; set; }
    public string? AprobadoPor { get; set; }
    public DateTime FechaCreacion { get; set; }
}
