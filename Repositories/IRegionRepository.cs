﻿using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;

namespace DemoProjectAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region?> UpdateAsync(Guid id,Region region);

        Task<Region?> DeleteAsync(Guid id);

        Task<Region?> CreateAsync(Region region);
    }
}
