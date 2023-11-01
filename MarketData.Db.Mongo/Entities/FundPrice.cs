namespace MarketData.Db.Mongo.Entities;

using MarketData.Db.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

public record FundPrice : IFundPrice
{
    public FundPrice()
    {
    }

    [SetsRequiredMembers]
    public FundPrice(long fundId, decimal price, DateOnly priceDate)
    {
        FundId = fundId;
        Price = price;
        PriceDate = priceDate;
    }

    [SetsRequiredMembers]
    public FundPrice(long id, long fundId, decimal price, DateOnly priceDate)
        : this(fundId, price, priceDate)
        => Id = id;

    /// <summary>
    /// Price identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Fund identifier.
    /// </summary>
    public long FundId { get; set; }

    /// <summary>
    /// Timestamp the price was received.
    /// </summary>
    public required DateTime Timestamp { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// The actual price.
    /// </summary>
    [Column(TypeName = "decimal(10,5)")]
    public required decimal Price { get; init; }

    /// <summary>
    /// The date the price was published (a.k.a. "NAV date").
    /// </summary>
    public required DateOnly PriceDate { get; init; }
}