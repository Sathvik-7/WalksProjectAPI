using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace WalksProjectAPI.Repositories
{
    public class SQLWalksRepository : IWalksRepository
    {
        private readonly WalksDbContext _dbContext;
        public SQLWalksRepository(WalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        
        public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, 
            bool isAscending =true, int pageNumber = 1, int pageSize = 3)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();//ToListAsync();

            #region Filter
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            #endregion

            #region Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            #endregion

            #region Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            #endregion

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync(); 
            //return await _dbContext.Walks.ToListAsync();
        }

        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=> x.Id == id);
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
