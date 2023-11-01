namespace MarketData.Db.Interfaces;

/// <summary>
/// An interface for fund prices.
/// </summary>
public interface IFundPrice
{
    /// <summary>
    /// Price identifier.
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Fund identifier.
    /// </summary>
    long FundId { get; set; }

    /// <summary>
    /// Timestamp the price was received.
    /// </summary>
    DateTime Timestamp { get; init; }

    /// <summary>
    /// The actual price.
    /// </summary>
    decimal Price { get; init; }

    /// <summary>
    /// The date the price was published (a.k.a. "NAV date").
    /// </summary>
    DateOnly PriceDate { get; init; }
}