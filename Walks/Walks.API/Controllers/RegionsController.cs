using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDBContext _dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(WalksDBContext walksDBContext, IRegionRepository regionRepository)
        {
            _dbContext = walksDBContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var createRegion = new Region
            {
                Code = createRegionDTO.Code,
                RegionImageUrl = createRegionDTO.RegionImageUrl,
                Name = createRegionDTO.Name,
            };

            createRegion = await regionRepository.CreateAsync(createRegion);

            var regionDTO = new RegionDTO
            {
                Id = createRegion.Id,
                Name = createRegion.Name,
                Code = createRegion.Code,
                RegionImageUrl = createRegion.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = createRegion.Id }, regionDTO);

        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var region = new Region
            {
                Name = updateRegionDTO.Name,
                Code = updateRegionDTO.Code,
                RegionImageUrl = updateRegionDTO.RegionImageUrl
            };

            region = await regionRepository.UpdateAsync(id, region);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
