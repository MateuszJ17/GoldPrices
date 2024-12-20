using GoldPrices.Domain.GoldPrices;
using Microsoft.EntityFrameworkCore;

namespace GoldPrices.Infrastructure;

public abstract class ApplicationDbContext : DbContext
{
    public DbSet<GoldPrice> GoldPrices { get; set; }
    
    public string DbPath { get; protected init; }
}