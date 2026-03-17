using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class BookingPassenger
{
    public int Id { get; set; }

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int FlightId { get; set; }
    public Flight Flight { get; set; } = null!;

    // Seat assigned
    public int? AircraftSeatId { get; set; }
    public AircraftSeat? AircraftSeat { get; set; }

    [Required, StringLength(10)]
    public string Title { get; set; } = "Mr";

    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    [StringLength(20)]
    public string? PassportNumber { get; set; }

    [StringLength(3)]
    public string? NationalityCode { get; set; }

    public SeatClass Class { get; set; } = SeatClass.Economy;

    public bool IsLeadPassenger { get; set; }

    // Extras
    public CabinBaggage CabinBaggage { get; set; } = CabinBaggage.None;
    public bool HasCheckedBaggage { get; set; }
    public decimal ExtrasPrice { get; set; }

    // Check-in
    public bool HasCheckedIn { get; set; }
    public string? BoardingPassRef { get; set; }

    // Direction for return trips
    public string Direction { get; set; } = "Outbound"; // "Outbound" or "Return"
}
