using AutoMapper;
using LandisGyr.ConsoleApp.Models;

namespace LandisGyr.ConsoleApp.Features
{
    public class EndpointProfile : Profile
    {
        public EndpointProfile()
        {
            CreateMap<CreateEndpoint, Endpoint>()
               .ForMember(d => d.SerialNumber, opt => opt.MapFrom(s => s.SerialNumber))
               .ForMember(d => d.SwitchState, opt => opt.MapFrom(s => s.SwitchState))
               .ForMember(d => d.MeterNumber, opt => opt.MapFrom(s => s.MeterNumber))
               .ForMember(d => d.MeterModel, opt => opt.MapFrom(s => s.MeterModel))
               .ForMember(d => d.MeterFirmwareVersion, opt => opt.MapFrom(s => s.MeterFirmwareVersion));

            CreateMap<UpdateEndpoint, Endpoint>()
                .ForMember(d => d.SerialNumber, opt => opt.MapFrom(s => s.SerialNumber))
                .ForMember(d => d.SwitchState, opt => opt.MapFrom(s => s.SwitchState))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Endpoint, Endpoint>()
                .ForMember(d => d.SwitchState, opt => opt.MapFrom(s => s.SwitchState))
                .ForAllOtherMembers(opt => opt.UseDestinationValue());
        }
    }
}
