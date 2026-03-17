using BaRemake.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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
            // ── London ────────────────────────────────────────────────────────

            new() {
                Name = "The Langham, London",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "1c Portland Place, Regent Street, London W1B 1JA",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 4821,
                Description = "A legendary five-star hotel in the heart of London's West End, set in a magnificent Victorian building opened in 1865. Steps from Oxford Circus, the BBC, and the finest of London's culture and dining.",
                Chain = "Langham Hotels",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 51.5154, Longitude = -0.1457,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – CC-BY-SA: The Langham (Flickr 8292694584)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6f/The_Langham_%288292694584%29.jpg/1280px-The_Langham_%288292694584%29.jpg",
                    // Wikimedia Commons – Langham Hotel, Marylebone (geograph 5365323)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/The_Langham_Hotel%2C_Marylebone_%28geograph_5365323%29.jpg/1280px-The_Langham_Hotel%2C_Marylebone_%28geograph_5365323%29.jpg",
                    // Wikimedia Commons – Langham Hotel by night
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/The_Langham_Hotel_by_night_London.jpg/1280px-The_Langham_Hotel_by_night_London.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 420, TotalRooms = 30, AvailableRooms = 18, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant room with bespoke furnishings, marble bathroom and views over Portland Place." },
                    new() { RoomType = "Superior Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 45, MaxOccupancy = 2, PricePerNight = 550, TotalRooms = 20, AvailableRooms = 12, HasBath = true, HasBalcony = false, IncludesBreakfast = true, Description = "Spacious room with premium bedding, deep-soaking bath and complimentary continental breakfast." },
                    new() { RoomType = "Langham Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 90, MaxOccupancy = 3, PricePerNight = 1200, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasBalcony = true, HasKitchenette = true, IncludesBreakfast = true, Description = "Palatial suite with separate living room, butler service, kitchenette and panoramic London views." },
                }
            },

            new() {
                Name = "citizenM Tower of London",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "40 Trinity Square, London EC3N 4DJ",
                StarRating = 4, ReviewScore = 8.9m, ReviewCount = 12450,
                Description = "Modern, design-led hotel directly above Tower Hill Underground, with an extraordinary 9th-floor cloudM lounge bar offering 360° views of the Tower of London, Tower Bridge and the Thames.",
                Chain = "citizenM",
                Category = HotelCategory.Boutique, IsFeatured = false,
                HasBar = true, HasGym = true, HasFreeWifi = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 51.5082, Longitude = -0.0756,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Tower of London with bridge (public domain)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Tower_of_London_viewed_from_the_River_Thames.jpg/1280px-Tower_of_London_viewed_from_the_River_Thames.jpg",
                    // Wikimedia Commons – Tower Hill area
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Tower_Hill_-_geograph.org.uk_-_1008697.jpg/1280px-Tower_Hill_-_geograph.org.uk_-_1008697.jpg",
                    // Wikimedia Commons – Tower Bridge night
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Tower_Bridge_London_Feb_2006.jpg/1280px-Tower_Bridge_London_Feb_2006.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "citizenM Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 14, MaxOccupancy = 2, PricePerNight = 145, TotalRooms = 200, AvailableRooms = 85, IncludesBreakfast = false, IsNonSmoking = true, Description = "Compact, clever room with king XL bed, mood lighting, power shower, and full smart room controls via tablet." },
                }
            },

            new() {
                Name = "The Savoy",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "Strand, London WC2R 0EZ",
                StarRating = 5, ReviewScore = 9.3m, ReviewCount = 6210,
                Description = "London's most iconic hotel since 1889, situated between Covent Garden and the Thames. Art Deco and Edwardian grandeur, the famous American Bar, Savoy Grill, and Michelin-starred Restaurant 1890 by Gordon Ramsay.",
                Chain = "Fairmont",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 51.5104, Longitude = -0.1205,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – The Savoy Hotel turning circle entrance
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Savoy_Hotel_London_entrance.jpg/1280px-Savoy_Hotel_London_entrance.jpg",
                    // Wikimedia Commons – Savoy from Victoria Embankment Gardens
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/54/Savoy_entrance_from_Victoria_Embankment_Gardens.jpg/1280px-Savoy_entrance_from_Victoria_Embankment_Gardens.jpg",
                    // Wikimedia Commons – Shell Mex House and The Savoy from South Bank
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/The_Adelphi%2C_Shell_Mex_House_and_Savoy_Hotel.jpg/1280px-The_Adelphi%2C_Shell_Mex_House_and_Savoy_Hotel.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Superior Queen Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 36, MaxOccupancy = 2, PricePerNight = 650, TotalRooms = 50, AvailableRooms = 22, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant courtyard-facing room in Edwardian or Art Deco style with ample natural light and marble bathroom." },
                    new() { RoomType = "Luxury King River View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 48, MaxOccupancy = 2, PricePerNight = 950, TotalRooms = 30, AvailableRooms = 12, HasBath = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Generous room with panoramic Thames river views and the original glamour of the Savoy's interior design." },
                    new() { RoomType = "Junior Suite River View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 75, MaxOccupancy = 3, PricePerNight = 1650, TotalRooms = 15, AvailableRooms = 6, HasBath = true, HasSeaView = true, HasBalcony = false, IsNonSmoking = true, IncludesBreakfast = true, Description = "Open-plan suite with claw-foot bath, Edwardian furnishings and unrivalled panoramic Thames views." },
                }
            },

            new() {
                Name = "Four Seasons Hotel London at Park Lane",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "Hamilton Place, Park Lane, London W1J 7DR",
                StarRating = 5, ReviewScore = 9.1m, ReviewCount = 3890,
                Description = "Discreet Mayfair gem combining 1930s golden-age glamour with contemporary luxury. Rooftop spa with Hyde Park panoramas, Michelin-starred Pavyllon London by Yannick Alléno, and unparalleled butler service.",
                Chain = "Four Seasons",
                Category = HotelCategory.Hotel, IsFeatured = false,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true, HasParking = true, IsPetFriendly = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 51.5037, Longitude = -0.1517,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – 45 Park Lane building (adjacent Four Seasons property)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/45_Park_Lane%2C_London.jpg/1280px-45_Park_Lane%2C_London.jpg",
                    // Wikimedia Commons – Hyde Park view from Park Lane area
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Hyde_Park_London_-_April_2008.jpg/1280px-Hyde_Park_London_-_April_2008.jpg",
                    // Wikimedia Commons – Mayfair streetscape
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Mayfair%2C_London.jpg/1280px-Mayfair%2C_London.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Mayfair Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 720, TotalRooms = 80, AvailableRooms = 30, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Spacious room with magnificent urban views, marble bathroom vanity and 24-hour room service." },
                    new() { RoomType = "Hyde Park Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 110, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 12, AvailableRooms = 4, HasBath = true, HasBalcony = false, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "9th-floor suite with stunning Hyde Park views, contemporary furnishings and open-plan living and dining areas." },
                }
            },

            // ── Paris ─────────────────────────────────────────────────────────

            new() {
                Name = "Le Meurice",
                City = "Paris", Country = "France", NearestAirportCode = "CDG",
                Address = "228 Rue de Rivoli, 75001 Paris",
                StarRating = 5, ReviewScore = 9.5m, ReviewCount = 3201,
                Description = "The palace hotel of kings since 1835, opposite the Tuileries Garden between the Louvre and Place de la Concorde. Michelin 3-starred restaurant, spectacular gilded Louis XVI interiors and outstanding culinary artistry.",
                Chain = "Dorchester Collection",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = false, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 48.8651, Longitude = 2.3316,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Le Meurice exterior, Rue de Rivoli (CC-BY-SA)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/7/76/Paris_75001_Rue_de_Rivoli_no_228_Le_Meurice_20170528.jpg/1280px-Paris_75001_Rue_de_Rivoli_no_228_Le_Meurice_20170528.jpg",
                    // Wikimedia Commons – Salon Meurice interior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/SalonMeurice_Credit_Marianne_Haas_JPEGHD.jpg/1280px-SalonMeurice_Credit_Marianne_Haas_JPEGHD.jpg",
                    // Wikimedia Commons – Tuileries Garden (adjacent to hotel)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/19/Jardin_des_Tuileries_1.jpg/1280px-Jardin_des_Tuileries_1.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Prestige Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 720, TotalRooms = 20, AvailableRooms = 6, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Magnificent room with period furnishings, marble bathroom, hand-painted silk walls and Tuileries or courtyard views." },
                    new() { RoomType = "Rivoli Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 120, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 5, AvailableRooms = 2, HasBath = true, HasBalcony = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Palatial suite overlooking Rue de Rivoli with separate drawing room, butler service and panoramic Paris views." },
                }
            },

            new() {
                Name = "Hotel de Crillon",
                City = "Paris", Country = "France", NearestAirportCode = "CDG",
                Address = "10 Place de la Concorde, 75008 Paris",
                StarRating = 5, ReviewScore = 9.4m, ReviewCount = 2800,
                Description = "Historic palace hotel built in 1758 on Place de la Concorde, classified as a monument historique. The Rosewood flagship features stunning interiors by Karl Lagerfeld, an indoor pool, and the legendary Les Ambassadeurs bar.",
                Chain = "Rosewood Hotels",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasRoomService = true, HasConcierge = true, HasAirConditioning = true,
                Latitude = 48.8655, Longitude = 2.3215,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Hotel de Crillon night, Place de la Concorde
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/H%C3%B4tel_de_Crillon%2C_nuit%2C_Paris_8e_1.jpg/1280px-H%C3%B4tel_de_Crillon%2C_nuit%2C_Paris_8e_1.jpg",
                    // Wikimedia Commons – Hotel de Crillon daytime
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/H%C3%B4tel_de_Crillon%2C_Paris_25_November_2011.jpg/1280px-H%C3%B4tel_de_Crillon%2C_Paris_25_November_2011.jpg",
                    // Wikimedia Commons – Place de la Concorde with Crillon
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f7/Hotel_Crillon_-_Place_de_la_Concorde%2C_Paris_%283769629605%29.jpg/1280px-Hotel_Crillon_-_Place_de_la_Concorde%2C_Paris_%283769629605%29.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 850, TotalRooms = 25, AvailableRooms = 8, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Timeless room with Carrara marble bathroom, bespoke furnishings and views over the courtyard or Rue Boissy-d'Anglas." },
                    new() { RoomType = "Deluxe Room Concorde View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 48, MaxOccupancy = 2, PricePerNight = 1100, TotalRooms = 15, AvailableRooms = 5, HasBath = true, HasBalcony = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Magnificent room overlooking Place de la Concorde with an en-suite marble bath, Karl Lagerfeld-designed interiors." },
                }
            },

            // ── Dubai ─────────────────────────────────────────────────────────

            new() {
                Name = "Burj Al Arab Jumeirah",
                City = "Dubai", Country = "United Arab Emirates", NearestAirportCode = "DXB",
                Address = "Jumeirah Beach Road, Umm Suqeim 3, Dubai",
                StarRating = 5, ReviewScore = 9.6m, ReviewCount = 8901,
                Description = "The world's most iconic luxury hotel, soaring 321 metres on a man-made island, shaped like a billowing dhow sail. Every room is a duplex suite. Private helipad, underwater Al Mahara restaurant, and 24-hour butler service.",
                Chain = "Jumeirah Group",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirportShuttle = true, HasRoomService = true, HasConcierge = true,
                Latitude = 25.1412, Longitude = 55.1853,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Burj Al Arab exterior (CC-BY-SA)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/Burj_al_Arab_%282392511901%29.jpg/1280px-Burj_al_Arab_%282392511901%29.jpg",
                    // Wikimedia Commons – Al Mahara restaurant interior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e8/Al_Mahara_Burj_al_Araba_Dubai_March_2008b.JPG/1280px-Al_Mahara_Burj_al_Araba_Dubai_March_2008b.JPG",
                    // Wikimedia Commons – Burj Al Arab interior lobby atrium
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Burj_Al_Arab_8-Dubai_UAE-Andres_Larin.jpg/1280px-Burj_Al_Arab_8-Dubai_UAE-Andres_Larin.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Suite Sea View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 170, MaxOccupancy = 3, PricePerNight = 3500, TotalRooms = 60, AvailableRooms = 20, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "170 sqm two-floor duplex suite with panoramic Arabian Gulf views, private butler and pillar-free dining room." },
                    new() { RoomType = "Panoramic Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 320, MaxOccupancy = 4, PricePerNight = 7500, TotalRooms = 10, AvailableRooms = 3, HasBath = true, HasBalcony = true, HasKitchenette = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Enormous 320 sqm suite on upper floors with wrap-around terrace, private cinema, gold-leaf décor and dedicated butler team." },
                }
            },

            // ── New York ──────────────────────────────────────────────────────

            new() {
                Name = "The Plaza Hotel",
                City = "New York", Country = "United States", NearestAirportCode = "JFK",
                Address = "768 5th Ave, New York, NY 10019",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 11200,
                Description = "A National Historic Landmark at Fifth Avenue and Central Park South since 1907. Legendary home of Eloise, the iconic Palm Court afternoon tea, and the Oak Bar. Unrivalled address opposite Central Park.",
                Chain = "Fairmont",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasRoomService = true, HasConcierge = true, HasAirConditioning = true,
                Latitude = 40.7645, Longitude = -73.9745,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – The Plaza Hotel April 2008 exterior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Plaza_Hotel_April_2008.JPG/1280px-Plaza_Hotel_April_2008.JPG",
                    // Wikimedia Commons – Plaza Hotel from Fifth Avenue (CC)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/USA_flag_New_York_Plaza-20090519-RM-121559.jpg/1280px-USA_flag_New_York_Plaza-20090519-RM-121559.jpg",
                    // Wikimedia Commons – Grand Army Plaza and Plaza Hotel
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e2/Grand_Army_Plaza_NYC_-_panoramio.jpg/1280px-Grand_Army_Plaza_NYC_-_panoramio.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 850, TotalRooms = 50, AvailableRooms = 18, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Opulent room with beaux-arts décor, marble bathroom, Frette linens and Fifth Avenue or city views." },
                    new() { RoomType = "Central Park View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 100, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 15, AvailableRooms = 5, HasBath = true, HasBalcony = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Grand suite with floor-to-ceiling windows overlooking Central Park, separate drawing room and butler service." },
                }
            },

            // ── Barcelona ─────────────────────────────────────────────────────

            new() {
                Name = "Hotel Arts Barcelona",
                City = "Barcelona", Country = "Spain", NearestAirportCode = "BCN",
                Address = "Carrer de la Marina, 19-21, 08005 Barcelona",
                StarRating = 5, ReviewScore = 9.1m, ReviewCount = 7650,
                Description = "A striking 44-storey Ritz-Carlton tower on the Barcelona seafront beside Frank Gehry's 'El Peix' sculpture. Rooftop pool with Mediterranean panoramas, spa by Six Senses, and Michelin-starred Arola restaurant.",
                Chain = "Ritz-Carlton",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 41.3862, Longitude = 2.1976,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Hotel Arts by night (CC-BY-SA 4.0)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Hotel_Arts_by_night.JPG/1280px-Hotel_Arts_by_night.JPG",
                    // Wikimedia Commons – Hotel Arts and Torre Mapfre
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/062_Hotel_Arts_i_Torre_Mapfre.JPG/1280px-062_Hotel_Arts_i_Torre_Mapfre.JPG",
                    // Wikimedia Commons – Hotel Arts seafront Barcelona
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Barcelona_-_Hotel_Arts_01.jpg/1280px-Barcelona_-_Hotel_Arts_01.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Sea View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 380, TotalRooms = 40, AvailableRooms = 22, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Contemporary room with private balcony and sweeping views of the Mediterranean and Barcelona's Olympic Port." },
                    new() { RoomType = "Executive Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 85, MaxOccupancy = 3, PricePerNight = 750, TotalRooms = 15, AvailableRooms = 7, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Spacious suite with panoramic sea views, separate living room, and personalised butler access to Arts Club amenities." },
                }
            },

            new() {
                Name = "W Barcelona",
                City = "Barcelona", Country = "Spain", NearestAirportCode = "BCN",
                Address = "Plaça de la Rosa dels Vents, 1, 08039 Barcelona",
                StarRating = 5, ReviewScore = 8.7m, ReviewCount = 9320,
                Description = "The iconic sail-shaped tower by Ricardo Bofill at the tip of Barceloneta beach. 26 floors of Mediterranean glamour with WET rooftop pool deck, NOXE sky bar, Bliss Spa, and direct beach access.",
                Chain = "Marriott / W Hotels",
                Category = HotelCategory.Resort, IsFeatured = false,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 41.3731, Longitude = 2.1897,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – W Barcelona Hotel tower (CC-BY-SA)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/67/Barcelona_-_Hotel_W_Barcelona_%2801%29.jpg/1280px-Barcelona_-_Hotel_W_Barcelona_%2801%29.jpg",
                    // Wikimedia Commons – W Barcelona from the beach
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c3/35_Quatre_barres_i_Hotel_W.jpg/1280px-35_Quatre_barres_i_Hotel_W.jpg",
                    // Wikimedia Commons – W Barcelona sea-facing view
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a8/Barcelona_-_Hotel_W_Barcelona_%2808%29.jpg/1280px-Barcelona_-_Hotel_W_Barcelona_%2808%29.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Wonderful Room Sea View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 280, TotalRooms = 120, AvailableRooms = 55, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Stylish room with floor-to-ceiling sea-view windows, rainfall shower and W signature pillow-top bed." },
                    new() { RoomType = "Fabulous Corner Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 65, MaxOccupancy = 3, PricePerNight = 580, TotalRooms = 25, AvailableRooms = 10, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Corner suite with 270° views of the sea and city skyline, separate sitting area and double terrace." },
                    new() { RoomType = "Extreme WOW Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 300, MaxOccupancy = 6, PricePerNight = 5500, TotalRooms = 1, AvailableRooms = 1, HasBath = true, HasKitchenette = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Penthouse suite on the 26th floor with private terrace, outdoor hot tub, full kitchen and uninterrupted Mediterranean panorama." },
                }
            },

            // ── Amsterdam ─────────────────────────────────────────────────────

            new() {
                Name = "Waldorf Astoria Amsterdam",
                City = "Amsterdam", Country = "Netherlands", NearestAirportCode = "AMS",
                Address = "Herengracht 542-556, 1017 CG Amsterdam",
                StarRating = 5, ReviewScore = 9.3m, ReviewCount = 3400,
                Description = "Six interconnected 17th-century canal palaces on the Golden Bend of the Herengracht, a UNESCO World Heritage site. Heated indoor pool, Guerlain Spa, and the Michelin-starred Spectrum restaurant.",
                Chain = "Hilton / Waldorf Astoria",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 52.3632, Longitude = 4.9018,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Waldorf Astoria Amsterdam canal palace exterior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Rm1892-4.JPG/1280px-Rm1892-4.JPG",
                    // Wikimedia Commons – Herengracht canal view (UNESCO)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/80/Amsterdam_Herengracht_-_panoramio.jpg/1280px-Amsterdam_Herengracht_-_panoramio.jpg",
                    // Wikimedia Commons – Amsterdam Golden Bend canal houses
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/37/Gouden_bocht_herengracht.jpg/1280px-Gouden_bocht_herengracht.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Canal View Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 490, TotalRooms = 20, AvailableRooms = 9, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Elegant room in a historic canal palace with views over the Golden Bend, marble bathroom and Dutch heritage décor." },
                    new() { RoomType = "Grand Deluxe Canal Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 80, MaxOccupancy = 3, PricePerNight = 1100, TotalRooms = 8, AvailableRooms = 3, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Suite spanning two palace floors with canal views, antique furnishings, parlour and Guerlain bathroom products." },
                }
            },

            // ── Singapore ─────────────────────────────────────────────────────

            new() {
                Name = "Marina Bay Sands",
                City = "Singapore", Country = "Singapore", NearestAirportCode = "SIN",
                Address = "10 Bayfront Avenue, Singapore 018956",
                StarRating = 5, ReviewScore = 8.8m, ReviewCount = 25000,
                Description = "Iconic three-tower resort connected by the SkyPark with the world-famous infinity pool 200 metres above ground. Multiple Michelin-starred restaurants, Sands Casino, luxury mall, and the best city views in Asia.",
                Chain = "Las Vegas Sands",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 1.2834, Longitude = 103.8607,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Marina Bay Sands aerial/Luftbild
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Luftbild_Marina_Bay_Sands_Hotel_Singapur_%2836365614580%29_%282%29.jpg/1280px-Luftbild_Marina_Bay_Sands_Hotel_Singapur_%2836365614580%29_%282%29.jpg",
                    // Wikimedia Commons – Infinity pool panorama (CC-BY-SA 3.0)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/2012-12-30_Marina_Bay_Sands_infinity_pool.JPG/1280px-2012-12-30_Marina_Bay_Sands_infinity_pool.JPG",
                    // Wikimedia Commons – Marina Bay Sands from Merlion Park
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/32/Marina_Bay_Sands_%28view_from_Merlion_Park%29.jpg/1280px-Marina_Bay_Sands_%28view_from_Merlion_Park%29.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Room City View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 550, TotalRooms = 100, AvailableRooms = 45, IsNonSmoking = true, IncludesBreakfast = false, Description = "Contemporary room with floor-to-ceiling windows overlooking Singapore's stunning skyline and full SkyPark access." },
                    new() { RoomType = "Premier Room Bay View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 750, TotalRooms = 80, AvailableRooms = 30, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Premium room with unobstructed views of Marina Bay, Gardens by the Bay and access to the infinity pool." },
                    new() { RoomType = "Sands Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 120, MaxOccupancy = 4, PricePerNight = 1800, TotalRooms = 20, AvailableRooms = 8, HasBath = true, HasKitchenette = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Generous suite with panoramic bay views, kitchenette, butler service and VIP SkyPark infinity pool access." },
                }
            },

            new() {
                Name = "Raffles Singapore",
                City = "Singapore", Country = "Singapore", NearestAirportCode = "SIN",
                Address = "1 Beach Road, Singapore 189673",
                StarRating = 5, ReviewScore = 9.4m, ReviewCount = 6820,
                Description = "A National Monument of Singapore since 1987, this legendary colonial grand dame at the edge of the civic district is synonymous with Asian elegance. All-suite hotel, birthplace of the Singapore Sling in the Long Bar.",
                Chain = "Accor / Raffles",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 1.2949, Longitude = 103.8545,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Raffles Hotel exterior (CC-BY)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Raffles_Hotel%2C_Singapore_-_20070726.jpg/1280px-Raffles_Hotel%2C_Singapore_-_20070726.jpg",
                    // Wikimedia Commons – Raffles Long Bar
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Raffles_Hotel_Long_Bar_A.JPG/1280px-Raffles_Hotel_Long_Bar_A.JPG",
                    // Wikimedia Commons – Raffles Hotel courtyard/facade
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/86/Raffles_Hotel_Singapore_facade.jpg/1280px-Raffles_Hotel_Singapore_facade.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Superior Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 56, MaxOccupancy = 2, PricePerNight = 850, TotalRooms = 40, AvailableRooms = 18, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Gracious all-suite accommodation with high ceilings, teak floors, colonial furnishings and 24-hour butler service." },
                    new() { RoomType = "Palm Court Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 100, MaxOccupancy = 3, PricePerNight = 1600, TotalRooms = 15, AvailableRooms = 6, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Spacious suite overlooking the legendary Palm Court garden, with antique four-poster bed and personal butler." },
                }
            },

            // ── Rome ──────────────────────────────────────────────────────────

            new() {
                Name = "Rome Cavalieri, A Waldorf Astoria Hotel",
                City = "Rome", Country = "Italy", NearestAirportCode = "FCO",
                Address = "Via Alberto Cadlolo 101, 00136 Rome",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 5600,
                Description = "Perched on Monte Mario, offering the most spectacular panoramic views of Rome including St Peter's Basilica. Three pools, Michelin-starred La Pergola restaurant (Italy's only 3-star), private art collection of over 1,000 treasures.",
                Chain = "Hilton / Waldorf Astoria",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasAirportShuttle = true, HasConcierge = true, IsPetFriendly = true,
                Latitude = 41.9147, Longitude = 12.4323,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Rome Cavalieri / Hotel Cavalieri Rome exterior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Rome_Cavalieri_Waldorf_Astoria.jpg/1280px-Rome_Cavalieri_Waldorf_Astoria.jpg",
                    // Wikimedia Commons – View of Rome from Monte Mario
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/Monte_Mario%2C_Rome.jpg/1280px-Monte_Mario%2C_Rome.jpg",
                    // Wikimedia Commons – Panorama of Rome with St Peter's
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Roma_panorama_dal_gianicolo.jpg/1280px-Roma_panorama_dal_gianicolo.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 36, MaxOccupancy = 2, PricePerNight = 380, TotalRooms = 30, AvailableRooms = 12, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant room with Italian furnishings, marble bathroom and access to three outdoor pools and the hilltop gardens." },
                    new() { RoomType = "Rome View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 85, MaxOccupancy = 3, PricePerNight = 950, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasBalcony = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Suite with private balcony and sweeping panoramic views of Rome, the Tiber and St Peter's Basilica." },
                }
            },

            // ── Miami ─────────────────────────────────────────────────────────

            new() {
                Name = "Faena Hotel Miami Beach",
                City = "Miami", Country = "United States", NearestAirportCode = "MIA",
                Address = "3201 Collins Ave, Miami Beach, FL 33140",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 4100,
                Description = "Visionary oceanfront resort on Miami Beach opened 2015, built in the restored 1948 Saxony Hotel. Iconic gilded woolly mammoth skeleton art installation, stunning pool, Tierra Santa healing house spa, and The Theatre restaurant.",
                Chain = "Faena Group",
                Category = HotelCategory.Resort, IsFeatured = false,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasBeachAccess = true, HasAirConditioning = true, HasConcierge = true,
                Latitude = 25.8036, Longitude = -80.1207,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Saxony Hotel (became Faena) Collins Avenue Miami Beach
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Saxony_Hotel%2C_Miami_Beach.jpg/1280px-Saxony_Hotel%2C_Miami_Beach.jpg",
                    // Wikimedia Commons – Miami Beach Collins Avenue
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Miami_Beach_Collins_Ave.jpg/1280px-Miami_Beach_Collins_Ave.jpg",
                    // Wikimedia Commons – Miami Beach ocean front
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a0/Miami_beach_at_sunset.jpg/1280px-Miami_beach_at_sunset.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Ocean View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 48, MaxOccupancy = 2, PricePerNight = 650, TotalRooms = 35, AvailableRooms = 14, HasSeaView = true, HasBalcony = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Luxurious room with Bofill-inspired décor, balcony with direct Atlantic ocean views and hand-crafted furnishings." },
                    new() { RoomType = "Faena Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 130, MaxOccupancy = 4, PricePerNight = 2500, TotalRooms = 8, AvailableRooms = 2, HasBath = true, HasKitchenette = true, HasSeaView = true, HasBalcony = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Opulent suite with panoramic ocean views, separate living and dining rooms, private butler and in-suite jacuzzi." },
                }
            },

            // ── More London ───────────────────────────────────────────────────

            new() {
                Name = "Claridge's",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "Brook Street, Mayfair, London W1K 4HR",
                StarRating = 5, ReviewScore = 9.4m, ReviewCount = 5640,
                Description = "The definitive Art Deco grand hotel, open since 1812 and rebuilt in its iconic 1930s form. The beating heart of Mayfair — legendary haunt of royalty, heads of state and Hollywood A-listers. Davies and Brook restaurant by Daniel Humm (ex-Eleven Madison Park).",
                Chain = "Maybourne Hotel Group",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 51.5120, Longitude = -0.1491,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Claridge's Hotel exterior, Brook Street
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d2/Claridge%27s_Hotel_2007.jpg/1280px-Claridge%27s_Hotel_2007.jpg",
                    // Wikimedia Commons – Mayfair, Brook Street area
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Brook_Street%2C_Mayfair_-_geograph.org.uk_-_1038291.jpg/1280px-Brook_Street%2C_Mayfair_-_geograph.org.uk_-_1038291.jpg",
                    // Wikimedia Commons – New Bond Street / Mayfair streetscape
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Mayfair%2C_London.jpg/1280px-Mayfair%2C_London.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Superior Queen Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 32, MaxOccupancy = 2, PricePerNight = 580, TotalRooms = 50, AvailableRooms = 20, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant Art Deco room with bespoke furnishings, marble bathroom, and a quintessential Claridge's atmosphere." },
                    new() { RoomType = "Deluxe King Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 780, TotalRooms = 30, AvailableRooms = 12, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Gracious king room with Art Deco interiors, Asprey amenities, deep soaking bath and 24-hour butler service." },
                    new() { RoomType = "Mayfair Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 95, MaxOccupancy = 3, PricePerNight = 2200, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Grand suite with a separate sitting room, dining table for six, fireplace and butler service in the Claridge's tradition." },
                }
            },

            new() {
                Name = "The Ritz London",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "150 Piccadilly, St James's, London W1J 9BR",
                StarRating = 5, ReviewScore = 9.5m, ReviewCount = 4120,
                Description = "London's most celebrated hotel since 1906, overlooking Green Park in the heart of St James's. Exquisite Louis XVI gilded interiors, the world-famous Ritz Restaurant, the iconic Palm Court afternoon tea, and a legendary casino.",
                Chain = "Independent",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true, HasParking = false, HasConcierge = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true,
                Latitude = 51.5066, Longitude = -0.1428,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – The Ritz Hotel, Piccadilly London
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fa/The_Ritz_Hotel%2C_London%2C_England.jpg/1280px-The_Ritz_Hotel%2C_London%2C_England.jpg",
                    // Wikimedia Commons – Piccadilly, London
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/43/Piccadilly%2C_London_%28February_2006%29.JPG/1280px-Piccadilly%2C_London_%28February_2006%29.JPG",
                    // Wikimedia Commons – Green Park (opposite The Ritz)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Green_Park_in_London_-_geograph.org.uk_-_1192055.jpg/1280px-Green_Park_in_London_-_geograph.org.uk_-_1192055.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Queen Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 30, MaxOccupancy = 2, PricePerNight = 700, TotalRooms = 45, AvailableRooms = 16, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Beautifully appointed room in the Louis XVI style with hand-woven fabrics, marble bathroom and fresh orchids daily." },
                    new() { RoomType = "Deluxe Garden View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 1100, TotalRooms = 20, AvailableRooms = 8, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Sumptuous room overlooking the private garden or Green Park, with gilded furnishings and a deep marble bath." },
                    new() { RoomType = "Piccadilly Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 130, MaxOccupancy = 4, PricePerNight = 4500, TotalRooms = 6, AvailableRooms = 2, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Magnificent two-bedroom suite with panoramic Piccadilly views, a formal dining room, private butler and Ritz amenity kits." },
                }
            },

            new() {
                Name = "The Grosvenor Hotel",
                City = "London", Country = "United Kingdom", NearestAirportCode = "LHR",
                Address = "101 Buckingham Palace Road, London SW1W 0SJ",
                StarRating = 4, ReviewScore = 8.7m, ReviewCount = 7890,
                Description = "A handsome Victorian railway hotel adjacent to Victoria station, tastefully renovated in 2019. Grand staircase, Florentine restaurant, Hyde Park within walking distance, and exceptional transport links across London and beyond.",
                Chain = "Clermont Hotel Group",
                Category = HotelCategory.Hotel, IsFeatured = false,
                HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 51.4944, Longitude = -0.1441,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – The Grosvenor Hotel, Victoria, London
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9d/Grosvenor_Hotel%2C_London_-_geograph.org.uk_-_868282.jpg/1280px-Grosvenor_Hotel%2C_London_-_geograph.org.uk_-_868282.jpg",
                    // Wikimedia Commons – Victoria Station exterior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/London_Victoria_station_entrance.jpg/1280px-London_Victoria_station_entrance.jpg",
                    // Wikimedia Commons – Buckingham Palace Road
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9b/Buckingham_Palace%2C_London%2C_UK.jpg/1280px-Buckingham_Palace%2C_London%2C_UK.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 28, MaxOccupancy = 2, PricePerNight = 185, TotalRooms = 120, AvailableRooms = 60, IsNonSmoking = true, IncludesBreakfast = false, Description = "Comfortable Victorian-inspired room with modern amenities, power shower and great transport links to all London airports." },
                    new() { RoomType = "Superior King Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 265, TotalRooms = 60, AvailableRooms = 25, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Spacious room with courtyard or street views, bathtub, and upgraded bathroom toiletries. Breakfast included." },
                }
            },

            // ── Edinburgh ─────────────────────────────────────────────────────

            new() {
                Name = "The Balmoral Hotel",
                City = "Edinburgh", Country = "United Kingdom", NearestAirportCode = "EDI",
                Address = "1 Princes Street, Edinburgh EH2 2EQ",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 6320,
                Description = "Edinburgh's grandest hotel, anchoring the east end of Princes Street since 1902. The landmark clock tower (always set 3 minutes fast to help travellers catch their trains), Michelin-starred Number One restaurant, palm court spa, and the suite where J.K. Rowling finished Harry Potter.",
                Chain = "Rocco Forte Hotels",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 55.9527, Longitude = -3.1883,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – The Balmoral Hotel, Edinburgh clock tower
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/The_Balmoral_Hotel%2C_Edinburgh.jpg/1280px-The_Balmoral_Hotel%2C_Edinburgh.jpg",
                    // Wikimedia Commons – Princes Street with Edinburgh Castle beyond
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ab/Princes_Street_Edinburgh.jpg/1280px-Princes_Street_Edinburgh.jpg",
                    // Wikimedia Commons – Edinburgh Castle from Princes Street Gardens
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/Edinburgh_Castle_from_the_south.jpg/1280px-Edinburgh_Castle_from_the_south.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic King Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 30, MaxOccupancy = 2, PricePerNight = 320, TotalRooms = 55, AvailableRooms = 24, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant room in Edwardian grandeur with Scottish thistle motifs, marble bathroom and city or garden views." },
                    new() { RoomType = "Castle View King Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 480, TotalRooms = 25, AvailableRooms = 10, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Premium room with sweeping views of Edinburgh Castle across Princes Street Gardens, the most iconic outlook in Scotland." },
                    new() { RoomType = "Festival Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 100, MaxOccupancy = 3, PricePerNight = 1100, TotalRooms = 8, AvailableRooms = 3, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Opulent suite with panoramic castle and gardens views, separate drawing room, Floris bathroom products and butler service." },
                }
            },

            // ── Manchester ────────────────────────────────────────────────────

            new() {
                Name = "Hotel Gotham",
                City = "Manchester", Country = "United Kingdom", NearestAirportCode = "MAN",
                Address = "100 King Street, Manchester M2 4WU",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 3890,
                Description = "A dazzling Art Deco former bank on King Street, Manchester's most glamorous boutique hotel. 60 uniquely styled rooms and suites with floor-to-ceiling marble bathrooms, the rooftop Club Brass bar with panoramic skyline views, and the dramatic lower-ground Honey restaurant.",
                Chain = "Independent",
                Category = HotelCategory.Boutique, IsFeatured = true,
                HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 53.4801, Longitude = -2.2389,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – King Street, Manchester (the hotel's location)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/King_Street_Manchester.JPG/1280px-King_Street_Manchester.JPG",
                    // Wikimedia Commons – Manchester city centre skyline
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Manchester_from_the_air_%28geograph_5486459%29.jpg/1280px-Manchester_from_the_air_%28geograph_5486459%29.jpg",
                    // Wikimedia Commons – Manchester city central buildings
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/Manchester_England.jpg/1280px-Manchester_England.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "King Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 30, MaxOccupancy = 2, PricePerNight = 220, TotalRooms = 30, AvailableRooms = 14, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Art Deco-inspired room with floor-to-ceiling marble bathroom, Harvey Nichols toiletries and bespoke king bed." },
                    new() { RoomType = "Gotham Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 65, MaxOccupancy = 3, PricePerNight = 550, TotalRooms = 8, AvailableRooms = 3, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Spectacular suite with city views, freestanding copper bath, double walk-in shower and in-room cocktail cabinet." },
                }
            },

            // ── Scotland Resort ───────────────────────────────────────────────

            new() {
                Name = "Gleneagles",
                City = "Auchterarder", Country = "United Kingdom", NearestAirportCode = "EDI",
                Address = "Auchterarder, Perthshire PH3 1NF",
                StarRating = 5, ReviewScore = 9.3m, ReviewCount = 8450,
                Description = "Scotland's most legendary resort, set in 850 acres of Perthshire countryside. Three championship golf courses (including the Ryder Cup venue), an equestrian centre, falconry, Michelin-starred Andrew Fairlie restaurant, and a world-class spa. The definitive Scottish country-house experience.",
                Chain = "Ennismore",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true, IsPetFriendly = true, HasParking = true,
                Latitude = 56.2739, Longitude = -3.7486,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Gleneagles Hotel and golf courses, Perthshire
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Gleneagles_Hotel_and_Golf_Courses.jpg/1280px-Gleneagles_Hotel_and_Golf_Courses.jpg",
                    // Wikimedia Commons – Gleneagles Hotel front facade
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Gleneagles_Hotel.jpg/1280px-Gleneagles_Hotel.jpg",
                    // Wikimedia Commons – Perthshire hills landscape
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Perthshire_landscape.jpg/1280px-Perthshire_landscape.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 450, TotalRooms = 80, AvailableRooms = 35, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Elegant country-house room with rolling Perthshire views, Scottish wool throws and access to all resort facilities." },
                    new() { RoomType = "Golf View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 45, MaxOccupancy = 2, PricePerNight = 650, TotalRooms = 40, AvailableRooms = 18, HasBath = true, HasBalcony = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Superior room with private balcony overlooking the King's Course fairways with moorland and mountain backdrop." },
                    new() { RoomType = "Gleneagles Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 120, MaxOccupancy = 4, PricePerNight = 1800, TotalRooms = 12, AvailableRooms = 5, HasBath = true, HasBalcony = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Palatial suite with separate sitting room, double bedroom, terrace with panoramic course and moorland views and butler service." },
                }
            },

            // ── Berlin ────────────────────────────────────────────────────────

            new() {
                Name = "Hotel Adlon Kempinski",
                City = "Berlin", Country = "Germany", NearestAirportCode = "BER",
                Address = "Unter den Linden 77, 10117 Berlin",
                StarRating = 5, ReviewScore = 9.1m, ReviewCount = 7820,
                Description = "Berlin's most iconic grand hotel, directly beside the Brandenburg Gate since 1907 (rebuilt 1997). Legendary haunt of Michael Jackson, Charlie Chaplin and countless heads of state. Michelin-starred Lorenz Adlon Esszimmer, Adlon Spa and unrivalled views of the Gate.",
                Chain = "Kempinski",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 52.5163, Longitude = 13.3777,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Hotel Adlon Kempinski Berlin exterior
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/18/Hotel_Adlon_Kempinski_Berlin_2012.jpg/1280px-Hotel_Adlon_Kempinski_Berlin_2012.jpg",
                    // Wikimedia Commons – Brandenburg Gate illuminated at night
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Brandenburger_Tor_abends.jpg/1280px-Brandenburger_Tor_abends.jpg",
                    // Wikimedia Commons – Unter den Linden boulevard, Berlin
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/Unter_den_Linden_Berlin.jpg/1280px-Unter_den_Linden_Berlin.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Deluxe Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 420, TotalRooms = 70, AvailableRooms = 30, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Elegant room with warm wood panelling, marble bathroom, Kempinski Duck Down duvet and premium city views." },
                    new() { RoomType = "Brandenburg Gate View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 90, MaxOccupancy = 3, PricePerNight = 1400, TotalRooms = 10, AvailableRooms = 4, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Suite with unobstructed floor-to-ceiling views of the Brandenburg Gate, walk-in closet and Kempinski butler service." },
                }
            },

            // ── Vienna ────────────────────────────────────────────────────────

            new() {
                Name = "Hotel Sacher Wien",
                City = "Vienna", Country = "Austria", NearestAirportCode = "VIE",
                Address = "Philharmoniker Strasse 4, 1010 Vienna",
                StarRating = 5, ReviewScore = 9.2m, ReviewCount = 9640,
                Description = "Vienna's most legendary hotel, open since 1876 and birthplace of the world-famous Sachertorte. Directly opposite the Vienna State Opera. Exquisite red-velvet interiors adorned with original paintings, the atmospheric Red Bar, and the original Café Sacher serving 1,000+ Sachertorte daily.",
                Chain = "Sacher Hotels",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 48.2030, Longitude = 16.3695,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Hotel Sacher Wien, Philharmoniker Strasse
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8e/Hotel_Sacher_Wien_2008.jpg/1280px-Hotel_Sacher_Wien_2008.jpg",
                    // Wikimedia Commons – Vienna State Opera (directly opposite)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/Wiener_Staatsoper_bei_Nacht.jpg/1280px-Wiener_Staatsoper_bei_Nacht.jpg",
                    // Wikimedia Commons – Vienna historic centre
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ea/Vienna_-_Panorama.jpg/1280px-Vienna_-_Panorama.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 28, MaxOccupancy = 2, PricePerNight = 380, TotalRooms = 60, AvailableRooms = 28, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Warmly furnished room with red velvet accents, original oil paintings and the distinctive Sacher ambiance." },
                    new() { RoomType = "Superior Opera View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 38, MaxOccupancy = 2, PricePerNight = 620, TotalRooms = 20, AvailableRooms = 9, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Premium room overlooking the illuminated Vienna State Opera House, with hand-carved furniture and marble bathroom." },
                    new() { RoomType = "Sacher Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 85, MaxOccupancy = 3, PricePerNight = 1650, TotalRooms = 8, AvailableRooms = 3, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Magnificent suite with Biedermeier antiques, grand drawing room, butler service and sweeping Opera House views." },
                }
            },

            // ── Venice ────────────────────────────────────────────────────────

            new() {
                Name = "Belmond Hotel Cipriani",
                City = "Venice", Country = "Italy", NearestAirportCode = "VCE",
                Address = "Giudecca 10, 30133 Venice",
                StarRating = 5, ReviewScore = 9.6m, ReviewCount = 4250,
                Description = "Venice's most celebrated hotel, hidden on the island of Giudecca since 1958, a three-minute private launch from Piazza San Marco. 95 rooms and suites set in a former monastic garden, the only heated pool in Venice, Michelin-starred Oro restaurant, and a private pasta-making school.",
                Chain = "Belmond",
                Category = HotelCategory.Hotel, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 45.4255, Longitude = 12.3354,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Venice Grand Canal with gondolas
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/30/Gondola_canal_grande.jpg/1280px-Gondola_canal_grande.jpg",
                    // Wikimedia Commons – Giudecca island, Venice
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/Giudecca_Venezia.jpg/1280px-Giudecca_Venezia.jpg",
                    // Wikimedia Commons – Venice at sunset from the lagoon
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ec/Venice_-_Venezia_-_panoramio_%2841%29.jpg/1280px-Venice_-_Venezia_-_panoramio_%2841%29.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Garden Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 32, MaxOccupancy = 2, PricePerNight = 750, TotalRooms = 30, AvailableRooms = 12, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Serene room overlooking the monastery gardens with Venetian stucco walls, Fortuny fabrics and garden pool access." },
                    new() { RoomType = "Lagoon View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 1100, TotalRooms = 20, AvailableRooms = 8, HasBath = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Spacious room with sweeping views across the Venetian lagoon to San Marco and the Doge's Palace skyline." },
                    new() { RoomType = "Palladio Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 110, MaxOccupancy = 3, PricePerNight = 3200, TotalRooms = 6, AvailableRooms = 2, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Grand lagoon-facing suite in the historic Palladio wing with private terrace, butler service and panoramic lagoon views." },
                }
            },

            // ── Lake Como ─────────────────────────────────────────────────────

            new() {
                Name = "Grand Hotel Tremezzo",
                City = "Tremezzo", Country = "Italy", NearestAirportCode = "MXP",
                Address = "Via Regina 8, 22019 Tremezzo CO, Italy",
                StarRating = 5, ReviewScore = 9.4m, ReviewCount = 3180,
                Description = "A magnificent Belle Époque palace on the western shore of Lake Como since 1910, with mesmerising views of Villa Carlotta and the Alpine peaks beyond. Three pools (including a floating lake pool), a celebrated wine cellar, private beach and boat dock, and the La Terrazza restaurant.",
                Chain = "Independent",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true, HasBeachAccess = true, HasParking = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 45.9873, Longitude = 9.2262,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Lake Como, Tremezzo, looking to Villa Carlotta
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/Lago_di_Como_Tremezzo.JPG/1280px-Lago_di_Como_Tremezzo.JPG",
                    // Wikimedia Commons – Lake Como panoramic view
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/1a/LakeComo.jpg/1280px-LakeComo.jpg",
                    // Wikimedia Commons – Lake Como Bellagio area with mountains
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/ef/Bellagio_Lake_Como.jpg/1280px-Bellagio_Lake_Como.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Lake View Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 35, MaxOccupancy = 2, PricePerNight = 520, TotalRooms = 40, AvailableRooms = 18, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Elegant room with private balcony and breathtaking lake views framed by Alpine peaks and Mediterranean gardens." },
                    new() { RoomType = "Grand Suite Lake View", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 90, MaxOccupancy = 3, PricePerNight = 1600, TotalRooms = 8, AvailableRooms = 3, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Palatial belle époque suite with panoramic Como views, separate sitting room, antique furnishings and a floating pool pass." },
                }
            },

            // ── French Riviera ────────────────────────────────────────────────

            new() {
                Name = "Hôtel du Cap-Eden-Roc",
                City = "Cap d'Antibes", Country = "France", NearestAirportCode = "NCE",
                Address = "Boulevard J.F. Kennedy, 06601 Antibes",
                StarRating = 5, ReviewScore = 9.5m, ReviewCount = 2890,
                Description = "The pinnacle of the French Riviera since 1870, a legendary clifftop retreat on Cap d'Antibes where F. Scott Fitzgerald wrote Tender is the Night. No key cards — each guest has an actual key. The iconic salt-water pool hewn from the rocks, spectacular cliff-edge dining at Eden-Roc Restaurant, and absolute privacy.",
                Chain = "Oetker Collection",
                Category = HotelCategory.Resort, IsFeatured = true,
                HasPool = true, HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true, HasBeachAccess = true, HasTennis = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 43.5468, Longitude = 7.1282,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Cap d'Antibes peninsula, Côte d'Azur
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/53/Cap_d%27Antibes.jpg/1280px-Cap_d%27Antibes.jpg",
                    // Wikimedia Commons – French Riviera / Nice coast from above
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Nice_airport_and_Promenade_des_Anglais_aerial_view.jpg/1280px-Nice_airport_and_Promenade_des_Anglais_aerial_view.jpg",
                    // Wikimedia Commons – Antibes old town and bay
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/7/71/Antibes_-_panoramio_%286%29.jpg/1280px-Antibes_-_panoramio_%286%29.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 40, MaxOccupancy = 2, PricePerNight = 1200, TotalRooms = 50, AvailableRooms = 18, HasBath = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Elegant Provençal room in the Belle Époque main building with sea or garden views and access to all cliff-top facilities." },
                    new() { RoomType = "Eden-Roc Cabana Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 75, MaxOccupancy = 3, PricePerNight = 3500, TotalRooms = 12, AvailableRooms = 4, HasBath = true, HasBalcony = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Iconic poolside suite with private terrace, direct access to the rock-hewn seawater pool and uninterrupted Mediterranean views." },
                }
            },

            // ── Brussels ──────────────────────────────────────────────────────

            new() {
                Name = "Hotel Amigo",
                City = "Brussels", Country = "Belgium", NearestAirportCode = "BRU",
                Address = "Rue de l'Amigo 1-3, 1000 Brussels",
                StarRating = 5, ReviewScore = 9.0m, ReviewCount = 4580,
                Description = "Brussels's most distinguished luxury hotel, steps from the Grand Place — a UNESCO World Heritage Site and one of the world's most beautiful squares. Spanish Renaissance-inspired architecture, the celebrated Ristorante Bocconi, and a striking Tintin-themed bar.",
                Chain = "Rocco Forte Hotels",
                Category = HotelCategory.Hotel, IsFeatured = false,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true,
                Latitude = 50.8458, Longitude = 4.3497,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Grand Place, Brussels (directly adjacent)
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Grand_place_brussels.jpg/1280px-Grand_place_brussels.jpg",
                    // Wikimedia Commons – Brussels Town Hall, Grand Place
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Brussels-Grand_Place_04.jpg/1280px-Brussels-Grand_Place_04.jpg",
                    // Wikimedia Commons – Brussels city panorama
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/85/Brussels_panorama.jpg/1280px-Brussels_panorama.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Superior Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 32, MaxOccupancy = 2, PricePerNight = 290, TotalRooms = 55, AvailableRooms = 24, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Refined room with rich Flemish tapestries, black-granite bathroom and a location moments from the Grand Place." },
                    new() { RoomType = "Grand Place View Suite", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 70, MaxOccupancy = 3, PricePerNight = 750, TotalRooms = 6, AvailableRooms = 2, HasBath = true, HasCityView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Rare suite with a private balcony overlooking the Grand Place, Brussels's gilded medieval masterpiece." },
                }
            },

            // ── Geneva ────────────────────────────────────────────────────────

            new() {
                Name = "Beau-Rivage Geneva",
                City = "Geneva", Country = "Switzerland", NearestAirportCode = "GVA",
                Address = "Quai du Mont-Blanc 13, 1201 Geneva",
                StarRating = 5, ReviewScore = 9.3m, ReviewCount = 5120,
                Description = "Geneva's most storied grand hotel, open since 1865 on the shores of Lake Geneva. Legendary guests include Empress Sissi, who received the first guests, and Richard Wagner. The restaurant Le Chat-Botté holds two Michelin stars; the terrace affords breathtaking views of the Jet d'Eau and Mont Blanc.",
                Chain = "Independent (Mayer family since 1865)",
                Category = HotelCategory.Hotel, IsFeatured = false,
                HasSpa = true, HasGym = true, HasRestaurant = true, HasBar = true,
                HasFreeWifi = true, HasAirConditioning = true, HasRoomService = true, HasConcierge = true, HasAirportShuttle = true,
                Latitude = 46.2068, Longitude = 6.1506,
                ImagesJson = JsonSerializer.Serialize(new[]
                {
                    // Wikimedia Commons – Beau-Rivage Geneva lakeside facade
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Beau-Rivage_Geneva.jpg/1280px-Beau-Rivage_Geneva.jpg",
                    // Wikimedia Commons – Lake Geneva with Jet d'Eau and Alps
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Lac_L%C3%A9man_et_le_jet_d%27eau%2C_Genf%2C_Suiza.jpg/1280px-Lac_L%C3%A9man_et_le_jet_d%27eau%2C_Genf%2C_Suiza.jpg",
                    // Wikimedia Commons – Geneva lake panorama with Mont Blanc
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/Geneva_rade.jpg/1280px-Geneva_rade.jpg"
                }),
                Rooms = new List<HotelRoom>
                {
                    new() { RoomType = "Classic Room", BedType = BedType.Queen, BedCount = 1, SizeSquareMetres = 30, MaxOccupancy = 2, PricePerNight = 480, TotalRooms = 40, AvailableRooms = 16, HasBath = true, IsNonSmoking = true, IncludesBreakfast = false, Description = "Tasteful room with hand-embroidered Swiss linens, marble bathroom and a choice of lake or city views." },
                    new() { RoomType = "Lake View Deluxe Room", BedType = BedType.King, BedCount = 1, SizeSquareMetres = 42, MaxOccupancy = 2, PricePerNight = 780, TotalRooms = 20, AvailableRooms = 9, HasBath = true, HasSeaView = true, IsNonSmoking = true, IncludesBreakfast = true, Description = "Stunning room with full-width lake views, the Jet d'Eau and Alpine peaks as the backdrop. Includes breakfast." },
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

        Hotel? GetHotel(string name) => hotels.FirstOrDefault(h => h.Name.Contains(name));

        var packages = new List<Package>();

        if (airports.ContainsKey("LHR") && airports.ContainsKey("CDG") && GetHotel("Meurice") is Hotel parisHotel)
            packages.Add(new Package {
                Name = "Paris City Break", Description = "4 nights in the heart of Paris with return flights from London Heathrow, including breakfast at Le Meurice.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["CDG"],
                HotelId = parisHotel.Id, Nights = 4, BasePrice = 799, OriginalPrice = 1050,
                IncludesBreakfast = true, Tags = "City break", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("BCN") && GetHotel("Hotel Arts") is Hotel bcnHotel)
            packages.Add(new Package {
                Name = "Barcelona Beach & City", Description = "7 nights in Barcelona with sea views at Hotel Arts, return flights from London and airport transfers.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["BCN"],
                HotelId = bcnHotel.Id, Nights = 7, BasePrice = 1099, OriginalPrice = 1450,
                IncludesTransfers = true, Tags = "Beach", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("DXB") && GetHotel("Burj Al Arab") is Hotel dubaiHotel)
            packages.Add(new Package {
                Name = "Dubai Luxury Escape", Description = "5 nights at the Burj Al Arab, the world's most luxurious hotel, with return business class flights from Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["DXB"],
                HotelId = dubaiHotel.Id, Nights = 5, BasePrice = 5999, OriginalPrice = 7500,
                IncludesBreakfast = true, IncludesTransfers = true, Tags = "Luxury", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("JFK") && GetHotel("Plaza Hotel") is Hotel nyHotel)
            packages.Add(new Package {
                Name = "New York City Package", Description = "6 nights at The Plaza Hotel, return flights from London Heathrow, and breakfast included.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["JFK"],
                HotelId = nyHotel.Id, Nights = 6, BasePrice = 2299, OriginalPrice = 2900,
                IncludesBreakfast = true, Tags = "City break", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("SIN") && GetHotel("Raffles") is Hotel rafflesHotel)
            packages.Add(new Package {
                Name = "Singapore Colonial Escape", Description = "5 nights at the legendary Raffles Singapore with return flights from London Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["SIN"],
                HotelId = rafflesHotel.Id, Nights = 5, BasePrice = 2899, OriginalPrice = 3600,
                IncludesBreakfast = true, IncludesTransfers = false, Tags = "Luxury", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("FCO") && GetHotel("Cavalieri") is Hotel romeHotel)
            packages.Add(new Package {
                Name = "Rome Luxury Weekend", Description = "4 nights at the Rome Cavalieri Waldorf Astoria with panoramic city views, return flights and airport transfers.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["FCO"],
                HotelId = romeHotel.Id, Nights = 4, BasePrice = 1299, OriginalPrice = 1700,
                IncludesBreakfast = false, IncludesTransfers = true, Tags = "City break,Luxury", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("EDI") && GetHotel("Balmoral") is Hotel balmoral)
            packages.Add(new Package {
                Name = "Edinburgh City Break", Description = "4 nights at The Balmoral Hotel, Edinburgh's finest address on Princes Street, with return flights from London Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["EDI"],
                HotelId = balmoral.Id, Nights = 4, BasePrice = 699, OriginalPrice = 950,
                IncludesBreakfast = true, Tags = "City break,Scotland", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("VIE") && GetHotel("Sacher") is Hotel sacher)
            packages.Add(new Package {
                Name = "Vienna Operatic Escape", Description = "5 nights at the legendary Hotel Sacher Wien opposite the Vienna State Opera, with return flights from London Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["VIE"],
                HotelId = sacher.Id, Nights = 5, BasePrice = 1299, OriginalPrice = 1700,
                IncludesBreakfast = true, Tags = "City break,Luxury", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("VCE") && GetHotel("Cipriani") is Hotel cipriani)
            packages.Add(new Package {
                Name = "Venice Luxury Retreat", Description = "4 nights at Belmond Hotel Cipriani on the island of Giudecca — the most exclusive address in Venice — with return flights from London.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["VCE"],
                HotelId = cipriani.Id, Nights = 4, BasePrice = 2499, OriginalPrice = 3200,
                IncludesBreakfast = true, IncludesTransfers = true, Tags = "Luxury,City break", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("MXP") && GetHotel("Tremezzo") is Hotel tremezzo)
            packages.Add(new Package {
                Name = "Lake Como Belle Époque", Description = "6 nights at the Grand Hotel Tremezzo on the shores of Lake Como, with return flights from London Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["MXP"],
                HotelId = tremezzo.Id, Nights = 6, BasePrice = 1899, OriginalPrice = 2500,
                IncludesBreakfast = true, Tags = "Luxury,Beach", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("NCE") && GetHotel("Cap-Eden-Roc") is Hotel capEden)
            packages.Add(new Package {
                Name = "French Riviera Retreat", Description = "5 nights at the legendary Hôtel du Cap-Eden-Roc on Cap d'Antibes, with return flights from London to Nice.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["NCE"],
                HotelId = capEden.Id, Nights = 5, BasePrice = 4999, OriginalPrice = 6500,
                IncludesBreakfast = true, IncludesTransfers = true, Tags = "Luxury,Beach", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("BER") && GetHotel("Adlon") is Hotel adlon)
            packages.Add(new Package {
                Name = "Berlin Brandenburg Break", Description = "4 nights at Hotel Adlon Kempinski next to the Brandenburg Gate, with return flights from London Heathrow.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["BER"],
                HotelId = adlon.Id, Nights = 4, BasePrice = 899, OriginalPrice = 1200,
                IncludesBreakfast = true, Tags = "City break", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("GVA") && GetHotel("Beau-Rivage") is Hotel beauRivage)
            packages.Add(new Package {
                Name = "Geneva Lakeside Luxury", Description = "3 nights at the Beau-Rivage Geneva on Lake Geneva with views of the Jet d'Eau and Mont Blanc, return flights included.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["GVA"],
                HotelId = beauRivage.Id, Nights = 3, BasePrice = 1099, OriginalPrice = 1450,
                IncludesBreakfast = true, Tags = "City break,Luxury", IsFeatured = false, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (airports.ContainsKey("LHR") && airports.ContainsKey("EDI") && GetHotel("Gleneagles") is Hotel gleneagles)
            packages.Add(new Package {
                Name = "Scottish Highland Resort", Description = "4 nights at Gleneagles in Perthshire — three championship golf courses, a world-class spa and Michelin dining — with return flights to Edinburgh.",
                OriginAirportId = airports["LHR"], DestinationAirportId = airports["EDI"],
                HotelId = gleneagles.Id, Nights = 4, BasePrice = 1499, OriginalPrice = 2000,
                IncludesBreakfast = true, IncludesTransfers = true, Tags = "Luxury,Golf,Scotland", IsFeatured = true, IsActive = true,
                ValidFrom = DateTime.Today, ValidTo = DateTime.Today.AddMonths(6)
            });

        if (packages.Any())
        {
            _db.Packages.AddRange(packages);
            await _db.SaveChangesAsync();
        }
    }
}
