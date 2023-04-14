using AutoMapper;
using Domain;
using Domain.Repos.Model;

namespace Controller.Utillities.AutoMapperProfiles
{
    public class ChoreProfile : Profile
    {
        public ChoreProfile()
        {
            CreateMap<ControlMessage, Setting>().ReverseMap();
            CreateMap<ControlMessage, ControlMessage>()
                .ForAllMembers(opts => opts.Condition((_, _, src) => src is not null));
        }
    }
}