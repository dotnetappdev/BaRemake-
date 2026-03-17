using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

/// <summary>
/// A holiday package = flight(s) + hotel + optional transfers
/// </summary>
public class Package
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [StringLength(200)]
    public string? ImageUrl { get; set; }

    // Origin/Destination airports
    public int OriginAirportId { get; set; }
    public Airport OriginAirport { get; set; } = null!;

    public int DestinationAirportId { get; set; }
    public Airport DestinationAirport { get; set; } = null!;

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public int Nights { get; set; } = 7;

    public decimal BasePrice { get; set; } // per person
    public decimal OriginalPrice { get; set; } // for showing "was £X"

    public bool IncludesTransfers { get; set; }
    public bool IncludesBreakfast { get; set; }
    public bool IncludesCarHire { get; set; }

    public bool IsFeatured { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime ValidFrom { get; set; } = DateTime.Today;
    public DateTime ValidTo { get; set; } = DateTime.Today.AddMonths(6);

    public string? Tags { get; set; } // "Beach", "City break", "Family", "Luxury"

    // Navigation
    public ICollection<PackageBooking> Bookings { get; set; } = new List<PackageBooking>();
}

/// <summary>
/// A booked package (flight booking + hotel booking together)
/// </summary>
public class PackageBooking
{
    public int Id { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    // The individual flight booking
    public int? FlightBookingId { get; set; }
    public Booking? FlightBooking { get; set; }

    // The hotel booking(s) in the package
    public ICollection<HotelBooking> HotelBookings { get; set; } = new List<HotelBooking>();

    [StringLength(10)]
    public string BookingReference { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }
    public Shared.Enums.BookingStatus Status { get; set; } = Shared.Enums.BookingStatus.Confirmed;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Guests { get; set; } = 1;
}
