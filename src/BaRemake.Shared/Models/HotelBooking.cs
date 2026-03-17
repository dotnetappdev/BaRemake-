using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class HotelBooking
{
    public int Id { get; set; }

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public int HotelRoomId { get; set; }
    public HotelRoom Room { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    // Could be part of a package booking
    public int? PackageBookingId { get; set; }
    public PackageBooking? PackageBooking { get; set; }

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public int Nights => (CheckOutDate - CheckInDate).Days;
    public int Guests { get; set; } = 1;

    public decimal PricePerNight { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal TaxAmount { get; set; }

    [StringLength(10)]
    public string BookingReference { get; set; } = string.Empty;

    public Shared.Enums.BookingStatus Status { get; set; } = Shared.Enums.BookingStatus.Confirmed;

    [StringLength(500)]
    public string? SpecialRequests { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CancelledAt { get; set; }

    public bool IncludesBreakfast { get; set; }
}
