using Domain.Repos.Dtos;

namespace Domain.Repos.IRepositories
{
    public interface IDeviceRepo : IBaseRepo<DeviceReadDto, DeviceUpdateDto, DeviceCreateDto>
    {
        public Task<DeviceReadDto?> FindViaUri(string uri);

        public Task<DeviceReadDto?> FindViaLimsi(string limsi);

        public Task<bool> IsExist(DeviceCreateDto device);

        public Task<DeviceReadDto?> Update(DeviceUpdateDto dto);
    }
}