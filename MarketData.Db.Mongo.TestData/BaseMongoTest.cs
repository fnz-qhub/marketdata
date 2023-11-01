namespace MarketData.Db.Mongo.TestData;

using EphemeralMongo;
using MarketData.Db.Mongo.Entities;
using MongoDB.Driver;

public abstract class BaseMongoTest : IDisposable
{
    public void Dispose()
    {
        runner.Dispose();
        GC.SuppressFinalize(this);
    }

    protected readonly IMongoClient MongoClient;
    protected virtual string DatabaseName { get; } = Guid.NewGuid().ToString();
    protected readonly IMongoDatabase Database;

    protected IMongoCollection<Fund> FundCollection => Database.GetCollection<Fund>(MongoMarketDataProvider.FundCollectionName);
    protected IMongoCollection<FundPrice> FundPriceCollection => Database.GetCollection<FundPrice>(MongoMarketDataProvider.FundPriceCollectionName);

    public BaseMongoTest()
    {
        MongoClient = new MongoClient(runner.ConnectionString);
        Database = MongoClient.GetDatabase(DatabaseName);
    }

    protected readonly IMongoRunner runner = MongoRunnerProvider.Get();
}
