namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Domain.Entity.Product>> GetAllAsync();
        Task<Domain.Entity.Product> GetByIdAsync(Guid id);
        Task CreateAsync(Domain.Entity.Product product);
        Task UpdateAsync(Domain.Entity.Product product);
        Task DeleteAsync(Domain.Entity.Product product);
        bool IsExist(Guid id);
    }
}
