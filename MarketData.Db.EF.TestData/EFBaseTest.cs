namespace MarketData.Db.EF.TestData;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public abstract class EFBaseTest : IDisposable
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection connection = new SqliteConnection(InMemoryConnectionString);

    protected EFBaseTest()
    {
        connection.Open();
        using var db = GetDbContext();
        db.Database.EnsureCreated();
    }

    protected MarketDataDbContext GetDbContext()
    {
        var builder = new DbContextOptionsBuilder<MarketDataDbContext>();
        builder.UseSqlite(connection);
        var options = builder.Options;
        return new MarketDataDbContext(options);
    }

    public void Dispose() => connection.Dispose();
}
