using GoldPrices.Features.GoldPrices;

namespace GoldPrices.Domain.GoldPrices;

public class GoldPrice
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public float AveragePrice { get; set; }

    public GoldPriceResponse ToResponse()
    {
        return new GoldPriceResponse(Id, StartDate, EndDate, AveragePrice);
    }
}