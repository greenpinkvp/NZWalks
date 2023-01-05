using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.WalkDifficulties;
using NZWalks.API.Repositories.IRepository;
using System.Data;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/walkDifficulty")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalkDifficultiesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDiffulties = await _unitOfWork.WalkDifficulty.GetAllAsync();
            return Ok(_mapper.Map<List<WalkDifficultyDTO>>(walkDiffulties));
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDiffulty = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == id);

            if (walkDiffulty == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDifficultyDTO>(walkDiffulty));
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> CreateWalkDifficultyAsync([FromBody] WalkDifficultyCreateDTO walkDifficultyCreateDTO)
        {
            //validate the incoming request
            //if (!ValidateCreateWalkDifficultyAsync(walkDifficultyCreateDTO))
            //{
            //    return BadRequest(ModelState);
            //}

            var walkDifficulty = new WalkDifficulty()
            {
                Id = new Guid(),
                Code = walkDifficultyCreateDTO.Code
            };

            await _unitOfWork.WalkDifficulty.CreateAsync(walkDifficulty);
            await _unitOfWork.SaveAsync();

            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = walkDifficulty.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] WalkDifficultyUpdateDTO walkDifficultyUpdateDTO)
        {
            //validate the incoming request
            //if (!ValidateUpdateWalkDifficultyAsync(walkDifficultyUpdateDTO))
            //{
            //    return BadRequest(ModelState);
            //}

            var existWalkDiff = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == id);

            if (existWalkDiff == null)
            {
                return NotFound();
            }

            existWalkDiff.Code = walkDifficultyUpdateDTO.Code;

            await _unitOfWork.WalkDifficulty.UpdateAsync(existWalkDiff);
            await _unitOfWork.SaveAsync();

            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(existWalkDiff);
            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = id }, walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync([FromRoute] Guid id)
        {
            var walkDiff = await _unitOfWork.WalkDifficulty.GetAsync(x => x.Id == id);

            if (walkDiff == null)
            {
                return NotFound();
            }

            await _unitOfWork.WalkDifficulty.RemoveAsync(walkDiff);
            await _unitOfWork.SaveAsync();

            var walkDifficultyDTO = _mapper.Map<WalkDifficultyDTO>(walkDiff);
            return Ok(walkDifficultyDTO);
        }

        #region Private method

        private bool ValidateCreateWalkDifficultyAsync(WalkDifficultyCreateDTO walkDifficultyCreateDTO)
        {
            if (walkDifficultyCreateDTO == null)
            {
                ModelState.AddModelError(nameof(walkDifficultyCreateDTO), $"Walk Difficulty Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(walkDifficultyCreateDTO.Code))
            {
                ModelState.AddModelError(nameof(walkDifficultyCreateDTO.Code),
                    $"{nameof(walkDifficultyCreateDTO.Code)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(WalkDifficultyUpdateDTO walkDifficultyUpdateDTO)
        {
            if (walkDifficultyUpdateDTO == null)
            {
                ModelState.AddModelError(nameof(walkDifficultyUpdateDTO), $"Walk Difficulty Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(walkDifficultyUpdateDTO.Code))
            {
                ModelState.AddModelError(nameof(walkDifficultyUpdateDTO.Code),
                    $"{nameof(walkDifficultyUpdateDTO.Code)} cannot be null or empty or white space.");
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