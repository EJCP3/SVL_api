using SVL.Core.Application.DTOs.Dashboard;

namespace SVL.Core.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> ObtenerResumenAsync();
}
