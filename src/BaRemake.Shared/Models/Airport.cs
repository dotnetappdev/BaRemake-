using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Airport
{
    public int Id { get; set; }

    [Required, StringLength(3, MinimumLength = 3)]
    public string IATACode { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string City { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Country { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string CountryCode { get; set; } = string.Empty;

    public string Timezone { get; set; } = "UTC";

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Route> OriginRoutes { get; set; } = new List<Route>();
    public ICollection<Route> DestinationRoutes { get; set; } = new List<Route>();
}
