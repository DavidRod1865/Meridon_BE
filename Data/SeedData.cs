
public static class SeedData
{
    public static async Task Initialize(Supabase.Client client)
    {
        // Check if Users table has data
        var users = await client.From<User>().Get();
        if (users.Models.Count == 0)
        {
            Console.WriteLine("Seeding Users...");
            await client.From<User>().Insert(new[]
            {
                new User { ClerkId = "clerk_001", Role = "Trader" },
                new User { ClerkId = "clerk_002", Role = "Scheduler" },
                new User { ClerkId = "clerk_003", Role = "Risk Manager" }
            });
        }

        // Check if Trades table has data
        var trades = await client.From<Trade>().Get();
        if (trades.Models.Count == 0)
        {
            Console.WriteLine("Seeding Trades...");
            await client.From<Trade>().Insert(new[]
            {
                new Trade { Commodity = "Gold", Quantity = 50, Price = 2500, Status = "Pending", TraderId = 1 },
                new Trade { Commodity = "Silver", Quantity = 100, Price = 1500, Status = "Confirmed", TraderId = 1 }
            });
        }

        // Check if Routes table has data
        var routes = await client.From<Route>().Get();
        if (routes.Models.Count == 0)
        {
            Console.WriteLine("Seeding Routes...");
            await client.From<Route>().Insert(new[]
            {
                new Route { StartLocation = "New York", EndLocation = "Los Angeles", Price = 5000, DeliveryTime = TimeSpan.FromHours(48), ScheduleDate = DateTime.UtcNow, SchedulerId = 2 },
                new Route { StartLocation = "Chicago", EndLocation = "Houston", Price = 3000, DeliveryTime = TimeSpan.FromHours(24), ScheduleDate = DateTime.UtcNow, SchedulerId = 2 }
            });
        }
    }
}
