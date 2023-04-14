using AutoMapper;
using Domain.Repos.Dtos;
using Domain.Repos.Model;

namespace Controller.Utillities.AutoMapperProfiles
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            #region device entities

            CreateMap<Device, DeviceReadDto>();
            CreateMap<DeviceCreateDto, Device>();
            CreateMap<DeviceUpdateDto, Device>()
                .ForAllMembers(opts => opts.Condition(src => src is not null));

            #endregion device entities

            #region operationLog entities

            CreateMap<OperationLog, OperationLogReadDto>();
            CreateMap<OperationLogCreateDto, OperationLog>();
            CreateMap<OperationLogUpdateDto, OperationLog>()
                .ForAllMembers(opts => opts.Condition(src => src is not null));

            #endregion operationLog entities
        }
    }
}