using GoldPrices.Domain.GoldPrices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldPrices.Infrastructure.EntityConfigurations;

public class GoldPriceConfiguration : IEntityTypeConfiguration<GoldPrice>
{
    public void Configure(EntityTypeBuilder<GoldPrice> builder)
    {
        builder.HasKey(x => x.Id);
    }
}