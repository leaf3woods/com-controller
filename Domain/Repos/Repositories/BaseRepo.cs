using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos.Repositories
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : BaseEntity
    {
        public CtlDbContext ControllerDc { get; set; } = null!;
        public virtual DbSet<TEntity> Entities { get => ControllerDc.Set<TEntity>(); }

        /// <summary>
        ///     获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity?> Get(Guid id)
            => await Entities.FindAsync(id);

        /// <summary>
        ///     获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll()
            => await Entities.ToArrayAsync();

        /// <summary>
        ///     增加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> Create(TEntity entity)
        {
            await Entities.AddAsync(entity);
            return await ControllerDc.SaveChangesAsync();
        }

        /// <summary>
        ///     增加多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> CreateRange(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
            return await ControllerDc.SaveChangesAsync();
        }

        /// <summary>
        ///     删除指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<int> Delete(Guid id)
        {
            var entity = await Entities.FindAsync(id);
            Entities.Remove(entity ?? throw new Exception($"{nameof(TEntity)} is not exist"));
            return await ControllerDc.SaveChangesAsync();
        }

        /// <summary>
        ///     删除多个实体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteRange(IEnumerable<Guid> ids)
        {
            var entities = new List<TEntity>();
            foreach (var id in ids)
            {
                var entity = await Entities.FindAsync(id);
                if (entity is not null) entities.Add(entity);
            }
            Entities.RemoveRange(entities);
            return await ControllerDc.SaveChangesAsync();
        }
    }
}