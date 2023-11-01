using MarketData.Db.Entities;
using MongoDB.Driver;

namespace MarketData.Db.Mongo
{
    public class MongoMarketDataProvider : IMarketDataProvider
    {
        public const string ConnectionString = "mongodb://localhost/MarketData";

        public const string FundCollectionName = "Funds";
        public const string FundPriceCollectionName = "FundPrices";

        private readonly IMongoDatabase database;

        public MongoMarketDataProvider()
        {
            var url = MongoUrl.Create(ConnectionString);
            var clientSettings = MongoClientSettings.FromUrl(url);
            var client = new MongoClient(clientSettings);
            database = client.GetDatabase(url.DatabaseName);
        }

        public Task<FundPrice?> GetLatestPrice(string isin)
        {
            var fundCollection = database.GetCollection<Fund>(FundCollectionName);
            var fund = fundCollection.AsQueryable()
                .Where(x => x.Isin == isin)
                .SingleOrDefault();

            if (fund == null)
                return Task.FromResult<FundPrice?>(null);

            var fundPriceCollection = database.GetCollection<FundPrice>(FundPriceCollectionName);
            var price = fundPriceCollection.AsQueryable()
                .Where(x => x.FundId == fund.Id)
                .OrderByDescending(x => x.PriceDate)
                .ThenByDescending(x => x.Timestamp)
                .FirstOrDefault();

            return Task.FromResult(price);
        }

        public Task<FundPrice?> GetLatestPriceForDate(string isin, DateOnly priceDate)
        {
            throw new NotImplementedException();
        }

        public Task<IList<FundPrice>> GetLatestPrices(string isin, int ndays)
        {
            throw new NotImplementedException();
        }

        public Task<FundPrice> InsertPrice(FundPrice fundPrice)
        {
            throw new NotImplementedException();
        }
    }
}