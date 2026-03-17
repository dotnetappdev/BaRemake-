using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class Hotel
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string City { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string Country { get; set; } = string.Empty;

    [StringLength(3)]
    public string? NearestAirportCode { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [Range(1, 5)]
    public int StarRating { get; set; } = 3;

    public decimal ReviewScore { get; set; } // e.g. 8.7 out of 10
    public int ReviewCount { get; set; }

    [StringLength(2000)]
    public string? Description { get; set; }

    // Comma-separated amenity keys e.g. "wifi,pool,gym,spa,parking,restaurant,bar"
    public string? AmenitiesJson { get; set; }

    // JSON array of image URLs
    public string? ImagesJson { get; set; }

    [StringLength(100)]
    public string? Chain { get; set; } // e.g. "Marriott", "Hilton"

    public HotelCategory Category { get; set; } = HotelCategory.Hotel;

    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }

    public bool HasPool { get; set; }
    public bool HasSpa { get; set; }
    public bool HasGym { get; set; }
    public bool HasRestaurant { get; set; }
    public bool HasBar { get; set; }
    public bool HasParking { get; set; }
    public bool HasFreeWifi { get; set; }
    public bool HasAirConditioning { get; set; }
    public bool IsPetFriendly { get; set; }
    public bool HasRoomService { get; set; }
    public bool HasConcierge { get; set; }
    public bool HasBeachAccess { get; set; }
    public bool HasAirportShuttle { get; set; }

    // Check-in/out times
    public TimeSpan CheckInTime { get; set; } = new TimeSpan(15, 0, 0);
    public TimeSpan CheckOutTime { get; set; } = new TimeSpan(11, 0, 0);

    // Navigation
    public ICollection<HotelRoom> Rooms { get; set; } = new List<HotelRoom>();
    public ICollection<HotelBooking> Bookings { get; set; } = new List<HotelBooking>();
}

public enum HotelCategory
{
    Hotel = 1,
    Apartment = 2,
    Villa = 3,
    Resort = 4,
    Hostel = 5,
    BedAndBreakfast = 6,
    Motel = 7,
    Boutique = 8
}
