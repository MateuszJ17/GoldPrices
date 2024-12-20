using FluentValidation;
using GoldPrices.Features.GoldPrices.Queries;

namespace GoldPrices.Features.GoldPrices.Validators;

public class GetGoldPricesValidator : AbstractValidator<GetGoldPrices>
{
    public GetGoldPricesValidator()
    {
        RuleFor(x => x)
            .Must(x => x.StartDate <= x.EndDate)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage(CustomErrorMessages.StartDateEarlierThanEndDateMessage)
            .WithName(x => nameof(x.StartDate));

    }
}