using MarketData.Db.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketData.Db.EF;

public class MarketDataDbContext : DbContext
{
    public DbSet<Fund> Funds { get; init; } = null!;

    public DbSet<FundPrice> Prices { get; init; } = null!;

    public MarketDataDbContext(DbContextOptions<MarketDataDbContext> options)
        : base(options)
    {
    }
}