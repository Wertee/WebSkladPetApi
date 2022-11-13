namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Domain.Entity.Product>> GetAll();
        Task<Domain.Entity.Product> Get(Guid id);
        Task Create(Domain.Entity.Product product);
        Task Update(Domain.Entity.Product product);
        Task Delete(Domain.Entity.Product product);
        bool IsExist(Guid id);
    }
}
