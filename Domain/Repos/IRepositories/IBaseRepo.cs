using Domain.Repos.Dtos;

namespace Domain.Repos.IRepositories
{
    public interface IBaseRepo<TReadDto, TUpdateDto, TCreateDto>
        where TReadDto : ReadableDto, new()
        where TUpdateDto : UpdateableDto, new()
        where TCreateDto : CreateableDto, new()
    {
        public Task<TReadDto?> Get(Guid id);

        public Task<IEnumerable<TReadDto>> GetAll();

        public Task<TReadDto?> Create(TCreateDto dto);

        public Task<int> CreateRange(IEnumerable<TCreateDto> dtos);

        public Task<TReadDto?> Delete(Guid id);

        public Task<int> DeleteRange(IEnumerable<Guid> ids);
    }
}