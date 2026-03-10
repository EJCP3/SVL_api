namespace SVL.Core.Application.DTOs.Licencias;

public class CrearLicenciaDto
{
    public Guid EmpleadoId { get; set; }
    public string TipoLicencia { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public string? Motivo { get; set; }
    public bool RequiereSoporteMedico { get; set; }
    public string? DocumentoSoporteMedico { get; set; }
}
