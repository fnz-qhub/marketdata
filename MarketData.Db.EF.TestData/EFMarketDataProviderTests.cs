namespace MarketData.Db.EF.TestData
{
    public class EFMarketDataProviderTests
    {
        [Fact]
        public async Task CheckLatestPrices()
        {
            var provider = new EFMarketDataProvider(new MarketDataDbContext());

            var fund = "FR0010957852";
            var price = await provider.GetLatestPrice(fund);
            Assert.NotNull(price);
            Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
            Assert.Equal(23.41m, price.Price);

            fund = "LU0312383663";
            price = await provider.GetLatestPrice(fund);
            Assert.NotNull(price);
            Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
            Assert.Equal(132.344m, price.Price);

            fund = "LU1819949089";
            price = await provider.GetLatestPrice(fund);
            Assert.NotNull(price);
            Assert.Equal(new DateOnly(2023, 11, 1), price.PriceDate);
            Assert.Equal(1.0232m, price.Price);
        }
    }
}
