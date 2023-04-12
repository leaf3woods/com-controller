using Domain.Repos.Model;

namespace Domain.Repos.IRepositories
{
    public interface IDeviceRepo : IBaseRepo<Device>
    {
        public Task<Device?> GetViaUri(string uri);

        public Task<Device?> GetViaLimsi(string limsi);
    }
}