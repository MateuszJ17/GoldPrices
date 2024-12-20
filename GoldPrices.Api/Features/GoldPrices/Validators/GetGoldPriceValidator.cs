using FluentValidation;
using GoldPrices.Features.GoldPrices.Queries;

namespace GoldPrices.Features.GoldPrices.Validators;

public class GetGoldPriceValidator : AbstractValidator<GetGoldPrice>
{
    public GetGoldPriceValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}