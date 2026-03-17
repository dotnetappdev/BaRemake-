using BaRemake.Shared.Enums;

namespace BaRemake.Shared.Models;

public class FlightSeatPrice
{
    public int Id { get; set; }

    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;

    public SeatClass Class { get; set; }

    public decimal BasePrice { get; set; }
    public decimal SeatSelectionFee { get; set; }
    public decimal ExtraLegroomFee { get; set; }
    public decimal BaggageFee { get; set; }

    // Dynamic pricing: multiplier applied based on occupancy
    public decimal DynamicMultiplier { get; set; } = 1.0m;

    public decimal EffectivePrice => BasePrice * DynamicMultiplier;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
