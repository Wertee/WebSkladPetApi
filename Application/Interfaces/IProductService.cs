using Application.Product.DTO;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> Get();
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task Create(ProductDTO productDto);
        Task Update(ProductDTO productDto);
        Task Delete(Guid id);
    }
}
