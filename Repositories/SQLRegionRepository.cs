using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DemoProjectAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _dbContext;
        public SQLRegionRepository(WalksDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exisitingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingRegion == null)
            {
                return null;
            }
            else
            {
                region.Code = exisitingRegion.Code;
                region.RegionImageUrl = exisitingRegion.RegionImageUrl;
                region.Name = exisitingRegion.Name;
            }
            await _dbContext.SaveChangesAsync();

            return exisitingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exisitingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exisitingRegion == null)
            {
                return null;
            }
            else
            {
                _dbContext.Regions.Remove(exisitingRegion);
                await _dbContext.SaveChangesAsync();
            }
            return exisitingRegion;
        }


    }
}
