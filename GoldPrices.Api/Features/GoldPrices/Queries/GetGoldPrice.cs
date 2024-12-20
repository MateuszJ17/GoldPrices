using MediatR;

namespace GoldPrices.Features.GoldPrices.Queries;

public class GetGoldPrice : IRequest<GoldPriceResponse?>
{
    public Guid Id { get; init; }
}