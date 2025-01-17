using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("trades")]
public class Trade : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("commodity")]
    public string Commodity { get; set; }

    [Column("quantity")]
    public double Quantity { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("status")]
    public string Status { get; set; } // e.g., Pending, Confirmed, Completed

    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }

    // Foreign Key to User table
    [Column("trader_id")] // Matches the foreign key column in the "trades" table
    public int TraderId { get; set; }
}
