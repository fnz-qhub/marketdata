using MarketData.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketData.Db.EF;

public class EFMarketDataProvider : IMarketDataProvider
{
    private readonly MarketDataDbContext dbContext;

    public EFMarketDataProvider(MarketDataDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<FundPrice?> GetLatestPrice(string isin)
    {
        var fundId = (await dbContext.Funds
            .Where(x => x.Isin == isin)
            .FirstOrDefaultAsync())
            ?.Id;

        return await dbContext.Prices
            .Where(x => x.FundId == fundId)
            .OrderByDescending(x => x.PriceDate)
            .ThenByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync();
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