using MediatR;

namespace GoldPrices.Features.GoldPrices.Queries;

public class GetGoldPrices : IRequest<IReadOnlyList<GoldPriceResponse>>
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}