using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
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
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _unitOfWork.Region.GetAllAsync();

            return Ok(_mapper.Map<List<RegionDTO>>(regions));
        }
    }
}