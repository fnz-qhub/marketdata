namespace MarketData.Db.Interfaces;

using System.ComponentModel.DataAnnotations;
using MarketData.Db.Entities;

/// <summary>
/// Fund entity interface.
/// </summary>
public interface IFund
{
    /// <summary>
    /// Fund identifier.
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// ISIN of the fund.
    /// </summary>
    [MaxLength(12)]
    string Isin { get; init; }

    /// <summary>
    /// Name of the fund.
    /// </summary>
    string Name { get; init; }

    /// <summary>
    /// Fund class.
    /// </summary>
    FundClass Class { get; init; }
}