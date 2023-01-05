using FluentValidation;
using NZWalks.API.Models.DTO.Walks;

namespace NZWalks.API.Validators
{
    public class WalkUpdateValidator : AbstractValidator<WalkUpdateDTO>
    {
        public WalkUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}