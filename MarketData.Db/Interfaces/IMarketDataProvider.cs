namespace MarketData.Db.Interfaces;

/// <summary>
/// An interface for market data providers.
/// </summary>
/// <typeparam name="TFundPrice">The type of the fund price.</typeparam>
public interface IMarketDataProvider<TFundPrice>
    where TFundPrice : IFundPrice
{
    /// <summary>
    /// Insert a new price.
    /// </summary>
    /// <param name="fundPrice">The price to insert.</param>
    /// <returns>The updated price entity.</returns>
    Task<TFundPrice> InsertPrice(TFundPrice fundPrice);

    /// <summary>
    /// Get latest price for a given fund (identified by an ISIN).
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <returns>Latest price for the fund.</returns>
    Task<TFundPrice?> GetLatestPrice(string isin);

    /// <summary>
    /// Get latest price for a given fund (identified by an ISIN) and a given price date.
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <param name="priceDate">The price date of the price we need.</param>
    /// <returns>Latest price for the fund.</returns>
    Task<TFundPrice?> GetLatestPriceForDate(string isin, DateOnly priceDate);

    /// <summary>
    /// Get latest prices for a fund (identified by an ISIN).
    /// For N last price dates, return the latest price for each price date.
    /// </summary>
    /// <param name="isin">ISIN of the fund.</param>
    /// <param name="ndays">Number of last price dates to include.</param>
    /// <returns>Latest N daily prices for specified fund.</returns>
    Task<IList<TFundPrice>> GetLatestPrices(string isin, int ndays);
}