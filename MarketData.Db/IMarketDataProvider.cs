using MarketData.Db.Entities;

namespace MarketData.Db;

public interface IMarketDataProvider
{
    /// <summary>
    /// Insert a new price.
    /// </summary>
    /// <param name="fundPrice">The price to insert.</param>
    /// <returns>The updated price entity.</returns>
    Task<FundPrice> InsertPrice(FundPrice fundPrice);

    /// <summary>
    /// Get latest price for a given fund (identified by an ISIN).
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <returns>Latest price for the fund.</returns>
    Task<FundPrice?> GetLatestPrice(string isin);

    /// <summary>
    /// Get latest price for a given fund (identified by an ISIN) and a given price date.
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <param name="priceDate">The price date of the price we need.</param>
    /// <returns>Latest price for the fund.</returns>
    Task<FundPrice?> GetLatestPriceForDate(string isin, DateOnly priceDate);

    /// <summary>
    /// Get latest prices for a fund (identified by an ISIN).
    /// For N last price dates, return the latest price for each price date.
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <param name="ndays">Number of last price dates to include.</param>
    /// <returns></returns>
    Task<IList<FundPrice>> GetLatestPrices(string isin, int ndays);
}