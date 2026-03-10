using SVL.Core.Application.DTOs.Dashboard;
using SVL.Core.Application.Interfaces;
using SVL.Core.Domain.Entities;
using SVL.Core.Domain.Enums;

namespace SVL.Core.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IGenericRepository<SolicitudVacaciones> _vacationRepo;
    private readonly IGenericRepository<Licencia> _leaveRepo;
    private readonly IGenericRepository<Employee> _employeeRepo;
    private readonly IGenericRepository<Holiday> _holidayRepo;

    public DashboardService(
        IGenericRepository<SolicitudVacaciones> vacationRepo,
        IGenericRepository<Licencia> leaveRepo,
        IGenericRepository<Employee> employeeRepo,
        IGenericRepository<Holiday> holidayRepo)
    {
        _vacationRepo = vacationRepo;
        _leaveRepo = leaveRepo;
        _employeeRepo = employeeRepo;
        _holidayRepo = holidayRepo;
    }

    public async Task<DashboardSummaryDto> ObtenerResumenAsync()
    {
        var hoy = DateTime.Today;
        var primerDiaMes = new DateTime(hoy.Year, hoy.Month, 1);
        var proximoMes = hoy.AddMonths(1);

        var vacaciones = await _vacationRepo.GetAllAsync();
        var licencias = await _leaveRepo.GetAllAsync();
        var empleados = await _employeeRepo.GetAllAsync();
        var feriados = await _holidayRepo.GetAllAsync();

        return new DashboardSummaryDto
        {
            TotalEmpleados = empleados.Count(),

            // Verifica si en SolicitudVacaciones la propiedad se llama 'Status' o 'EstadoSolicitud'
            SolicitudesVacacionesPendientes = vacaciones.Count(v => v.EstadoSolicitud == RequestStatus.Pendiente),
            SolicitudesLicenciasPendientes = licencias.Count(l => l.EstadoSolicitud == RequestStatus.Pendiente),

            // Cambié 'CreatedDate' por 'FechaCreacion' o similar según tu BaseEntity
            VacacionesAprobadasMes = vacaciones.Count(v => v.EstadoSolicitud == RequestStatus.Aprobado && v.CreatedDate >= primerDiaMes),
            LicenciasAprobadasMes = licencias.Count(l => l.EstadoSolicitud == RequestStatus.Aprobado && l.CreatedDate >= primerDiaMes),

            // Error CS1061: En Holiday, la propiedad probablemente se llama 'Fecha' no 'Date'
            FeriadosProximoMes = feriados.Count(f => f.Fecha.Month == proximoMes.Month),

            // Error CS1061: En SolicitudVacaciones, usa 'FechaInicio' y 'FechaFin'
            EmpleadosDeVacacionesHoy = vacaciones.Count(v => v.EstadoSolicitud == RequestStatus.Aprobado && hoy >= v.FechaInicio && hoy <= v.FechaFin),

            // Para Licencia ya confirmamos que usas FechaInicio y FechaFin
            EmpleadosConLicenciaHoy = licencias.Count(l => l.EstadoSolicitud == RequestStatus.Aprobado && hoy >= l.FechaInicio && hoy <= l.FechaFin)
        };
    }
}