using System.ComponentModel.DataAnnotations;

namespace BaRemake.Shared.Models;

public class HotelRoom
{
    public int Id { get; set; }

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    [Required, StringLength(100)]
    public string RoomType { get; set; } = string.Empty; // "Standard Double", "Superior King", "Suite"

    [StringLength(1000)]
    public string? Description { get; set; }

    public int MaxOccupancy { get; set; } = 2;
    public int BedCount { get; set; } = 1;
    public BedType BedType { get; set; } = BedType.Double;
    public int SizeSquareMetres { get; set; }

    public decimal PricePerNight { get; set; }

    public int TotalRooms { get; set; } = 10;
    public int AvailableRooms { get; set; } = 10;

    public bool HasBalcony { get; set; }
    public bool HasSeaView { get; set; }
    public bool HasCityView { get; set; }
    public bool HasKitchenette { get; set; }
    public bool HasBath { get; set; }
    public bool IsNonSmoking { get; set; } = true;
    public bool IncludesBreakfast { get; set; }

    public string? ImagesJson { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<HotelBooking> Bookings { get; set; } = new List<HotelBooking>();
}

public enum BedType
{
    Single = 1,
    Twin = 2,
    Double = 3,
    Queen = 4,
    King = 5,
    SuperKing = 6,
    Bunk = 7
}
