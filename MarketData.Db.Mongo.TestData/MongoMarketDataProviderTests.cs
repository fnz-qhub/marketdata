namespace MarketData.Db.Mongo.TestData;

using MarketData.Db.Entities;
using MarketData.Db.Mongo.Entities;

public class MongoMarketDataProviderTests : BaseMongoTest
{
    [Fact]
    public async Task CanInsertFund()
    {
        // Arrange
        var fund = new Fund
        {
            Class = FundClass.Equity,
            Isin = "EXAMPLE",
            Name = "Example Equity",
        };
        var provider = new MongoMarketDataProvider(Database);

        // Act
        await provider.InsertFund(fund);

        // Assert
        Assert.NotEqual(0, FundCollection.EstimatedDocumentCount());
    }

    [Fact]
    public async Task CheckLatestPrices()
    {
        var provider = new MongoMarketDataProvider(Database);
        await FundCollection.InsertManyAsync(MarketTestData.ExampleFunds);
        await FundPriceCollection.InsertManyAsync(MarketTestData.ExampleFundPrices);

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
