using FluentValidation;
using NZWalks.API.Models.DTO.Regions;

namespace NZWalks.API.Validators
{
    public class RegionUpdateValidator : AbstractValidator<RegionUpdateDTO>
    {
        public RegionUpdateValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}