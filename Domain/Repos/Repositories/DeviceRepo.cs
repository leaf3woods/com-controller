using Domain.Repos.Dtos;
using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos.Repositories
{
    public class DeviceRepo : BaseRepo<Device, DeviceReadDto, DeviceUpdateDto, DeviceCreateDto>, IDeviceRepo
    {
        public async Task<DeviceReadDto?> GetViaLimsi(string limsi) =>
            Mapper.Map<DeviceReadDto>(await Entities.SingleOrDefaultAsync(d => d.Limsi == limsi));

        public async Task<DeviceReadDto?> GetViaUri(string uri) =>
            Mapper.Map<DeviceReadDto>(await Entities.SingleOrDefaultAsync(d => d.Uri == uri));

        public async Task<bool> IsExist(DeviceCreateDto device) =>
            await Entities.AnyAsync(d => d.Limsi == device.Limsi && d.Uri == device.Uri);

        public override async Task<DeviceReadDto?> Update(DeviceUpdateDto dto) 
        {
            var oldEntity = await Entities.FindAsync(dto.Id);
            var newEntity = Mapper.Map(dto, oldEntity);
            if(newEntity is not null)
            {
                var result = Entities.Update(newEntity);
                return Mapper.Map<DeviceReadDto>(result);
            }
            return null;
        }
    }
}