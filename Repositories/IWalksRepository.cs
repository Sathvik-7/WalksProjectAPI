using DemoProjectAPI.Models.Domain;

namespace WalksProjectAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<List<Walks>> GetAllAsync();

        Task<Walks?> GetByIdAsync(Guid id);

        Task<Walks?> UpdateAsync(Guid id, Walks walks);

        Task<Walks?> DeleteAsync(Guid id);

        Task<Walks> CreateAsync(Walks walks);
        Task UpdateAsync(Guid id, Region walk);
    }
}
