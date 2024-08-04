using DemoProjectAPI.Models.Domain;

namespace WalksProjectAPI.Repositories
{
    public interface IWalksRepository
    {
        Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery=null,
            string? sortBy=null, bool isAscending = true,int pageNumber = 1,int pageSize = 3);

        Task<Walks?> GetByIdAsync(Guid id);

        Task<Walks?> UpdateAsync(Guid id, Walks walks);

        Task<Walks?> DeleteAsync(Guid id);

        Task<Walks> CreateAsync(Walks walks);
    }
}
