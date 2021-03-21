using AutoMapper;
using LandisGyr.ConsoleApp.Models;

namespace LandisGyr.ConsoleApp.Features
{
    public class EndpointProfile : Profile
    {
        public EndpointProfile()
        {
            CreateMap<CreateEndpoint, Endpoint>()
               .ForMember(e => e.SerialNumber, opt => opt.MapFrom(s => s.SerialNumber))
               .ForMember(e => e.SwitchState, opt => opt.MapFrom(s => s.SwitchState))
               .ForMember(e => e.MeterNumber, opt => opt.MapFrom(s => s.MeterNumber))
               .ForMember(e => e.MeterModel, opt => opt.MapFrom(s => s.MeterModel))
               .ForMember(e => e.MeterFirmwareVersion, opt => opt.MapFrom(s => s.MeterFirmwareVersion));

            // Aqui indicamos para o AutoMapper criar, por inferência, o mapeamento entre o UpdateEndpoint e o Endpoint
            CreateMap<UpdateEndpoint, Endpoint>()
                .ForMember(e => e.SerialNumber, opt => opt.MapFrom(s => s.SerialNumber))
                .ForMember(e => e.SwitchState, opt => opt.MapFrom(s => s.SwitchState))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Endpoint, Endpoint>();
        }
    }
}
