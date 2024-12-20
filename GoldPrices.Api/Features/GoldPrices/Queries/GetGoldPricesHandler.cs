using System.Collections.Immutable;
using GoldPrices.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldPrices.Features.GoldPrices.Queries;

public class GetGoldPricesHandler(ApplicationDbContext dbContext, ILogger<GetGoldPricesHandler> logger)
    : IRequestHandler<GetGoldPrices, IReadOnlyList<GoldPriceResponse>>
{
    public async Task<IReadOnlyList<GoldPriceResponse>> Handle(GetGoldPrices request, CancellationToken cancellationToken)
    {
        var query = dbContext.GoldPrices.AsQueryable();

        if (request.StartDate is not null)
        {
            logger.LogInformation("Filtering by start date of {StartDate}.", request.StartDate);
            query = query.Where(x => x.StartDate >= request.StartDate);
        }

        if (request.EndDate is not null)
        {
            logger.LogInformation("Filtering by end date of {EndDate}", request.EndDate);
            query = query.Where(x => x.EndDate <= request.EndDate);
        }

        var entities = await query.ToListAsync(cancellationToken);

        return entities.Select(x => x.ToResponse()).ToImmutableList();
    }
}