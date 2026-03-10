namespace SVL.Core.Application.DTOs.Vacaciones;

public class DiasDisponiblesDto
{
    public Guid EmpleadoId { get; set; }
    public string NombreEmpleado { get; set; } = string.Empty;
    public int DiasCorrespondientes { get; set; }
    public int DiasUsados { get; set; }
    public int DiasDisponibles { get; set; }
    public int DiasPendientesAprobacion { get; set; }
    public int DiasRestantes { get; set; }
}
