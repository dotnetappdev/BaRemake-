using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

/// <summary>
/// Persisted branding/white-label settings.
/// One row in DB (Id = 1), updated via Admin → Settings.
/// </summary>
public class BrandingSettings
{
    public int Id { get; set; } = 1;

    [Required, StringLength(100)]
    public string SiteName { get; set; } = "BaRemake";

    [StringLength(200)]
    public string TagLine { get; set; } = "The World's Favourite Airline";

    [StringLength(500)]
    public string? LogoUrl { get; set; }

    /// <summary>Inline SVG or emoji for logo fallback</summary>
    [StringLength(20)]
    public string LogoIcon { get; set; } = "✈";

    // Brand colours
    [StringLength(7)]
    public string PrimaryColor { get; set; } = "#002157";

    [StringLength(7)]
    public string SecondaryColor { get; set; } = "#c6a84b";

    [StringLength(7)]
    public string AccentColor { get; set; } = "#75aadb";

    // Theme
    public string ThemePreset { get; set; } = "BA"; // BA, BookIt, Green, Red, Custom
    public bool DefaultDarkMode { get; set; }

    // Header
    [StringLength(7)]
    public string HeaderBackground { get; set; } = "#002157";

    [StringLength(7)]
    public string HeaderTextColor { get; set; } = "#ffffff";

    // Footer text
    [StringLength(200)]
    public string? FooterCopyright { get; set; }
    public string FooterTechLine { get; set; } = "Built with .NET 10 · Blazor · EF Core";

    // SEO / meta
    [StringLength(200)]
    public string? MetaDescription { get; set; }

    // Features toggles
    public bool ShowHotels { get; set; } = true;
    public bool ShowPackages { get; set; } = true;
    public bool ShowCarHire { get; set; } = false;
    public bool ShowMultiAirline { get; set; } = true;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public string? UpdatedByUserId { get; set; }
}
