using GoldPrices.Domain.GoldPrices;
using MediatR;

namespace GoldPrices.Features.GoldPrices.Commands;

public class CreateGoldPrice : IRequest<Guid>
{
    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public float AveragePrice { get; init; }

    public GoldPrice ToDatabaseEntity(Guid? id = null)
    {
        return new GoldPrice
        {
            Id = id ?? Guid.NewGuid(),
            StartDate = StartDate,
            EndDate = EndDate,
            AveragePrice = AveragePrice,
        };
    }
}