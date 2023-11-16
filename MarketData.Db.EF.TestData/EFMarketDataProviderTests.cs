namespace MarketData.Db.EF.TestData;

using MarketData.Db.EF.Entities;

public class EFMarketDataProviderTests : EFBaseTest
{
    [Fact]
    public async Task CanInsertFund()
    {
        // Arrange
        var fund = new Fund {
            Class = Db.Entities.FundClass.Equity,
            Isin = "EXAMPLE",
            Name = "Example Equity",
        };
        await using var db = GetDbContext();
        var provider = new EFMarketDataProvider(db);

        // Act
        await provider.InsertFund(fund);
        db.ChangeTracker.Clear();

        // Assert
        Assert.NotEmpty(db.Funds);
    }

    [Fact]
    public async Task CheckLatestPrices()
    {
        await using (var dbInner = GetDbContext())
        {
            await dbInner.Funds.AddRangeAsync(MarketTestData.ExampleFunds);
            _ = await dbInner.SaveChangesAsync();
        }

        await using var db = GetDbContext();
        var provider = new EFMarketDataProvider(db);

        var fund = "FR0010957852";
        var price = await provider.GetLatestPrice(fund);
        Assert.NotNull(price);
        Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
        Assert.Equal(23.41m, price.Price);

        fund = "LU0312383663";
        price = await provider.GetLatestPrice(fund);
        Assert.NotNull(price);
        Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
        Assert.Equal(132.344m, price.Price);

        fund = "LU1819949089";
        price = await provider.GetLatestPrice(fund);
        Assert.NotNull(price);
        Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
        Assert.Equal(1.0232m, price.Price);
    }
}
