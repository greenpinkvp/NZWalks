using FluentValidation;
using NZWalks.API.Models.DTO.WalkDifficulties;

namespace NZWalks.API.Validators
{
    public class WalkDifficultyCreateValidator:AbstractValidator<WalkDifficultyCreateDTO>
    {
        public WalkDifficultyCreateValidator()
        {
            RuleFor(x=>x.Code).NotEmpty();
        }
    }
}
