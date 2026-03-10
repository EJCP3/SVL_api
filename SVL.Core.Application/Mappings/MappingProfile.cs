using AutoMapper;
using SVL.Core.Application.DTOs.Feriados;
using SVL.Core.Application.DTOs.Licencias;
using SVL.Core.Application.DTOs.Vacaciones;
using SVL.Core.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SVL.Core.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CrearVacacionesDto, SolicitudVacaciones>();
        CreateMap<SolicitudVacaciones, VacacionesDto>()
              // 1. Mapear el nombre del empleado (asumiendo que traes la entidad cargada)
              .ForMember(dest => dest.NombreEmpleado,
                         opt => opt.MapFrom(src => $"{src.Empleado.Nombre} {src.Empleado.Apellido}"))

              // 2. Formato de fecha solicitado: "14 dic 2024 15:30"
              .ForMember(dest => dest.FechaSolicitudFormateada,
                         opt => opt.MapFrom(src => src.FechaCreacion.ToString("dd MMM yyyy HH:mm")))

              // 3. Convertir el Enum a texto legible
              .ForMember(dest => dest.EstadoTexto,
                         opt => opt.MapFrom(src => src.EstadoSolicitud.ToString()));

        // Licencias (Leave)
        CreateMap<Licencia, LicenciaDto>().ReverseMap();
        CreateMap<CrearLicenciaDto, Licencia>();

        // Feriados (Holiday)
        CreateMap<Holiday, FeriadoDto>().ReverseMap();
        CreateMap<CrearFeriadoDto, Holiday>();
    }
}