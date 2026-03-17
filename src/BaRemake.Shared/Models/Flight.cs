using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Flight
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string FlightNumber { get; set; } = string.Empty;

    // Airline that operates this flight
    public int AirlineId { get; set; }
    public Airline Airline { get; set; } = null!;

    public int RouteId { get; set; }
    public Route Route { get; set; } = null!;

    public int AircraftId { get; set; }
    public Aircraft Aircraft { get; set; } = null!;

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public FlightStatus Status { get; set; } = FlightStatus.Scheduled;

    public int AvailableEconomySeats { get; set; }
    public int AvailableBusinessSeats { get; set; }
    public int AvailableFirstSeats { get; set; }

    // Navigation
    public ICollection<FlightSeatPrice> SeatPrices { get; set; } = new List<FlightSeatPrice>();
    public ICollection<BookingPassenger> Passengers { get; set; } = new List<BookingPassenger>();
}
