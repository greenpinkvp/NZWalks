using FluentValidation;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Validators
{
    public class WalkCreateValidator:AbstractValidator<WalkCreateDTO>
    {
        public WalkCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
