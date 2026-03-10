using SVL.Core.Domain.Common;

namespace SVL.Core.Domain.Entities;

public class Holiday : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public bool EsRecurrente { get; set; }
    public int Anio { get; set; }
}
