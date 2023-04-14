using AutoMapper;
using Domain.Repos.Dtos;
using Domain.Repos.IRepositories;
using Domain.Repos.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repos.Repositories
{
    public abstract class BaseRepo<TEntity, TReadDto, TUpdateDto, TCreateDto> : IBaseRepo< TReadDto, TUpdateDto, TCreateDto>
        where TReadDto : ReadableDto, new()
        where TUpdateDto : UpdateableDto, new()
        where TCreateDto : CreateableDto, new()
        where TEntity : BaseEntity, new()
    {
        public CtlDbContext ControllerDc { get; set; } = null!;
        public IMapper Mapper { get; init; } = null!;
        protected virtual DbSet<TEntity> Entities { get => ControllerDc.Set<TEntity>(); }

        /// <summary>
        ///     获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TReadDto?> Get(Guid id)
            => Mapper.Map<TReadDto>(await Entities.FindAsync(id));

        /// <summary>
        ///     获取所有实体
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TReadDto>> GetAll()
            => Mapper.Map<IEnumerable<TReadDto>>(await Entities.ToArrayAsync());

        /// <summary>
        ///     增加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TReadDto?> Create(TCreateDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);
            await Entities.AddAsync(entity);
            await ControllerDc.SaveChangesAsync();
            return Mapper.Map<TReadDto>(entity);
        }

        /// <summary>
        ///     增加多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> CreateRange(IEnumerable<TCreateDto> dtos)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(dtos);
            await Entities.AddRangeAsync(entities);
            return await ControllerDc.SaveChangesAsync();
        }

        /// <summary>
        ///     删除指定实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<TReadDto?> Delete(Guid id)
        {
            var entity = await Entities.FindAsync(id);
            Entities.Remove(entity ?? throw new Exception($"{nameof(TEntity)} is not exist"));
            await ControllerDc.SaveChangesAsync();
            return Mapper.Map<TReadDto>(entity);
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

        /// <summary>
        /// 更新指定实体
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public abstract Task<TReadDto?> Update(TUpdateDto dto);
    }
}