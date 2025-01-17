using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("routes")]
public class Route : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("start_location")]
    public string StartLocation { get; set; }

    [Column("end_location")]
    public string EndLocation { get; set; }

    [Column("price")]
    public double Price { get; set; }

    [Column("delivery_time")]
    public TimeSpan DeliveryTime { get; set; }

    [Column("schedule_date")]
    public DateTime ScheduleDate { get; set; }

    // Foreign Key to User table
    [Column("scheduler_id")]
    public int SchedulerId { get; set; }
}
