namespace MarketData.Db.Mongo;
using MarketData.Db.Interfaces;
using MarketData.Db.Mongo.Entities;
using MongoDB.Driver;

public class MongoMarketDataProvider : IMarketDataProvider
{
    public const string FundCollectionName = "Funds";
    public const string FundPriceCollectionName = "FundPrices";

    private readonly IMongoDatabase database;

    public MongoMarketDataProvider(IMongoDatabase database)
        => this.database = database;

    public Task<IFundPrice?> GetLatestPrice(string isin)
    {
        var fundCollection = database.GetCollection<Fund>(FundCollectionName);
        var fund = fundCollection.AsQueryable()
            .Where(x => x.Isin == isin)
            .SingleOrDefault();

        if (fund == null)
            return Task.FromResult<IFundPrice?>(null);

        var fundPriceCollection = database.GetCollection<FundPrice>(FundPriceCollectionName);
        var price = fundPriceCollection.AsQueryable()
            .Where(x => x.FundId == fund.Id)
            .OrderByDescending(x => x.PriceDate)
            .ThenByDescending(x => x.Timestamp)
            .FirstOrDefault();

        return Task.FromResult((IFundPrice?)price);
    }

    public Task<IFundPrice?> GetLatestPriceForDate(string isin, DateOnly priceDate)
        => throw new NotImplementedException();

    public Task<IList<IFundPrice>> GetLatestPrices(string isin, int ndays)
        => throw new NotImplementedException();

    public Task<IFundPrice> InsertPrice(IFundPrice fundPrice)
        => throw new NotImplementedException();
}