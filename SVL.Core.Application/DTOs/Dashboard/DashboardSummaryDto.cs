namespace SVL.Core.Application.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public int TotalEmpleados { get; set; }
    public int SolicitudesVacacionesPendientes { get; set; }
    public int SolicitudesLicenciasPendientes { get; set; }
    public int VacacionesAprobadasMes { get; set; }
    public int LicenciasAprobadasMes { get; set; }
    public int FeriadosProximoMes { get; set; }
    public int EmpleadosDeVacacionesHoy { get; set; }
    public int EmpleadosConLicenciaHoy { get; set; }
}
