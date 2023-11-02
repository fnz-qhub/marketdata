namespace MarketData.Db.EF.Entities;

using MarketData.Db.Entities;
using MarketData.Db.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Fund entity.
/// </summary>
[Index(nameof(Isin), IsUnique = true, Name = "IX_Funds_Isin")]
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

    /// <summary>
    /// Fund prices.
    /// </summary>
    [InverseProperty(nameof(FundPrice.Fund))]
    public IList<FundPrice> Prices { get; init; } = new List<FundPrice>();
}