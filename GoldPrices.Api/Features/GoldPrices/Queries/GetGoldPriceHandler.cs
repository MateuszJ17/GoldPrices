using GoldPrices.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoldPrices.Features.GoldPrices.Queries;

public class GetGoldPriceHandler(ApplicationDbContext dbContext) : IRequestHandler<GetGoldPrice, GoldPriceResponse?>
{
    public async Task<GoldPriceResponse?> Handle(GetGoldPrice request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.GoldPrices
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return entity?.ToResponse();
    }
}