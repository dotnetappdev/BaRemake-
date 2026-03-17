using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class ApplicationUser : IdentityUser
{
    [Required, StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public DateTime DateOfBirth { get; set; }

    [StringLength(20)]
    public string? PassportNumber { get; set; }

    [StringLength(3)]
    public string? PassportCountry { get; set; }

    public DateTime? PassportExpiry { get; set; }

    [StringLength(20)]
    public string? Title { get; set; } // Mr, Mrs, Ms, Dr etc.

    public string? PreferredSeatPreference { get; set; } // Window, Aisle, Middle

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Multi-tenancy: null = direct customer, set = belongs to travel agent tenant
    public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    // For travel agents: the users they manage
    public string? ManagedByAgentId { get; set; }

    // Navigation
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<HotelBooking> HotelBookings { get; set; } = new List<HotelBooking>();
}
