namespace MarketData.Db.Mongo.TestData;

using MarketData.Db.Entities;
using MarketData.Db.Mongo.Entities;
using MongoDB.Driver;
using System;

public class MarketTestData : BaseMongoTest
{
    public static IEnumerable<Fund> ExampleFunds
    {
        get
        {
            yield return new()
            {
                Id = 1L,
                Isin = "FR0010957852",
                Name = "BNP Paribas MidCap Europe",
                Class = FundClass.Equity,
            };
            yield return new()
            {
                Id = 2L,
                Isin = "LU0312383663",
                Name = "Pictet-Clean Energy",
                Class = FundClass.Equity,
            };
            yield return new()
            {
                Id = 3L,
                Isin = "LU1819949089",
                Name = "BNP Paribas Sustainable Enhanced Bond",
                Class = FundClass.Bond,
            };
        }
    }

    public static IEnumerable<FundPrice> ExampleFundPrices
    {
        get
        {
            var i = 0L;
            yield return new(i++, 1L, 22.64m, new DateOnly(2023, 10, 25));
            yield return new(i++, 1L, 23.12m, new DateOnly(2023, 10, 26));
            yield return new(i++, 1L, 23.78m, new DateOnly(2023, 10, 27));
            yield return new(i++, 1L, 21.89m, new DateOnly(2023, 10, 30));
            yield return new(i++, 1L, 22.02m, new DateOnly(2023, 10, 31));
            yield return new(i++, 1L, 23.41m, new DateOnly(2023, 11, 1));
            yield return new(i++, 2L, 132.767m, new DateOnly(2023, 10, 25));
            yield return new(i++, 2L, 129.361m, new DateOnly(2023, 10, 26));
            yield return new(i++, 2L, 133.291m, new DateOnly(2023, 10, 27));
            yield return new(i++, 2L, 135.499m, new DateOnly(2023, 10, 30));
            yield return new(i++, 2L, 133.987m, new DateOnly(2023, 10, 31));
            yield return new(i++, 2L, 132.344m, new DateOnly(2023, 11, 1));
            yield return new(i++, 3L, 1.0238m, new DateOnly(2023, 10, 25));
            yield return new(i++, 3L, 1.0198m, new DateOnly(2023, 10, 26));
            yield return new(i++, 3L, 1.0212m, new DateOnly(2023, 10, 27));
            yield return new(i++, 3L, 1.0287m, new DateOnly(2023, 10, 30));
            yield return new(i++, 3L, 1.0265m, new DateOnly(2023, 10, 31));
            yield return new(i++, 3L, 1.0232m, new DateOnly(2023, 11, 1));
        }
    }

    [Fact]
    public async Task Populate()
    {
        await FundCollection.InsertManyAsync(ExampleFunds);
        await FundPriceCollection.InsertManyAsync(ExampleFundPrices);

        var fundCount = await FundCollection.EstimatedDocumentCountAsync();
        var fundPriceCount = await FundPriceCollection.EstimatedDocumentCountAsync();

        Assert.Equal(3, fundCount);
        Assert.Equal(18, fundPriceCount);
    }

    [Fact]
    public async Task RemoveAll()
    {
        await FundCollection.DeleteManyAsync(Builders<Fund>.Filter.Empty);
        await FundPriceCollection.DeleteManyAsync(Builders<FundPrice>.Filter.Empty);

        var fundCount = await FundCollection.EstimatedDocumentCountAsync();
        var fundPriceCount = await FundPriceCollection.EstimatedDocumentCountAsync();

        Assert.Equal(0, fundCount);
        Assert.Equal(0, fundPriceCount);
    }
}