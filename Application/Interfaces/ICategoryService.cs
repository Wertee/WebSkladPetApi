using Application.Category.DTO;


namespace Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> Get();
        Task<CategoryDTO> Get(Guid id);
        Task Create(CategoryDTO categoryDto);
        Task Update(CategoryDTO categoryDto);
        Task Delete(Guid id);
    }
}
