using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/commodities")]
public class CommodityController : ControllerBase
{
    private readonly CommodityService _commodityService;

    public CommodityController(CommodityService commodityService)
    {
        _commodityService = commodityService;
    }

    [HttpGet("prices")]
    public async Task<IActionResult> GetPrices([FromQuery] string startDate, [FromQuery] string endDate)
    {
        if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
        {
            return BadRequest("Start date and end date are required.");
        }

        var data = await _commodityService.GetPricesByDateRangeAsync(startDate, endDate);
        return Ok(data);
    }

}
