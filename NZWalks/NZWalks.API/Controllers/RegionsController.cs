using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Regions;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await _unitOfWork.Region.GetAllAsync();

            return Ok(_mapper.Map<List<RegionDTO>>(regions));
        }

        //[HttpGet("{id:guid}", Name = "GetRegion")]
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _unitOfWork.Region.GetAsync(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ActionName("CreateRegion")]
        public async Task<IActionResult> CreateRegionAsync(RegionCreateDTO regionCreateDTO)
        {
            //Request(DTO) to domain model
            var region = new Region()
            {
                Id = new Guid(),
                Code = regionCreateDTO.Code,
                Name = regionCreateDTO.Name,
                Area = regionCreateDTO.Area,
                Lat = regionCreateDTO.Lat,
                Long = regionCreateDTO.Long,
                Population = regionCreateDTO.Population
            };

            //Pass details to repository
            await _unitOfWork.Region.CreateAsync(region);
            await _unitOfWork.SaveAsync();

            //convert back to dto
            var regionDTO = _mapper.Map<RegionDTO>(region);
            return CreatedAtAction("GetRegion", new { id = region.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ActionName("DeleteRegion")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from db
            var region = await _unitOfWork.Region.GetAsync(x => x.Id == id);

            //if NULL not found
            if (region == null)
            {
                return NotFound();
            }

            //remove if not null
            await _unitOfWork.Region.RemoveAsync(region);
            await _unitOfWork.SaveAsync();

            //convert backend to DTO
            var regionDTO = _mapper.Map<RegionDTO>(region);
            //return Ok
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] RegionUpdateDTO regionUpdateDTO)
        {
            var existRegion = await _unitOfWork.Region.GetAsync(x => x.Id == id);
            if (existRegion == null)
            {
                return NotFound();
            }

            existRegion.Code = regionUpdateDTO.Code;
            existRegion.Name = regionUpdateDTO.Name;
            existRegion.Area = regionUpdateDTO.Area;
            existRegion.Lat = regionUpdateDTO.Lat;
            existRegion.Long = regionUpdateDTO.Long;
            existRegion.Population = regionUpdateDTO.Population;

            await _unitOfWork.Region.UpdateAsync(existRegion);
            await _unitOfWork.SaveAsync();

            var regionDTO = _mapper.Map<RegionDTO>(existRegion);

            return Ok(regionDTO);
        }
    }
}