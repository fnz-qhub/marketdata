using MarketData.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketData.Db.EF.TestData
{
    public class MarketTestData
    {
        [Fact]
        public async Task Populate()
        {
            var funds = new[]
            {
                new Fund
                {
                    Isin = "FR0010957852",
                    Name = "BNP Paribas MidCap Europe",
                    Class = FundClass.Equity,
                    Prices = new[] 
                    {
                        (22.64m, new DateOnly(2023, 10, 25)),
                        (23.12m, new DateOnly(2023, 10, 26)),
                        (23.78m, new DateOnly(2023, 10, 27)),
                        (21.89m, new DateOnly(2023, 10, 30)),
                        (22.02m, new DateOnly(2023, 10, 31)),
                        (23.41m, new DateOnly(2023, 11, 1)),
                    }.Select(priceDate => new FundPrice
                    {
                        Price = priceDate.Item1,
                        PriceDate = priceDate.Item2,
                        Timestamp = DateTime.UtcNow
                    }).ToList()
                },
                new Fund
                {
                    Isin = "LU0312383663",
                    Name = "Pictet-Clean Energy",
                    Class = FundClass.Equity,
                    Prices = new[]
                    {
                        (132.767m, new DateOnly(2023, 10, 25)),
                        (129.361m, new DateOnly(2023, 10, 26)),
                        (133.291m, new DateOnly(2023, 10, 27)),
                        (135.499m, new DateOnly(2023, 10, 30)),
                        (133.987m, new DateOnly(2023, 10, 31)),
                        (132.344m, new DateOnly(2023, 11, 1)),
                    }.Select(priceDate => new FundPrice
                    {
                        Price = priceDate.Item1,
                        PriceDate = priceDate.Item2,
                        Timestamp = DateTime.UtcNow
                    }).ToList()
                },
                new Fund
                {
                    Isin = "LU1819949089",
                    Name = "BNP Paribas Sustainable Enhanced Bond",
                    Class = FundClass.Bond,
                    Prices = new[]
                    {
                        (1.0238m, new DateOnly(2023, 10, 25)),
                        (1.0198m, new DateOnly(2023, 10, 26)),
                        (1.0212m, new DateOnly(2023, 10, 27)),
                        (1.0287m, new DateOnly(2023, 10, 30)),
                        (1.0265m, new DateOnly(2023, 10, 31)),
                        (1.0232m, new DateOnly(2023, 11, 1)),
                    }.Select(priceDate => new FundPrice
                    {
                        Price = priceDate.Item1,
                        PriceDate = priceDate.Item2,
                        Timestamp = DateTime.UtcNow
                    }).ToList()
                },
            };

            await using var context = new MarketDataDbContext();
            await context.Funds.AddRangeAsync(funds);

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task RemoveAll()
        {
            await using var context = new MarketDataDbContext();
            await context.Funds.ExecuteDeleteAsync();
        }
    }
}