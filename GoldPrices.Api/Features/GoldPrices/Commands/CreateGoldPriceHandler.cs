using GoldPrices.Infrastructure;
using MediatR;

namespace GoldPrices.Features.GoldPrices.Commands;

public class CreateGoldPriceHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateGoldPrice, Guid>
{
    public async Task<Guid> Handle(CreateGoldPrice request, CancellationToken cancellationToken)
    {
        var entity = request.ToDatabaseEntity();

        await dbContext.GoldPrices.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}