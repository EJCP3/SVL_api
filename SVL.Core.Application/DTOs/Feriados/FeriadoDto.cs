namespace SVL.Core.Application.DTOs.Feriados;

public class FeriadoDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public bool EsRecurrente { get; set; }
    public int Anio { get; set; }
    public bool Estado { get; set; }
}
