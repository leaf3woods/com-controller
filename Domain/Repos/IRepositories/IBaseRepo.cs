using Domain.Repos.Model;

namespace Domain.Repos.IRepositories
{
    public interface IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity?> Get(Guid id);

        public Task<IEnumerable<TEntity>> GetAll();

        public Task<int> Create(TEntity entity);

        public Task<int> CreateRange(IEnumerable<TEntity> entities);

        public Task<int> Delete(Guid id);

        public Task<int> DeleteRange(IEnumerable<Guid> ids);
    }
}