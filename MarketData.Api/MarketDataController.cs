using MarketData.Db;
using MarketData.Db.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MarketData.Api
{
    [ApiController]
    [Route("marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataProvider marketDataProvider;

        public MarketDataController(IMarketDataProvider marketDataProvider)
        {
            this.marketDataProvider = marketDataProvider;
        }

        [HttpGet("{isin}/latest")]
        public async Task<ActionResult<FundPrice>> GetLatestFundPrice(string isin)
        {
            return Ok(await marketDataProvider.GetLatestPrice(isin));
        }
    }
}
