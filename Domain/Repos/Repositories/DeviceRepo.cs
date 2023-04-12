using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos.Repositories
{
    public class DeviceRepo : BaseRepo<Device>, IDeviceRepo
    {
        public async Task<Device?> GetViaLimsi(string limsi) =>
            await Entities.SingleOrDefaultAsync(d => d.Limsi == limsi);

        public async Task<Device?> GetViaUri(string uri) =>
            await Entities.SingleOrDefaultAsync(d => d.Uri == uri);
    }
}