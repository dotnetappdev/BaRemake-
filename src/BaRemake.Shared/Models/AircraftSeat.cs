using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class AircraftSeat
{
    public int Id { get; set; }

    public int AircraftId { get; set; }
    public Aircraft Aircraft { get; set; } = null!;

    public int Row { get; set; }

    [Required, StringLength(1)]
    public string Column { get; set; } = string.Empty;

    // e.g. "1A"
    public string SeatNumber { get; set; } = string.Empty;

    public SeatClass Class { get; set; } = SeatClass.Economy;
    public bool IsExtraLegroom { get; set; }
    public bool IsExitRow { get; set; }
    public bool IsWindowSeat { get; set; }
    public bool IsAisleSeat { get; set; }
    public bool IsMiddleSeat { get; set; }
}
