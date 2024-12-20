using FluentValidation;
using GoldPrices.Features.GoldPrices.Commands;

namespace GoldPrices.Features.GoldPrices.Validators;

public class CreateGoldPriceValidator : AbstractValidator<CreateGoldPrice>
{
    public CreateGoldPriceValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty();
        
        RuleFor(x => x)
            .Must(x => x.StartDate <= x.EndDate)
            .WithMessage(CustomErrorMessages.StartDateEarlierThanEndDateMessage)
            .WithName(x => nameof(x.StartDate));
        
        RuleFor(x => x.AveragePrice)
            .NotNull();
    }
}