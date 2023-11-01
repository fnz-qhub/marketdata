namespace MarketData.Api;

using MarketData.Api.ApiModels;
using MarketData.Db.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("marketdata")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataProvider marketDataProvider;

    public MarketDataController(IMarketDataProvider marketDataProvider)
        => this.marketDataProvider = marketDataProvider;

    [HttpGet("{isin}/latest")]
    [Produces(typeof(ApiFundPrice))]
    [ProducesResponseType(204)]
    public async Task<ActionResult<ApiFundPrice>> GetLatestFundPrice(string isin)
        => Ok(await marketDataProvider.GetLatestPrice(isin));
}
