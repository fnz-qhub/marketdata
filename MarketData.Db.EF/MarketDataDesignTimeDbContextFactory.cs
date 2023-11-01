namespace MarketData.Db.EF;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class MarketDataDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MarketDataDbContext>
{
    public MarketDataDbContext CreateDbContext(string[] args)
        => new(
            new DbContextOptionsBuilder<MarketDataDbContext>()
                .UseSqlServer(args.Any(arg => arg == "--full")
                    ? @"Server=localhost; Database=MarketData; User Id=MarketData; Password=M@rk3tD@t@; TrustServerCertificate=True"
                    : @"Server=(localdb)\mssqllocaldb; Database=MarketData; Trusted_Connection=True; MultipleActiveResultSets=true",
                    options => options.UseDateOnlyTimeOnly())
                .LogTo(Console.WriteLine)
                .Options);
}