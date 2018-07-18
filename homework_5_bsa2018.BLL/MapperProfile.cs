using System.Linq;
using AutoMapper;
using homework_5_bsa2018.Shared.DTOs;
using homework_5_bsa2018.DAL.Models;
using homework_5_bsa2018.Shared.LoadDTO;

namespace homework_5_bsa2018.BLL
{
    public class MapperProfile:Profile
    {
       public MapperProfile()
        {
            CreateMap<Crew, CrewDTO>()
                .ForMember(crew => crew.PilotId, crew => crew.MapFrom(x => x.Pilot.Id))
                .ForMember(crew => crew.StewardressIds, crew => crew.MapFrom(x => x.Stewardesses.Select(s => s.Id)));

            CreateMap<Pilot, PilotDTO>();
            CreateMap<PilotDTO, Pilot>()
                .ForMember(item=> item.Id, item => item.Ignore());

            CreateMap<Stewardess, StewardessDTO>();
            CreateMap<StewardessDTO, Stewardess>()
                .ForMember(item => item.Id, item => item.Ignore());

            CreateMap<PlaneType, PlaneTypeDTO>();
            CreateMap<PlaneTypeDTO, PlaneType>()
                .ForMember(item => item.Id, item => item.Ignore());

            CreateMap<Plane, PlaneDTO>()
                .ForMember(plane => plane.TypePlaneId, plane => plane.MapFrom(x => x.TypePlane.Id))
                .ForMember(plane => plane.Created, plane => plane.MapFrom(x => x.Created.ToLongDateString()))
                .ForMember(plane => plane.LifeTime, plane => plane.MapFrom(x => x.LifeTime.ToString()));


            CreateMap<Ticket, TicketDTO>();
            CreateMap<TicketDTO, Ticket>()
                .ForMember(item => item.Id, item => item.Ignore());


            CreateMap<Flight, FlightDTO>()
                .ForMember(flight => flight.StartTime, flight => flight.MapFrom(x => x.StartTime))
                .ForMember(flight => flight.FinishTime, flight => flight.MapFrom(x => x.FinishTime))
                .ForMember(flight => flight.TicketIds, flight => flight.MapFrom(x => x.Tickets.Select(t => t.Id)));

            CreateMap<Departure, DepartureDTO>()
               .ForMember(departure => departure.PlaneId, flight => flight.MapFrom(x => x.Plane.Id))
               .ForMember(departure => departure.CrewId, flight => flight.MapFrom(x => x.Crew.Id))
               .ForMember(departure => departure.DepartureTime, flight => flight.MapFrom(x => x.DepartureTime.ToString()));

            CreateMap<LoadCrewDTO, Crew>()
                .ForMember(x => x.Pilot, opt => opt.MapFrom(i => i.pilot.FirstOrDefault()))
                .ForMember(x => x.Stewardesses, opt => opt.MapFrom(i => i.stewardess));
        }
    }
}
