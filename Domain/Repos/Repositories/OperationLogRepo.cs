using Domain.Repos.IRepositories;
using Domain.Repos.Model;

namespace Domain.Repos.Repositories
{
    public class OperationLogRepo : BaseRepo<OperationLog>, IOperationLogRepo
    {
    }
}