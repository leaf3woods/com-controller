using Domain.Repos.Dtos;
using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos.Repositories
{
    public class OperationLogRepo : BaseRepo<OperationLog, OperationLogReadDto, OperationLogUpdateDto, OperationLogCreateDto>, IOperationLogRepo
    {
        public async Task<IEnumerable<OperationLogReadDto>> FindViaLimsi(string limsi)
        {
            var logs = await Entities
                .Include(o => o.Device)
                .Where(o => o.Device.Limsi == limsi)
                .ToArrayAsync();
            return Mapper.Map<IEnumerable<OperationLogReadDto>>(logs);
        }

        public override async Task<OperationLogReadDto?> Update(OperationLogUpdateDto dto)
        {
            var oldEntity = await Entities.FindAsync(dto.Id);
            var newEntity = Mapper.Map(dto, oldEntity);
            if (newEntity is not null)
            {
                var result = Entities.Update(newEntity);
                return Mapper.Map<OperationLogReadDto>(result);
            }
            return null;
        }
    }
}