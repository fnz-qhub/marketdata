namespace MarketData.Db.Mongo.Entities;

using MarketData.Db.Entities;
using MarketData.Db.Interfaces;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Fund entity.
/// </summary>
public record Fund : IFund
{
    /// <summary>
    /// Fund identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// ISIN of the fund.
    /// </summary>
    [MaxLength(12)]
    public required string Isin { get; init; }

    /// <summary>
    /// Name of the fund.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Fund class.
    /// </summary>
    public required FundClass Class { get; init; }
}