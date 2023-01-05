using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Walks;
using NZWalks.API.Repositories.IRepository;
using System.Data;

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
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks = await _unitOfWork.Walk.GetAllAsync(includeProperties: "Region,WalkDifficulty");
            return Ok(_mapper.Map<List<WalkDTO>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> CreateWalkAsync([FromBody] WalkCreateDTO walkCreateDTO)
        {
            //validate the incoming request
            if (!(await ValidateCreateWalkAsync(walkCreateDTO)))
            {
                return BadRequest(ModelState);
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] WalkUpdateDTO walkUpdateDTO)
        {
            //Validate the incoming request
            if (!(await ValidateUpdateWalkAsync(walkUpdateDTO)))
            {
                return BadRequest(ModelState);
            }
            var existWalk = await _unitOfWork.Walk.GetAsync(x => x.Id == id);

            if (existWalk == null)
            {
                return NotFound();
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
        [Authorize(Roles = "writer")]
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

        #region Private method

        private async Task<bool> ValidateCreateWalkAsync(WalkCreateDTO walkCreateDTO)
        {
            //if (walkCreateDTO == null)
            //{
            //    ModelState.AddModelError(nameof(walkCreateDTO), $"Walk Data is required.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(walkCreateDTO.Name))
            //{
            //    ModelState.AddModelError(nameof(walkCreateDTO.Name),
            //        $"{nameof(walkCreateDTO.Name)} cannot be null or empty or white space.");
            //}

            //if (walkCreateDTO.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(walkCreateDTO.Length),
            //        $"{nameof(walkCreateDTO.Length)} should be greater than to zero.");
            //}

            var region = await _unitOfWork.Region.GetAsync(x => x.Id == walkCreateDTO.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(walkCreateDTO.RegionId),
                    $"{nameof(walkCreateDTO.RegionId)} is invalid.");
            }

            var walkDifficulty = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == walkCreateDTO.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(walkCreateDTO.WalkDifficultyId),
                   $"{nameof(walkCreateDTO.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(WalkUpdateDTO walkUpdateDTO)
        {
            //if (walkUpdateDTO == null)
            //{
            //    ModelState.AddModelError(nameof(walkUpdateDTO), $"Walk Data is required.");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(walkUpdateDTO.Name))
            //{
            //    ModelState.AddModelError(nameof(walkUpdateDTO.Name),
            //        $"{nameof(walkUpdateDTO.Name)} cannot be null or empty or white space.");
            //}

            //if (walkUpdateDTO.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(walkUpdateDTO.Length),
            //        $"{nameof(walkUpdateDTO.Length)} should be greater than to zero.");
            //}

            var region = await _unitOfWork.Region.GetAsync(x => x.Id == walkUpdateDTO.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(walkUpdateDTO.RegionId),
                    $"{nameof(walkUpdateDTO.RegionId)} is invalid.");
            }

            var walkDifficulty = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == walkUpdateDTO.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(walkUpdateDTO.WalkDifficultyId),
                   $"{nameof(walkUpdateDTO.WalkDifficultyId)} is invalid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion Private method
    }
}