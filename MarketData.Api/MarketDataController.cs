#define UseEF
#if !UseEF
#define UseMongo
#endif
namespace MarketData.Api;

using MarketData.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;
#if UseEF
using FundPrice = Db.EF.Entities.FundPrice;
#endif
#if UseMongo
using FundPrice = Db.Mongo.Entities.FundPrice;
#endif

[ApiController]
[Route("marketdata")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataProvider<FundPrice> marketDataProvider;

    public MarketDataController(IMarketDataProvider<FundPrice> marketDataProvider)
    {
        this.marketDataProvider = marketDataProvider;
    }

    [HttpGet("{isin}/latest")]
    public async Task<ActionResult<FundPrice>> GetLatestFundPrice(string isin)
    {
        return Ok(await marketDataProvider.GetLatestPrice(isin));
    }
}
