namespace SVL.Core.Application.DTOs.Vacaciones;

public class CrearVacacionesDto
{
    public Guid EmpleadoId { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string? Comentarios { get; set; }
}
