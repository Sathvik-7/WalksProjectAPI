using AutoMapper;
using DemoProjectAPI.Models.Domain;
using DemoProjectAPI.Models.DTO;
using DemoProjectAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalksProjectAPI.Models.DTO;
using WalksProjectAPI.Repositories;

namespace WalksProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walkrepository;
        public WalksController(IMapper _mapper, IWalksRepository _walkrepository)
        {
            this._mapper = _mapper;
            this._walkrepository = _walkrepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walksDomainModel = _mapper.Map<Walks>(addWalksRequestDto);
            await _walkrepository.CreateAsync(walksDomainModel);
            return Ok(_mapper.Map<Walks>(walksDomainModel));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await _walkrepository.GetAllAsync();
            return Ok(_mapper.Map<List<Walks>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walkrepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<WalksDto>(walkDomain));//AutoMapper
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            var walk = _mapper.Map<Walks>(updateWalksRequestDto);

            var walkDomainModel = await _walkrepository.UpdateAsync(id, walk);

            if (walkDomainModel == null)
                return NotFound();

            return Ok(_mapper.Map<WalksDto>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _walkrepository.DeleteAsync(id);
            if (walkDomain == null)
                return NotFound();

            //returning after Mapping
            return Ok(_mapper.Map<WalksDto>(walkDomain));
        }
    }
}
