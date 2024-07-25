using AutoMapper;
using DemoProjectAPI.Data;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using DemoProjectAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProjectAPI.Controllers
{
    //https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(WalksDbContext dbContext, IRegionRepository _regionRepository, IMapper _mapper)
        {
            this._regionRepository = _regionRepository;
            this._mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get the data from Database - Domain Models
            var regions = await _regionRepository.GetAllAsync();

            #region DTO
            //Map the Domain Models to DTO
            //var regionsDto = new List<RegionDto>();
            //foreach (var item in regions)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Code = item.Code,
            //        RegionImageUrl = item.RegionImageUrl,
            //    });
            //}
            #endregion
            
            return Ok(_mapper.Map<List<RegionDto>>(regions));//return AutoMapper
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = _context.Regions.Find(id);
            var regionDomain = await _regionRepository.GetByIdAsync(id);
            
            //await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            #region DTO
            //Map the Domain Models to DTO
            //else
            //{
            //    //RegionDto regionDto = new RegionDto()
            //    //{
            //    //    Id = regionDomain.Id,
            //    //    Name = regionDomain.Name,
            //    //    Code = regionDomain.Code,
            //    //    RegionImageUrl = regionDomain.RegionImageUrl,
            //    //};

            //}
            #endregion
            
            return Ok(_mapper.Map<RegionDto>(regionDomain));//AutoMapper
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            #region Map or Convert DTO to Domain Model
            //var regionModel = new Region()
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            //};
            #endregion

            //AutoMapper
            var regionModel = _mapper.Map<Region>(addRegionRequestDto);

            #region Use Domain Model and Store the data
            //await _context.Regions.AddAsync(regionModel);
            //await _context.SaveChangesAsync();
            #endregion

            //repository folder reference
            await _regionRepository.CreateAsync(regionModel);

            #region Map or Convert DTO to Domain Model
            //var regionDto = new RegionDto()
            //{
            //    Id = regionModel.Id,
            //    Name = regionModel.Name,
            //    RegionImageUrl = regionModel.RegionImageUrl,
            //    Code = regionModel.Code
            //};
            #endregion

            //Automapper
            var regionDto = _mapper.Map<RegionDto>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            #region Map the DTO to Domain Model
            //Region region = new Region()
            //{
            //    Code = updateRegionRequestDto.Code,
            //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            //    Name = updateRegionRequestDto.Name
            //};
            #endregion

            var region = _mapper.Map<Region>(updateRegionRequestDto);

            var regionDomainModel = await _regionRepository.UpdateAsync(id, region);
            
            // _context.Regions.FindAsync(id);
            if (regionDomainModel == null)
                return NotFound();

            #region Map the DTO to Domain Model
            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            //regionDomainModel.Name = updateRegionRequestDto.Name;

            //await _context.SaveChangesAsync();

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl,
            //    Code = regionDomainModel.Code
            //};
            #endregion

            return Ok(_mapper.Map<RegionDto>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.DeleteAsync(id);
            //_context.Regions.FindAsync(id);
            if (regionDomain == null)
                return NotFound();

            #region Map DTO to Domain Model
            //_context.Regions.Remove(regionDomain);
            //await _context.SaveChangesAsync();

            //var regionDto = new RegionDto()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl,
            //    Code = regionDomain.Code
            //};
            #endregion

            //returning after Mapping
            return Ok(_mapper.Map<RegionDto>(regionDomain));
        }
    }
}
