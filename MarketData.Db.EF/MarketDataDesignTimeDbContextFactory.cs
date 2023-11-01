namespace MarketData.Db.EF;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class MarketDataDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MarketDataDbContext>
{
    public MarketDataDbContext CreateDbContext(string[] args)
        => new MarketDataDbContext(
            new DbContextOptionsBuilder<MarketDataDbContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=MarketData; Trusted_Connection=True; MultipleActiveResultSets=true",
                    options => options.UseDateOnlyTimeOnly())
                .LogTo(Console.WriteLine)
                .Options);
}
