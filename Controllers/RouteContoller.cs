using Microsoft.AspNetCore.Mvc;
using CTRMBackend.Services;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/routes")]
public class RouteController : ControllerBase
{
    private readonly Supabase.Client _client;

    public RouteController(SupabaseClientService supabaseClientService)
    {
        _client = supabaseClientService.GetClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetRoutes()
    {
        var routes = await _client.From<Route>().Get();
        return Ok(routes.Models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoute(int id)
    {
        var route = await _client.From<Route>().Filter("id", Operator.Equals, id).Single();
        if (route == null)
        {
            return NotFound("Route not found.");
        }

        return Ok(route);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoute([FromBody] Route route)
    {
        if (route == null)
        {
            return BadRequest("Route details are required.");
        }

        try
        {
            var response = await _client.From<Route>().Insert(route);
            return CreatedAtAction(nameof(GetRoute), new { id = response.Models[0].Id }, response.Models[0]);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating route: {ex.Message}");
            return StatusCode(500, "An error occurred while creating the route.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        try
        {
            await _client.From<Route>().Filter("id", Operator.Equals, id).Delete();
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error deleting route: {ex.Message}");
            return StatusCode(500, "An error occurred while deleting the route.");
        }
    }
}
