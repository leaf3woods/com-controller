using Domain.Repos.Dtos;

namespace Domain.Repos.IRepositories
{
    public interface IOperationLogRepo : IBaseRepo<OperationLogReadDto, OperationLogUpdateDto, OperationLogCreateDto>
    {
        Task<IEnumerable<OperationLogReadDto>> FindViaLimsi(string limsi);

        Task<OperationLogReadDto?> Update(OperationLogUpdateDto dto);
    }
}