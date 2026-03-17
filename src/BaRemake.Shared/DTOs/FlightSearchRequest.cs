using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.DTOs;

public class FlightSearchRequest
{
    [Required]
    public string OriginCode { get; set; } = string.Empty;

    [Required]
    public string DestinationCode { get; set; } = string.Empty;

    [Required]
    public DateTime DepartureDate { get; set; } = DateTime.Today.AddDays(7);

    public DateTime? ReturnDate { get; set; }

    [Range(1, 9)]
    public int Adults { get; set; } = 1;

    [Range(0, 8)]
    public int Children { get; set; } = 0;

    [Range(0, 4)]
    public int Infants { get; set; } = 0;

    public SeatClass CabinClass { get; set; } = SeatClass.Economy;

    public TripType TripType { get; set; } = TripType.OneWay;

    public int TotalPassengers => Adults + Children;
}
