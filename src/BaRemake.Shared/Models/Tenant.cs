using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

/// <summary>
/// Represents a tenant (travel agency, corporate client, etc.)
/// All customer accounts belonging to a travel agent are scoped to a tenant.
/// </summary>
public class Tenant
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Slug { get; set; } = string.Empty;

    [EmailAddress, StringLength(200)]
    public string? ContactEmail { get; set; }

    [StringLength(20)]
    public string? ContactPhone { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    // IATA agency code for travel agents
    [StringLength(10)]
    public string? IATAAgencyCode { get; set; }

    public TenantType Type { get; set; } = TenantType.TravelAgent;

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Commission rate for bookings made by this tenant's agents (%)
    public decimal CommissionRate { get; set; } = 5.0m;

    // Navigation
    public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
}

public enum TenantType
{
    DirectCustomer = 0,
    TravelAgent = 1,
    CorporateClient = 2,
    WhiteLabel = 3
}
