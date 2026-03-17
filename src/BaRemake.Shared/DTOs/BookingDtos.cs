using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.DTOs;

public class BookingSessionDto
{
    public FlightSearchRequest SearchRequest { get; set; } = new();
    public FlightDto? SelectedOutboundFlight { get; set; }
    public FlightDto? SelectedReturnFlight { get; set; }
    public List<int> SelectedOutboundSeatIds { get; set; } = new();
    public List<int> SelectedReturnSeatIds { get; set; } = new();
    public List<PassengerInputDto> Passengers { get; set; } = new();
    public int CurrentStep { get; set; } = 1;

    public decimal TotalPrice { get; set; }
    public decimal TaxAmount => TotalPrice * 0.12m;

    public bool IsReturnTrip => SearchRequest.TripType == TripType.Return;
}

public class PassengerInputDto
{
    [Required]
    public string Title { get; set; } = "Mr";

    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-30);

    [StringLength(20)]
    public string? PassportNumber { get; set; }

    [StringLength(3)]
    public string? NationalityCode { get; set; }

    public bool IsLeadPassenger { get; set; }
    public CabinBaggage CabinBaggage { get; set; } = CabinBaggage.None;
    public bool HasCheckedBaggage { get; set; }
    public int OutboundSeatId { get; set; }
    public int ReturnSeatId { get; set; }
    public string OutboundSeatNumber { get; set; } = string.Empty;
    public string ReturnSeatNumber { get; set; } = string.Empty;
}

public class BookingConfirmationDto
{
    public string BookingReference { get; set; } = string.Empty;
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public string ContactEmail { get; set; } = string.Empty;
    public FlightDto OutboundFlight { get; set; } = null!;
    public FlightDto? ReturnFlight { get; set; }
    public List<PassengerInputDto> Passengers { get; set; } = new();
}

public class AdminFlightPricingDto
{
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public string Route { get; set; } = string.Empty;

    public decimal EconomyBasePrice { get; set; }
    public decimal BusinessBasePrice { get; set; }
    public decimal FirstBasePrice { get; set; }
    public decimal EconomyDynamicMultiplier { get; set; } = 1.0m;
    public decimal BusinessDynamicMultiplier { get; set; } = 1.0m;
    public decimal ExtraLegroomFee { get; set; }
    public decimal SeatSelectionFee { get; set; }
    public decimal BaggageFee { get; set; }
}

public class AdminDashboardDto
{
    public int TotalFlightsToday { get; set; }
    public int TotalBookingsToday { get; set; }
    public decimal RevenueToday { get; set; }
    public int TotalPassengersToday { get; set; }
    public int PendingBookings { get; set; }
    public int CancelledBookings { get; set; }
    public List<FlightDto> UpcomingFlights { get; set; } = new();
    public List<RecentBookingDto> RecentBookings { get; set; } = new();
}

public class RecentBookingDto
{
    public string Reference { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Route { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RegisterDto
{
    [Required]
    public string Title { get; set; } = "Mr";

    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; } = DateTime.Today.AddYears(-30);

    [Phone]
    public string? PhoneNumber { get; set; }
}

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}
