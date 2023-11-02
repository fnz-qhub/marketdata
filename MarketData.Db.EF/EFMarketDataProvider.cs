namespace MarketData.Db.EF;

using MarketData.Db.EF.Entities;
using MarketData.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

public class EFMarketDataProvider : IMarketDataProvider
{
    private readonly MarketDataDbContext dbContext;

    public EFMarketDataProvider(MarketDataDbContext dbContext) => this.dbContext = dbContext;

    public async Task<IFund> InsertFund(IFund fund)
    {
        var newFund = new Fund {
            Class = fund.Class,
            Isin = fund.Isin,
            Name = fund.Name,
        };

        _ = await dbContext.AddAsync(newFund);
        _ = await dbContext.SaveChangesAsync();

        return newFund;
    }

    public async Task<IFundPrice?> GetLatestPrice(string isin)
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

    public Task<IFundPrice?> GetLatestPriceForDate(string isin, DateOnly priceDate)
        => throw new NotImplementedException();

    public Task<IList<IFundPrice>> GetLatestPrices(string isin, int ndays)
        => throw new NotImplementedException();

    public Task<IFundPrice> InsertPrice(IFundPrice fundPrice)
        => throw new NotImplementedException();
}