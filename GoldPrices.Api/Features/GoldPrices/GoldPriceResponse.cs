namespace GoldPrices.Features.GoldPrices;

public record GoldPriceResponse(Guid Id, DateTime StartDate, DateTime EndDate, float AveragePrice);