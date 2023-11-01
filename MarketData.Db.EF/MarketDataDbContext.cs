using MarketData.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketData.Db.EF;

public class MarketDataDbContext : DbContext
{
    public DbSet<Fund> Funds { get; init; } = null!;

    public DbSet<FundPrice> Prices { get; init; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        _ = optionsBuilder
            .UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=MarketData; Trusted_Connection=True; MultipleActiveResultSets=true",
                options => options.UseDateOnlyTimeOnly())
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<FundPrice>()
            .HasOne<Fund>()
            .WithMany()
            .HasForeignKey(x => x.FundId)
            .IsRequired();

        _ = modelBuilder.Entity<Fund>()
            .HasIndex(x => x.Isin).IsUnique();

        _ = modelBuilder.Entity<Fund>().HasMany(x => x.Prices)
            .WithOne()
            .HasForeignKey(x => x.FundId)
            .IsRequired();
    }
}