
namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void Create(T entityToCreate);
        void Update(T entityToUpdate);
        void Delete(T entityToDelete);
        bool IsExist(Guid id);
        void Detach(T entity);
    }
}
