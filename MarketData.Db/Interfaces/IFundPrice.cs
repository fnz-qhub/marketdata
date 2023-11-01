namespace MarketData.Db.Interfaces;

/// <summary>
/// An interface for fund prices.
/// </summary>
public interface IFundPrice
{
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

/// <summary>
/// An interface for fund prices with an Id of type <typeparamref name="TId"/>.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
public interface IFundPrice<TId> : IFundPrice
    where TId : IEquatable<TId>, IComparable<TId>
{
	/// <summary>
	/// Price identifier.
	/// </summary>
	TId Id { get; set; }
}