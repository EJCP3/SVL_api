using SVL.Core.Domain.Enums;

namespace SVL.Core.Application.DTOs.Vacaciones;

public class VacacionesDto
{
    public Guid Id { get; set; }
    public Guid EmpleadoId { get; set; }
    public string NombreEmpleado { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int TotalDias { get; set; }
    public string? Comentarios { get; set; }

    // Mantenemos el Enum por si acaso
    public RequestStatus EstadoSolicitud { get; set; }

    // NUEVO: Para mostrar "Pendiente", "Aprobado", etc.
    public string EstadoTexto { get; set; } = string.Empty;

    public DateTime? FechaAprobacion { get; set; }
    public string? AprobadoPor { get; set; }

    public DateTime FechaCreacion { get; set; }

    // NUEVO: La fecha con el formato que pediste (14 dic 2024 y hora)
    public string FechaSolicitudFormateada { get; set; } = string.Empty;
}