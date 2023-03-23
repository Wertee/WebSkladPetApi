using Application.Outcome.DTO;

namespace Application.Interfaces
{
    public interface IOutcomeService
    {
        Task<List<OutcomeDTO>> GetAllAsync();
        Task<OutcomeDTO> GetByIdAsync(Guid id);
        Task CreateAsync(OutcomeDTO productDto);
        Task UpdateAsync(OutcomeDTO productDto);
        Task DeleteAsync(Guid id);
    }
}
