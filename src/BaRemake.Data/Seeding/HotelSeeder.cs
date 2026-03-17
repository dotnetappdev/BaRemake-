using BaRemake.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Data.Seeding;

public class HotelSeeder
{
    private readonly ApplicationDbContext _db;
    public HotelSeeder(ApplicationDbContext db) => _db = db;

    public async Task SeedAsync()
    {
        if (await _db.Hotels.AnyAsync()) return;

        var hotels = new List<Hotel>
        {
            // London
            new() {
                Name = "The Langham, London",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "1c Portland Place, Regent Street, London W1B 1JA",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 4821,
                Description = "A legendary five-star hotel in the heart of London's West End, set in a magnificent Victorian building. Steps from Oxford Circus and the best of London's culture.",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 51.5154, Longitude = -0.1457,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 420, TotalRooms = 30, AvailableRooms = 18, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false },
                    new() { RoomType = "Superior Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 45, MaxOccupancy = 2, PricePerNight = 550, TotalRooms = 20, AvailableRooms = 12, HasBath = true, HasBalcony = false, IncludesBreakfast = true },
                    new() { RoomType = "Langham Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 90, MaxOccupancy = 3, PricePerNight = 1200, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasBalcony = true, HasKitchenette = true, IncludesBreakfast = true },
                }
            },
            new() {
                Name = "citizenM Tower of London",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "40 Trinity Square, London EC3N 4DJ",
                StarRating = 4, ReviewScore = 8.9m, ReviewCount = 12450,
                Description = "Modern, design-led hotel near the Tower of London with panoramic city views. Smart technology rooms and an amazing rooftop bar.",
                Category = HotelCategory.Boutique, IsFeatured = false,
                HasBar = true, HasGym = true, HasFreeWifi = true, HasAirConditioning = true,
                Latitude = 51.5082, Longitude = -0.0756,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "citizenM Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 14, MaxOccupancy = 2, PricePerNight = 145, TotalRooms = 200, AvailableRooms = 85, IncludesBreakfast = false },
                }
            },

            // Paris
            new() {
                Name = "Le Meurice",
                City = "Paris", Country = "France", NearestAirportCode = "CDG",
                Address = "228 Rue de Rivoli, 75001 Paris",
                StarRating = 5, ReviewScore = 9.5m, ReviewCount = 3201,
                Description = "The palace hotel of kings, steps from the Louvre and Tuileries Garden. Michelin-starred restaurant, spectacular gilded interiors.",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = false, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 48.8651, Longitude = 2.3316,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Prestige Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 720, TotalRooms = 20, AvailableRooms = 6, HasBath = true, IncludesBreakfast = true },
                    new() { RoomType = "Rivoli Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 120, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 5, AvailableRooms = 2, HasBath = true, HasBalcony = true, IncludesBreakfast = true },
                }
            },
            new() {
                Name = "Hotel de Crillon",
                City = "Paris", Country = "France", NearestAirportCode = "CDG",
                Address = "10 Place de la Concorde, 75008 Paris",
                StarRating = 5, ReviewScore = 9.4m, ReviewCount = 2800,
                Description = "Historic palace hotel on Place de la Concorde. Stunning Eiffel Tower views, swimming pool, and world-class Les Ambassadeurs bar.",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasRoomService = true, HasConcierge = true, HasAirConditioning = true,
                Latitude = 48.8655, Longitude = 2.3215,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 850, TotalRooms = 25, AvailableRooms = 8, HasBath = true, IncludesBreakfast = false },
                    new() { RoomType = "Deluxe Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 48, MaxOccupancy = 2, PricePerNight = 1100, TotalRooms = 15, AvailableRooms = 5, HasBath = true, HasBalcony = true, IncludesBreakfast = true },
                }
            },

            // Dubai
            new() {
                Name = "Burj Al Arab Jumeirah",
                City = "Dubai", Country = "United Arab Emirates", NearestAirportCode = "DXB",
                Address = "Jumeirah Beach Road, Dubai",
                StarRating = 5, ReviewScore = 9.6m, ReviewCount = 8901,
                Description = "The world's most iconic luxury hotel, shaped like a billowing sail. Every room a duplex suite, private beach, helicopter pad, and the finest dining.",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirportShuttle = true, HasRoomService = true, HasConcierge = true,
                Latitude = 25.1412, Longitude = 55.1853,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Suite Sea View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 170, MaxOccupancy = 3, PricePerNight = 3500, TotalRooms = 60, AvailableRooms = 20, HasBath = true, HasBalcony = true, IncludesBreakfast = true },
                    new() { RoomType = "Panoramic Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 320, MaxOccupancy = 4, PricePerNight = 7500, TotalRooms = 10, AvailableRooms = 3, HasBath = true, HasBalcony = true, HasKitchenette = true, IncludesBreakfast = true },
                }
            },

            // New York
            new() {
                Name = "The Plaza Hotel",
                City = "New York", Country = "United States", NearestAirportCode = "JFK",
                Address = "768 5th Ave, New York, NY 10019",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 11200,
                Description = "A National Historic Landmark on Central Park South at Fifth Avenue. Legendary service since 1907, magnificent rooms and the iconic Palm Court.",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasRoomService = true, HasConcierge = true, HasAirConditioning = true,
                Latitude = 40.7645, Longitude = -73.9745,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 850, TotalRooms = 50, AvailableRooms = 18, HasBath = true, HasCityView = true, IncludesBreakfast = false },
                    new() { RoomType = "Central Park View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 100, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 15, AvailableRooms = 5, HasBath = true, HasBalcony = true, IncludesBreakfast = true },
                }
            },

            // Barcelona
            new() {
                Name = "Hotel Arts Barcelona",
                City = "Barcelona", Country = "Spain", NearestAirportCode = "BCN",
                Address = "Carrer de la Marina, 19-21, 08005 Barcelona",
                StarRating = 5, ReviewScore = 9.1m, ReviewCount = 7650,
                Description = "A striking 44-storey tower on the Barcelona seafront. Rooftop pool with Mediterranean panoramas, award-winning spa and Michelin-starred dining.",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 41.3862, Longitude = 2.1976,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Sea View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 380, TotalRooms = 40, AvailableRooms = 22, HasBath = true, HasBalcony = true, HasSeaView = true, IncludesBreakfast = false },
                    new() { RoomType = "Executive Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 85, MaxOccupancy = 3, PricePerNight = 750, TotalRooms = 15, AvailableRooms = 7, HasBath = true, HasBalcony = true, HasSeaView = true, IncludesBreakfast = true },
                }
            },

            // Amsterdam
            new() {
                Name = "Waldorf Astoria Amsterdam",
                City = "Amsterdam", Country = "Netherlands", NearestAirportCode = "AMS",
                Address = "Herengracht 542-556, 1017 CG Amsterdam",
                StarRating = 5, ReviewScore = 9.3m, ReviewCount = 3400,
                Description = "Six interconnected 17th-century canal houses on the Golden Bend, fully restored. Spa, heated pool, and the finest Dutch heritage preserved.",
                Category = HotelCategory.Hotel,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 52.3632, Longitude = 4.9018,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Canal View Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 490, TotalRooms = 20, AvailableRooms = 9, HasBath = true, IncludesBreakfast = true },
                }
            },

            // Singapore
            new() {
                Name = "Marina Bay Sands",
                City = "Singapore", Country = "Singapore", NearestAirportCode = "SIN",
                Address = "10 Bayfront Avenue, Singapore 018956",
                StarRating = 5, ReviewScore = 8.8m, ReviewCount = 25000,
                Description = "Iconic three-tower hotel with the world-famous infinity pool 200m above ground. Casinos, Michelin restaurants, shopping mall, and the best city views.",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 1.2834, Longitude = 103.8607,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Room City View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 550, TotalRooms = 100, AvailableRooms = 45, IncludesBreakfast = false },
                    new() { RoomType = "Premier Room Bay View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 750, TotalRooms = 80, AvailableRooms = 30, HasBalcony = true, HasSeaView = true, IncludesBreakfast = false },
                    new() { RoomType = "Sands Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 120, MaxOccupancy = 4, PricePerNight = 1800, TotalRooms = 20, AvailableRooms = 8, HasBath = true, HasKitchenette = true, IncludesBreakfast = true },
                }
            },

            // Rome
            new() {
                Name = "Rome Cavalieri, A Waldorf Astoria Hotel",
                City = "Rome", Country = "Italy", NearestAirportCode = "FCO",
                Address = "Via Alberto Cadlolo 101, 00136 Rome",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 5600,
                Description = "Perched on Monte Mario, offering the most spectacular panoramic views of Rome. Three pools, Michelin-starred restaurant, world-class art collection.",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasAirportShuttle = true, HasConcierge = true, IsPetFriendly = true,
                Latitude = 41.9147, Longitude = 12.4323,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 36, MaxOccupancy = 2, PricePerNight = 380, TotalRooms = 30, AvailableRooms = 12, HasBath = true, IncludesBreakfast = false },
                    new() { RoomType = "Rome View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 85, MaxOccupancy = 3, PricePerNight = 950, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasBalcony = true, HasCityView = true, IncludesBreakfast = true },
                }
            },

            // Miami
            new() {
                Name = "Faena Hotel Miami Beach",
                City = "Miami", Country = "United States", NearestAirportCode = "MIA",
                Address = "3201 Collins Ave, Miami Beach, FL 33140",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 4100,
                Description = "Visionary oceanfront resort on Miami Beach. Iconic Argentinian design, whale skeleton art installation, stunning pool, and the Theatre restaurant.",
                Category = HotelCategory.Resort, IsFeatured = false,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 25.8036, Longitude = -80.1207,
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Ocean View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 48, MaxOccupancy = 2, PricePerNight = 650, TotalRooms = 35, AvailableRooms = 14, HasSeaView = true, HasBalcony = true, IncludesBreakfast = false },
                    new() { RoomType = "Faena Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 130, MaxOccupancy = 4, PricePerNight = 2500, TotalRooms = 8, AvailableRooms = 2, HasBath = true, HasKitchenette = true, HasSeaView = true, IncludesBreakfast = true },
                }
            },
        };

        _db.Hotels.AddRange(hotels);
        await _db.SaveChangesAsync();

        // Seed packages
        await SeedPackagesAsync();
    }

    private async Task SeedPackagesAsync()
    {
        if (await _db.Packages.AnyAsync()) return;

        var airports = await _db.Airports.ToDictionaryAsync(a => a.IATACode, a => a.Id);
        var hotels = await _db.Hotels.Include(h => h.Rooms).ToListAsync();

        Hotel? GetHotel(string city) => hotels.FirstOrDefault(h => h.City == city);

        var packages = new List<Package>();

        if (airports.ContainsKey("LHR") && GetHotel("Paris") is Hotel parisHotel)
            packages.Add(new Package {
                Name = "Paris City Break", Description = "4 nights in the heart of Paris with return flights from London Heathrow, including breakfast at Le Meurice.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["CDG"],
                HotelId = parisHotel.Id, Nights = 4, BasePrice = 799, OriginalPrice = 1050,
                IncludesBreakfast = true, Tags = "City break", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && GetHotel("Barcelona") is Hotel bcnHotel)
            packages.Add(new Package {
                Name = "Barcelona Beach &amp; City", Description = "7 nights in Barcelona with sea views, return flights, and airport transfers included.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["BCN"],
                HotelId = bcnHotel.Id, Nights = 7, BasePrice = 1099, OriginalPrice = 1450,
                IncludesTransfers = true, Tags = "Beach", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && GetHotel("Dubai") is Hotel dubaiHotel)
            packages.Add(new Package {
                Name = "Dubai Luxury Escape", Description = "5 nights at the Burj Al Arab, the world's most luxurious hotel, with return first class flights.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["DXB"],
                HotelId = dubaiHotel.Id, Nights = 5, BasePrice = 5999, OriginalPrice = 7500,
                IncludesBreakfast = true, IncludesTransfers = true, Tags = "Luxury", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && GetHotel("New York") is Hotel nyHotel)
            packages.Add(new Package {
                Name = "New York City Package", Description = "6 nights at The Plaza Hotel, return flights, and breakfast included.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["JFK"],
                HotelId = nyHotel.Id, Nights = 6, BasePrice = 2299, OriginalPrice = 2900,
                IncludesBreakfast = true, Tags = "City break", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (packages.Any())
        {
            _db.Packages.AddRange(packages);
            await _db.SaveChangesAsync();
        }
    }
}
