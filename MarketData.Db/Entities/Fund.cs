using System.ComponentModel.DataAnnotations;

namespace MarketData.Db.Entities;

/// <summary>
/// Fund entity.
/// </summary>
public record Fund
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

    /// <summary>
    /// Fund prices.
    /// </summary>
    public IList<FundPrice> Prices { get; init; } = new List<FundPrice>();
}