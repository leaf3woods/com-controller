using Domain.Repos.Dtos;

namespace Domain.Repos.IRepositories
{
    public interface IDeviceRepo : IBaseRepo<DeviceReadDto,DeviceUpdateDto,DeviceCreateDto>
    {
        public Task<DeviceReadDto?> GetViaUri(string uri);

        public Task<DeviceReadDto?> GetViaLimsi(string limsi);

        public Task<bool> IsExist(DeviceCreateDto device);

        public Task<DeviceReadDto?> Update(DeviceUpdateDto dto);
    }
}