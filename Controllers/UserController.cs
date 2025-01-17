using Microsoft.AspNetCore.Mvc;
using CTRMBackend.Services;
using static Supabase.Postgrest.Constants;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly Supabase.Client _client;

    public UserController(SupabaseClientService supabaseClientService)
    {
        _client = supabaseClientService.GetClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _client.From<User>().Get();

        // Map the result to a response DTO
        var result = users.Models.Select(user => new
        {
            user.Id,
            user.ClerkId,
            user.Role
        });

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserWithDetails(int id)
    {
        var user = await _client.From<User>().Filter("id", Operator.Equals, id).Single();
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var trades = await _client.From<Trade>().Filter("trader_id", Operator.Equals, user.Id).Get();
        var routes = await _client.From<Route>().Filter("scheduler_id", Operator.Equals, user.Id).Get();

        var response = new
        {
            user.Id,
            user.ClerkId,
            user.Role,
            Trades = trades.Models,
            Routes = routes.Models
        };

        return Ok(response);
    }
}
