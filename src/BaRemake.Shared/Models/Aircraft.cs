using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Aircraft
{
    public int Id { get; set; }

    [Required, StringLength(50)]
    public string Model { get; set; } = string.Empty;

    [Required, StringLength(10)]
    public string Registration { get; set; } = string.Empty;

    public int TotalRows { get; set; }
    public int SeatsPerRow { get; set; }

    // JSON: e.g. "3-3" for A320, "2-4-2" for wide-body, "3-4-3" for B777
    public string SeatLayout { get; set; } = "3-3";

    // Economy rows start/end
    public int EconomyStartRow { get; set; } = 8;
    public int EconomyEndRow { get; set; } = 30;

    // Business rows
    public int BusinessStartRow { get; set; } = 1;
    public int BusinessEndRow { get; set; } = 7;

    // Extra legroom rows (comma-separated, e.g. "8,14,15")
    public string ExtraLegroomRows { get; set; } = "8,14,15";

    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<AircraftSeat> Seats { get; set; } = new List<AircraftSeat>();
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
