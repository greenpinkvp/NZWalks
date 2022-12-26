using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;
using NZWalks.API.Repositories.IRepository;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/walks")]
    public class WalksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await _unitOfWork.Walk.GetAllAsync(includeProperties: "Region,WalkDifficulty");
            return Ok(_mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await _unitOfWork.Walk.GetAsync(x => x.Id == id, includeProperties: "Region,WalkDifficulty");

            if (walk == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walk));
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalkAsync([FromBody] WalkCreateDTO walkCreateDTO)
        {
            var region = await _unitOfWork.Region.GetAsync(x => x.Id == walkCreateDTO.RegionId);
            if (region == null)
            {
                return NotFound("Region id is invalid.");
            }

            var walkDifficulty = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == walkCreateDTO.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                return NotFound("Walk difficulty id is invalid.");
            }

            var walk = new Walk()
            {
                Id = new Guid(),
                Name = walkCreateDTO.Name,
                Length = walkCreateDTO.Length,
                RegionId = walkCreateDTO.RegionId,
                WalkDifficultyId = walkCreateDTO.WalkDifficultyId
            };

            await _unitOfWork.Walk.CreateAsync(walk);
            await _unitOfWork.SaveAsync();

            var walkDTO = _mapper.Map<WalkDTO>(walk);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walk.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] WalkUpdateDTO walkUpdateDTO)
        {
            var existWalk = await _unitOfWork.Walk.GetAsync(x => x.Id == id);

            if (existWalk == null)
            {
                return NotFound("Walk id is invalid.");
            }

            var region = await _unitOfWork.Region.GetAsync(x => x.Id == walkUpdateDTO.RegionId);
            if (region == null)
            {
                return NotFound("Region id is invalid.");
            }

            var walkDifficulty = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == walkUpdateDTO.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                return NotFound("Walk difficulty id is invalid.");
            }

            existWalk.Name = walkUpdateDTO.Name;
            existWalk.Length = walkUpdateDTO.Length;
            existWalk.RegionId = walkUpdateDTO.RegionId;
            existWalk.WalkDifficultyId = walkUpdateDTO.WalkDifficultyId;

            await _unitOfWork.Walk.UpdateAsync(existWalk);
            await _unitOfWork.SaveAsync();

            var walkDTO = _mapper.Map<WalkDTO>(existWalk);
            return CreatedAtAction(nameof(GetWalkAsync), new { id = id }, walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await _unitOfWork.Walk.GetAsync(x => x.Id == id);

            if (walk == null)
            {
                return NotFound();
            }

            await _unitOfWork.Walk.RemoveAsync(walk);
            await _unitOfWork.SaveAsync();

            var walkDTO = _mapper.Map<WalkDTO>(walk);
            return Ok(walkDTO);
        }
    }
}