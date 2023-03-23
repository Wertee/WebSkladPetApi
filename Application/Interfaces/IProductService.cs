using Application.Product.DTO;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task CreateAsync(ProductDTO productDto);
        Task UpdateAsync(ProductDTO productDto);
        Task DeleteAsync(Guid id);
    }
}
