using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace WalksProjectAPI.Repositories
{
    public class SQLWalksRepository:IWalksRepository
    {
        private readonly WalksDbContext _dbContext;
        public SQLWalksRepository(WalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        
        public async Task<List<Walks>> GetAllAsync()
        {
            return await _dbContext.Walks.ToListAsync();
        }

        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> CreateAsync(Walks walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walks)
        {
            var exisitingWalks = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingWalks == null)
            {
                return null;
            }
            else
            {
                walks.Name = exisitingWalks.Name;
                walks.Description = exisitingWalks.Description;
                walks.LengthInKm = exisitingWalks.LengthInKm;
                walks.WalkImageUrl = exisitingWalks.WalkImageUrl;
                walks.RegionId = exisitingWalks.RegionId;
                walks.DifficultyId = exisitingWalks.DifficultyId;
            }
            await _dbContext.SaveChangesAsync();

            return exisitingWalks;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var exisitingWalks = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingWalks == null)
            {
                return null;
            }
            else
            {
                _dbContext.Walks.Remove(exisitingWalks);
                await _dbContext.SaveChangesAsync();
            }
            return exisitingWalks;
        }
    }
}
