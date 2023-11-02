namespace MarketData.Api.ApiModels;

using MarketData.Db.Interfaces;
using System;

/// <summary>
/// An API model for fund prices.
/// </summary>
public class ApiFundPrice : IFundPrice
{
    /// <inheritdoc/>
    public long FundId { get; set; }

    /// <inheritdoc/>
    public DateTime Timestamp { get; init; }

    /// <inheritdoc/>
    public decimal Price { get; init; }

    /// <inheritdoc/>
    public DateOnly PriceDate { get; init; }
}
