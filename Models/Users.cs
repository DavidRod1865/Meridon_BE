using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("users")] // Ensure table name matches the Supabase schema
public class User : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("clerk_id")]
    public string ClerkId { get; set; }

    [Column("role")]
    public string Role { get; set; } // e.g., Trader, Scheduler, Risk Manager
}
