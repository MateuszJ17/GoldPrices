using System.Reflection;
using GoldPrices.Domain.GoldPrices;
using Microsoft.EntityFrameworkCore;

namespace GoldPrices.Infrastructure;

public class GoldPricesDbContext : ApplicationDbContext
{
    public DbSet<GoldPrice> GoldPrices { get; set; }

    public string DbPath { get; init; }

    public GoldPricesDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        DbPath = Path.Join(path, "goldprices.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}