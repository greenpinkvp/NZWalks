using FluentValidation;
using NZWalks.API.Models.DTO.WalkDifficulties;

namespace NZWalks.API.Validators
{
    public class WalkDifficultyUpdateValidator:AbstractValidator<WalkDifficultyUpdateDTO>
    {
        public WalkDifficultyUpdateValidator()
        {
            RuleFor(x=>x.Code).NotEmpty();
        }
    }
}
