using MarketData.Db.Entities;
using MongoDB.Driver;

namespace MarketData.Db.Mongo.TestData
{
    public class MarketTestData
    {
        [Fact]
        public async Task Populate()
        {
            var funds = new[]
            {
                new Fund
                {
                    Id = 1L,
                    Isin = "FR0010957852",
                    Name = "BNP Paribas MidCap Europe",
                    Class = FundClass.Equity,
                },
                new Fund
                {
                    Id = 2L,
                    Isin = "LU0312383663",
                    Name = "Pictet-Clean Energy",
                    Class = FundClass.Equity,
                },
                new Fund
                {
                    Id = 3L,
                    Isin = "LU1819949089",
                    Name = "BNP Paribas Sustainable Enhanced Bond",
                    Class = FundClass.Bond,
                },
            };

            var prices = new[]
            {
                (1L, 22.64m, new DateOnly(2023, 10, 25)),
                (1L, 23.12m, new DateOnly(2023, 10, 26)),
                (1L, 23.78m, new DateOnly(2023, 10, 27)),
                (1L, 21.89m, new DateOnly(2023, 10, 30)),
                (1L, 22.02m, new DateOnly(2023, 10, 31)),
                (1L, 23.41m, new DateOnly(2023, 11, 1)),
                (2L, 132.767m, new DateOnly(2023, 10, 25)),
                (2L, 129.361m, new DateOnly(2023, 10, 26)),
                (2L, 133.291m, new DateOnly(2023, 10, 27)),
                (2L, 135.499m, new DateOnly(2023, 10, 30)),
                (2L, 133.987m, new DateOnly(2023, 10, 31)),
                (2L, 132.344m, new DateOnly(2023, 11, 1)),
                (3L, 1.0238m, new DateOnly(2023, 10, 25)),
                (3L, 1.0198m, new DateOnly(2023, 10, 26)),
                (3L, 1.0212m, new DateOnly(2023, 10, 27)),
                (3L, 1.0287m, new DateOnly(2023, 10, 30)),
                (3L, 1.0265m, new DateOnly(2023, 10, 31)),
                (3L, 1.0232m, new DateOnly(2023, 11, 1)),
            }.Select((priceDate, i) => new FundPrice
            {
                Id = i,
                FundId = priceDate.Item1,
                Price = priceDate.Item2,
                PriceDate = priceDate.Item3,
                Timestamp = DateTime.UtcNow
            }).ToList();

            var url = MongoUrl.Create(MongoMarketDataProvider.ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(url);
            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(url.DatabaseName);

            var fundCollection = database.GetCollection<Fund>(MongoMarketDataProvider.FundCollectionName);
            await fundCollection.InsertManyAsync(funds);

            var fundPriceCollection = database.GetCollection<FundPrice>(MongoMarketDataProvider.FundPriceCollectionName);
            await fundPriceCollection.InsertManyAsync(prices);
        }

        [Fact]
        public async Task RemoveAll()
        {
            var url = MongoUrl.Create(MongoMarketDataProvider.ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(url);
            var client = new MongoClient(clientSettings);
            var database = client.GetDatabase(url.DatabaseName);

            var fundCollection = database.GetCollection<Fund>(MongoMarketDataProvider.FundCollectionName);
            var fundPriceCollection = database.GetCollection<FundPrice>(MongoMarketDataProvider.FundPriceCollectionName);

            await fundCollection.DeleteManyAsync(Builders<Fund>.Filter.Empty);
            await fundPriceCollection.DeleteManyAsync(Builders<FundPrice>.Filter.Empty);
        }

    }
}