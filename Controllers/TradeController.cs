using Microsoft.AspNetCore.Mvc;
using CTRMBackend.Services;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/trades")]
public class TradeController : ControllerBase
{
    private readonly Supabase.Client _client;

    public TradeController(SupabaseClientService supabaseClientService)
    {
        _client = supabaseClientService.GetClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetTrades()
    {
        var trades = await _client.From<Trade>().Get();
        return Ok(trades.Models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTrade(int id)
    {
        var trade = await _client.From<Trade>().Filter("id", Operator.Equals, id).Single();
        if (trade == null)
        {
            return NotFound("Trade not found.");
        }

        return Ok(trade);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTrade([FromBody] Trade trade)
    {
        if (trade == null)
        {
            return BadRequest("Trade details are required.");
        }

        try
        {
            var response = await _client.From<Trade>().Insert(trade);
            return CreatedAtAction(nameof(GetTrade), new { id = response.Models[0].Id }, response.Models[0]);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating trade: {ex.Message}");
            return StatusCode(500, "An error occurred while creating the trade.");
        }
    }

}
