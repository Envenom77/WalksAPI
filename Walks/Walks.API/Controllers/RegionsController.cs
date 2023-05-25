using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Data;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly WalksDBContext _dbContext;

        public RegionsController(WalksDBContext walksDBContext)
        {
            _dbContext = walksDBContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = _dbContext.Regions;

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
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

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
        public IActionResult Create([FromBody] CreateRegionDTO createRegionDTO)
        {
            var createRegion = new Region
            {
                Code = createRegionDTO.Code,
                RegionImageUrl = createRegionDTO.RegionImageUrl,
                Name = createRegionDTO.Name,
            };

            _dbContext.Regions.Add(createRegion);
            _dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {

            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            region.Name = updateRegionDTO.Name;
            region.Code = updateRegionDTO.Code;
            region.RegionImageUrl = updateRegionDTO.RegionImageUrl;

            _dbContext.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
