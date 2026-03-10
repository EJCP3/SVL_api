namespace SVL.Core.Application.DTOs.Feriados;

public class CrearFeriadoDto
{
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public bool EsRecurrente { get; set; }
}
