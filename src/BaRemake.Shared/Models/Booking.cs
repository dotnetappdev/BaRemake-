using BaRemake.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Booking
{
    public int Id { get; set; }

    [Required, StringLength(10)]
    public string BookingReference { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public TripType TripType { get; set; } = TripType.OneWay;

    public decimal TotalPrice { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal FeeAmount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ConfirmedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    // Contact details for booking
    [EmailAddress]
    public string ContactEmail { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;

    // Payment reference (in real system would link to payment provider)
    public string? PaymentReference { get; set; }

    // Navigation
    public ICollection<BookingPassenger> Passengers { get; set; } = new List<BookingPassenger>();
}
