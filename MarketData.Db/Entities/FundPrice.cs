﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MarketData.Db.Entities;

public record FundPrice
{
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
    public required DateTime Timestamp { get; init; }

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