using BaRemake.Shared.Enums;
using BaRemake.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Data.Seeding;

public class DataSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        await _db.Database.MigrateAsync();
        await SeedRolesAsync();
        await SeedAirlinesAsync();
        await SeedAirportsAsync();
        await SeedAircraftAsync();
        await SeedRoutesAsync();
        await SeedFlightsAsync();
        await SeedUsersAsync();
    }

    // ── Roles ──────────────────────────────────────────────────────────────

    private async Task SeedRolesAsync()
    {
        foreach (var role in new[] { "Admin", "Manager", "TravelAgent", "Customer" })
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // ── Airlines ──────────────────────────────────────────────────────────

    private async Task SeedAirlinesAsync()
    {
        if (await _db.Airlines.AnyAsync()) return;

        _db.Airlines.AddRange(new[]
        {
            new Airline { IATACode = "BA", ICAOCode = "BAW", Name = "British Airways",
                Country = "United Kingdom", Hub = "LHR",
                BrandColor = "#002157", BrandColorSecondary = "#c6a84b",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/42/British_Airways_Logo.svg/200px-British_Airways_Logo.svg.png",
                CabinBagPolicy = "1 cabin bag (56x45x25cm) + 1 personal item",
                HoldBagPolicy = "23kg (Economy Light: none)" },

            new Airline { IATACode = "U2", ICAOCode = "EZY", Name = "easyJet",
                Country = "United Kingdom", Hub = "LGW",
                BrandColor = "#FF6600", BrandColorSecondary = "#ffffff",
                Type = AirlineType.LowCost,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5a/Easyjet_logo.svg/200px-Easyjet_logo.svg.png",
                CabinBagPolicy = "1 small bag under seat (45x36x20cm) free; overhead bag with FLEXI only",
                HoldBagPolicy = "From £7.99 per bag, 15-32kg" },

            new Airline { IATACode = "FR", ICAOCode = "RYR", Name = "Ryanair",
                Country = "Ireland", Hub = "DUB",
                BrandColor = "#073590", BrandColorSecondary = "#f9c619",
                Type = AirlineType.UltraLowCost,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Ryanair_Logo.svg/200px-Ryanair_Logo.svg.png",
                CabinBagPolicy = "1 small bag (40x25x20cm) under seat. Overhead cabin bag with Priority.",
                HoldBagPolicy = "From £/€6 for 10-20kg" },

            new Airline { IATACode = "VS", ICAOCode = "VIR", Name = "Virgin Atlantic",
                Country = "United Kingdom", Hub = "LHR",
                BrandColor = "#D91C26", BrandColorSecondary = "#ffffff",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/Virgin_Atlantic_logo.svg/200px-Virgin_Atlantic_logo.svg.png",
                CabinBagPolicy = "1 cabin bag (56x36x23cm) + 1 personal item",
                HoldBagPolicy = "23kg (Economy) / 30kg (Premium) / 2×32kg (Upper Class)" },

            new Airline { IATACode = "LH", ICAOCode = "DLH", Name = "Lufthansa",
                Country = "Germany", Hub = "FRA",
                BrandColor = "#05164d", BrandColorSecondary = "#ffc900",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/Lufthansa_Logo_2018.svg/200px-Lufthansa_Logo_2018.svg.png",
                CabinBagPolicy = "1 cabin bag (55x40x23cm) + 1 personal item",
                HoldBagPolicy = "23kg included in most fares" },

            new Airline { IATACode = "AF", ICAOCode = "AFR", Name = "Air France",
                Country = "France", Hub = "CDG",
                BrandColor = "#002157", BrandColorSecondary = "#CD0000",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/44/Air_France_Logo.svg/200px-Air_France_Logo.svg.png",
                CabinBagPolicy = "1 cabin bag (55x35x25cm) + 1 personal item",
                HoldBagPolicy = "23kg included in all fares" },

            new Airline { IATACode = "EK", ICAOCode = "UAE", Name = "Emirates",
                Country = "United Arab Emirates", Hub = "DXB",
                BrandColor = "#D71A21", BrandColorSecondary = "#C6932B",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/200px-Emirates_logo.svg.png",
                CabinBagPolicy = "1 cabin bag (55x38x20cm) + 1 personal item",
                HoldBagPolicy = "30kg (Economy) / 40kg (Business) / 50kg (First)" },

            new Airline { IATACode = "IB", ICAOCode = "IBE", Name = "Iberia",
                Country = "Spain", Hub = "MAD",
                BrandColor = "#C8102E", BrandColorSecondary = "#F0A500",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Iberia_logo.svg/200px-Iberia_logo.svg.png",
                CabinBagPolicy = "1 cabin bag (56x45x25cm) + 1 personal item",
                HoldBagPolicy = "23kg (from Economy Standard)" },

            new Airline { IATACode = "KL", ICAOCode = "KLM", Name = "KLM Royal Dutch Airlines",
                Country = "Netherlands", Hub = "AMS",
                BrandColor = "#00a1de", BrandColorSecondary = "#ffffff",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/KLM_logo.svg/200px-KLM_logo.svg.png",
                CabinBagPolicy = "1 cabin bag (55x35x25cm) + 1 personal item",
                HoldBagPolicy = "23kg included in most fares" },

            new Airline { IATACode = "W6", ICAOCode = "WZZ", Name = "Wizz Air",
                Country = "Hungary", Hub = "BUD",
                BrandColor = "#C6007E", BrandColorSecondary = "#ffffff",
                Type = AirlineType.UltraLowCost,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/63/Wizz_Air_logo.svg/200px-Wizz_Air_logo.svg.png",
                CabinBagPolicy = "1 small bag (40x30x20cm) under seat. WIZZ Priority: overhead bag.",
                HoldBagPolicy = "From £12 for 10-32kg" },

            new Airline { IATACode = "TP", ICAOCode = "TAP", Name = "TAP Air Portugal",
                Country = "Portugal", Hub = "LIS",
                BrandColor = "#006600", BrandColorSecondary = "#C8102E",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/79/TAP_Air_Portugal_logo.svg/200px-TAP_Air_Portugal_logo.svg.png",
                CabinBagPolicy = "1 cabin bag (55x40x20cm) + 1 personal item",
                HoldBagPolicy = "23kg (from Light Plus)" },

            new Airline { IATACode = "SQ", ICAOCode = "SIA", Name = "Singapore Airlines",
                Country = "Singapore", Hub = "SIN",
                BrandColor = "#1A1F5F", BrandColorSecondary = "#e6a817",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Singapore_Airlines_logo_2.svg/200px-Singapore_Airlines_logo_2.svg.png",
                CabinBagPolicy = "1 cabin bag (55x38x20cm) + 1 personal item",
                HoldBagPolicy = "30kg (Economy) / 40kg (Business/First)" },

            new Airline { IATACode = "AA", ICAOCode = "AAL", Name = "American Airlines",
                Country = "United States", Hub = "DFW",
                BrandColor = "#003366", BrandColorSecondary = "#C8102E",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/23/American_Airlines_logo_2013.svg/200px-American_Airlines_logo_2013.svg.png",
                CabinBagPolicy = "1 cabin bag (56x36x23cm) + 1 personal item",
                HoldBagPolicy = "1st bag from $30, 23kg" },

            new Airline { IATACode = "UA", ICAOCode = "UAL", Name = "United Airlines",
                Country = "United States", Hub = "ORD",
                BrandColor = "#005DAA", BrandColorSecondary = "#ffffff",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/United_Airlines_Logo.svg/200px-United_Airlines_Logo.svg.png",
                CabinBagPolicy = "1 cabin bag + 1 personal item",
                HoldBagPolicy = "1st bag $35, 23kg" },

            new Airline { IATACode = "TK", ICAOCode = "THY", Name = "Turkish Airlines",
                Country = "Turkey", Hub = "IST",
                BrandColor = "#C8102E", BrandColorSecondary = "#ffffff",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5d/Turkish_Airlines_Logo.svg/200px-Turkish_Airlines_Logo.svg.png",
                CabinBagPolicy = "1 cabin bag (55x40x23cm) + 1 personal item",
                HoldBagPolicy = "20-30kg included based on route" },

            new Airline { IATACode = "QR", ICAOCode = "QTR", Name = "Qatar Airways",
                Country = "Qatar", Hub = "DOH",
                BrandColor = "#5C0D34", BrandColorSecondary = "#c4a04e",
                Type = AirlineType.FullService,
                LogoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/52/Qatar_Airways_Logo.svg/200px-Qatar_Airways_Logo.svg.png",
                CabinBagPolicy = "1 cabin bag (50x37x25cm) + 1 personal item",
                HoldBagPolicy = "23kg (Economy) / 30kg (Business)" },
        });

        await _db.SaveChangesAsync();
    }

    // ── Users ─────────────────────────────────────────────────────────────

    private async Task SeedUsersAsync()
    {
        // Admin
        if (await _userManager.FindByEmailAsync("admin@baremake.com") == null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@baremake.com",
                Email = "admin@baremake.com",
                FirstName = "System",
                LastName = "Administrator",
                Title = "Mr",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = new DateTime(1985, 1, 1),
                IsActive = true
            };
            var result = await _userManager.CreateAsync(admin, "Admin123!");
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(admin, new[] { "Admin", "Manager", "Customer" });
                await AddUserClaims(_userManager, admin, isAdmin: true);
            }
        }

        // Sample customers
        var customers = new[]
        {
            ("john.smith@example.com", "John", "Smith", "Mr", new DateTime(1988, 3, 15)),
            ("sarah.jones@example.com", "Sarah", "Jones", "Mrs", new DateTime(1992, 7, 22)),
            ("james.brown@example.com", "James", "Brown", "Mr", new DateTime(1975, 11, 8)),
            ("emma.wilson@example.com", "Emma", "Wilson", "Ms", new DateTime(1995, 4, 30)),
            ("oliver.taylor@example.com", "Oliver", "Taylor", "Mr", new DateTime(1983, 9, 12)),
            ("sophie.clark@example.com", "Sophie", "Clark", "Ms", new DateTime(1998, 2, 14)),
            ("william.martin@example.com", "William", "Martin", "Mr", new DateTime(1970, 6, 5)),
        };

        foreach (var (email, first, last, title, dob) in customers)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = first,
                    LastName = last,
                    Title = title,
                    EmailConfirmed = true,
                    DateOfBirth = dob,
                    IsActive = true,
                    PhoneNumber = "+44 7700 900000"
                };
                var result = await _userManager.CreateAsync(user, "Customer123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    await AddUserClaims(_userManager, user, isAdmin: false);
                }
            }
        }
    }

    private static async Task AddUserClaims(UserManager<ApplicationUser> um, ApplicationUser u, bool isAdmin)
    {
        await um.AddClaimsAsync(u, new[]
        {
            new System.Security.Claims.Claim("FullName", u.FullName),
            new System.Security.Claims.Claim("FirstName", u.FirstName),
            new System.Security.Claims.Claim("LastName", u.LastName),
            new System.Security.Claims.Claim("IsAdmin", isAdmin.ToString().ToLower()),
        });
    }

    // ── Airports ──────────────────────────────────────────────────────────

    private async Task SeedAirportsAsync()
    {
        if (await _db.Airports.AnyAsync()) return;

        var airports = new[]
        {
            // UK
            ("LHR", "Heathrow Airport", "London", "United Kingdom", "GB", "Europe/London", 51.4775, -0.4614),
            ("LGW", "Gatwick Airport", "London", "United Kingdom", "GB", "Europe/London", 51.1481, -0.1903),
            ("STN", "Stansted Airport", "London", "United Kingdom", "GB", "Europe/London", 51.8850, 0.2350),
            ("MAN", "Manchester Airport", "Manchester", "United Kingdom", "GB", "Europe/London", 53.3650, -2.2725),
            ("EDI", "Edinburgh Airport", "Edinburgh", "United Kingdom", "GB", "Europe/London", 55.9500, -3.3725),
            ("BHX", "Birmingham Airport", "Birmingham", "United Kingdom", "GB", "Europe/London", 52.4539, -1.7480),
            ("GLA", "Glasgow Airport", "Glasgow", "United Kingdom", "GB", "Europe/London", 55.8642, -4.4330),
            ("BRS", "Bristol Airport", "Bristol", "United Kingdom", "GB", "Europe/London", 51.3827, -2.7190),
            // Europe
            ("CDG", "Charles de Gaulle Airport", "Paris", "France", "FR", "Europe/Paris", 49.0097, 2.5479),
            ("ORY", "Orly Airport", "Paris", "France", "FR", "Europe/Paris", 48.7262, 2.3652),
            ("AMS", "Schiphol Airport", "Amsterdam", "Netherlands", "NL", "Europe/Amsterdam", 52.3105, 4.7683),
            ("FRA", "Frankfurt Airport", "Frankfurt", "Germany", "DE", "Europe/Berlin", 50.0379, 8.5622),
            ("MUC", "Munich Airport", "Munich", "Germany", "DE", "Europe/Berlin", 48.3537, 11.7860),
            ("MAD", "Barajas Airport", "Madrid", "Spain", "ES", "Europe/Madrid", 40.4936, -3.5668),
            ("BCN", "El Prat Airport", "Barcelona", "Spain", "ES", "Europe/Madrid", 41.2971, 2.0785),
            ("PMI", "Palma de Mallorca Airport", "Palma", "Spain", "ES", "Europe/Madrid", 39.5517, 2.7388),
            ("AGP", "Málaga Airport", "Malaga", "Spain", "ES", "Europe/Madrid", 36.6749, -4.4991),
            ("FCO", "Leonardo da Vinci Airport", "Rome", "Italy", "IT", "Europe/Rome", 41.8003, 12.2389),
            ("MXP", "Malpensa Airport", "Milan", "Italy", "IT", "Europe/Rome", 45.6306, 8.7281),
            ("VCE", "Marco Polo Airport", "Venice", "Italy", "IT", "Europe/Rome", 45.5053, 12.3519),
            ("ATH", "Athens International Airport", "Athens", "Greece", "GR", "Europe/Athens", 37.9364, 23.9445),
            ("HER", "Heraklion Airport", "Heraklion", "Greece", "GR", "Europe/Athens", 35.3397, 25.1803),
            ("RHO", "Rhodes Airport", "Rhodes", "Greece", "GR", "Europe/Athens", 36.4054, 28.0862),
            ("PRG", "Václav Havel Airport", "Prague", "Czech Republic", "CZ", "Europe/Prague", 50.1008, 14.2600),
            ("BUD", "Budapest Airport", "Budapest", "Hungary", "HU", "Europe/Budapest", 47.4298, 19.2611),
            ("WAW", "Warsaw Chopin Airport", "Warsaw", "Poland", "PL", "Europe/Warsaw", 52.1657, 20.9671),
            ("LIS", "Humberto Delgado Airport", "Lisbon", "Portugal", "PT", "Europe/Lisbon", 38.7813, -9.1359),
            ("FAO", "Faro Airport", "Faro", "Portugal", "PT", "Europe/Lisbon", 37.0144, -7.9659),
            ("VIE", "Vienna International Airport", "Vienna", "Austria", "AT", "Europe/Vienna", 48.1103, 16.5697),
            ("ZRH", "Zurich Airport", "Zurich", "Switzerland", "CH", "Europe/Zurich", 47.4647, 8.5492),
            ("CPH", "Copenhagen Airport", "Copenhagen", "Denmark", "DK", "Europe/Copenhagen", 55.6180, 12.6560),
            ("ARN", "Stockholm Arlanda Airport", "Stockholm", "Sweden", "SE", "Europe/Stockholm", 59.6519, 17.9186),
            ("OSL", "Oslo Gardermoen Airport", "Oslo", "Norway", "NO", "Europe/Oslo", 60.1976, 11.1004),
            ("DUB", "Dublin Airport", "Dublin", "Ireland", "IE", "Europe/Dublin", 53.4213, -6.2700),
            ("NCE", "Nice Côte d'Azur Airport", "Nice", "France", "FR", "Europe/Paris", 43.6584, 7.2159),
            ("TFS", "Tenerife South Airport", "Tenerife", "Spain", "ES", "Atlantic/Canary", 28.0445, -16.5725),
            ("LPA", "Gran Canaria Airport", "Gran Canaria", "Spain", "ES", "Atlantic/Canary", 27.9319, -15.3866),
            // Middle East & Africa
            ("DXB", "Dubai International Airport", "Dubai", "UAE", "AE", "Asia/Dubai", 25.2532, 55.3657),
            ("AUH", "Abu Dhabi International Airport", "Abu Dhabi", "UAE", "AE", "Asia/Dubai", 24.4330, 54.6511),
            ("DOH", "Hamad International Airport", "Doha", "Qatar", "QA", "Asia/Qatar", 25.2609, 51.6138),
            ("CAI", "Cairo International Airport", "Cairo", "Egypt", "EG", "Africa/Cairo", 30.1219, 31.4056),
            ("JNB", "O.R. Tambo Airport", "Johannesburg", "South Africa", "ZA", "Africa/Johannesburg", -26.1367, 28.2411),
            ("CPT", "Cape Town International Airport", "Cape Town", "South Africa", "ZA", "Africa/Johannesburg", -33.9715, 18.6021),
            ("CMN", "Mohammed V Airport", "Casablanca", "Morocco", "MA", "Africa/Casablanca", 33.3675, -7.5898),
            // Asia
            ("SIN", "Singapore Changi Airport", "Singapore", "Singapore", "SG", "Asia/Singapore", 1.3644, 103.9915),
            ("BKK", "Suvarnabhumi Airport", "Bangkok", "Thailand", "TH", "Asia/Bangkok", 13.6811, 100.7472),
            ("HKG", "Hong Kong International Airport", "Hong Kong", "China", "HK", "Asia/Hong_Kong", 22.3080, 113.9185),
            ("NRT", "Narita International Airport", "Tokyo", "Japan", "JP", "Asia/Tokyo", 35.7653, 140.3856),
            ("BOM", "Chhatrapati Shivaji Airport", "Mumbai", "India", "IN", "Asia/Kolkata", 19.0896, 72.8656),
            ("DEL", "Indira Gandhi International Airport", "Delhi", "India", "IN", "Asia/Kolkata", 28.5562, 77.1000),
            // Americas
            ("JFK", "John F. Kennedy Airport", "New York", "United States", "US", "America/New_York", 40.6413, -73.7781),
            ("EWR", "Newark Liberty Airport", "New York", "United States", "US", "America/New_York", 40.6895, -74.1745),
            ("BOS", "Logan International Airport", "Boston", "United States", "US", "America/New_York", 42.3656, -71.0096),
            ("MIA", "Miami International Airport", "Miami", "United States", "US", "America/New_York", 25.7959, -80.2870),
            ("ORD", "O'Hare International Airport", "Chicago", "United States", "US", "America/Chicago", 41.9742, -87.9073),
            ("LAX", "Los Angeles International Airport", "Los Angeles", "United States", "US", "America/Los_Angeles", 33.9425, -118.4081),
            ("SFO", "San Francisco International Airport", "San Francisco", "United States", "US", "America/Los_Angeles", 37.6213, -122.3790),
            ("YYZ", "Pearson International Airport", "Toronto", "Canada", "CA", "America/Toronto", 43.6777, -79.6248),
            ("YVR", "Vancouver International Airport", "Vancouver", "Canada", "CA", "America/Vancouver", 49.1967, -123.1815),
            ("GRU", "Guarulhos Airport", "São Paulo", "Brazil", "BR", "America/Sao_Paulo", -23.4356, -46.4731),
            ("GIG", "Galeão International Airport", "Rio de Janeiro", "Brazil", "BR", "America/Sao_Paulo", -22.8099, -43.2505),
            ("EZE", "Ezeiza International Airport", "Buenos Aires", "Argentina", "AR", "America/Argentina/Buenos_Aires", -34.8222, -58.5358),
            // Australia/Pacific
            ("SYD", "Sydney Kingsford Smith Airport", "Sydney", "Australia", "AU", "Australia/Sydney", -33.9399, 151.1753),
            ("MEL", "Melbourne Airport", "Melbourne", "Australia", "AU", "Australia/Melbourne", -37.6733, 144.8430),
        };

        _db.Airports.AddRange(airports.Select(a => new Airport
        {
            IATACode = a.Item1, Name = a.Item2, City = a.Item3,
            Country = a.Item4, CountryCode = a.Item5, Timezone = a.Item6,
            Latitude = a.Item7, Longitude = a.Item8, IsActive = true
        }));

        await _db.SaveChangesAsync();
    }

    // ── Aircraft ──────────────────────────────────────────────────────────

    private async Task SeedAircraftAsync()
    {
        if (await _db.Aircraft.AnyAsync()) return;

        var configs = new[]
        {
            ("Airbus A319", "G-EUPB", 25, "3-3", 1, 4, 25, "8,9"),
            ("Airbus A320neo", "G-TTNA", 30, "3-3", 1, 5, 30, "8,9"),
            ("Airbus A321neo", "G-NEOA", 37, "3-3", 1, 7, 37, "8,9,10"),
            ("Airbus A321XLR", "G-XLRA", 38, "3-3", 1, 7, 38, "8,9,10"),
            ("Boeing 737-800", "G-FDZB", 32, "3-3", 1, 4, 32, "14,15"),
            ("Boeing 737 MAX 8", "EI-HGX", 32, "3-3", 1, 4, 32, "14,15"),
            ("Boeing 777-300ER", "G-VIIA", 50, "3-4-3", 1, 8, 50, "12,36,37"),
            ("Boeing 787-9", "G-ZBKA", 44, "3-3-3", 1, 8, 44, "11,31,32"),
            ("Boeing 787-10", "G-ZBKB", 48, "3-3-3", 1, 8, 48, "11,31,32"),
            ("Airbus A380-800", "G-XLEB", 60, "3-4-3", 1, 10, 60, "12,43,44"),
        };

        foreach (var (model, reg, rows, layout, bizStart, bizEnd, ecoEnd, extraRows) in configs)
        {
            var plane = new Aircraft
            {
                Model = model, Registration = reg, TotalRows = rows,
                SeatsPerRow = layout.Split('-').Sum(int.Parse), SeatLayout = layout,
                BusinessStartRow = bizStart, BusinessEndRow = bizEnd,
                EconomyStartRow = bizEnd + 1, EconomyEndRow = ecoEnd,
                ExtraLegroomRows = extraRows, IsActive = true
            };
            _db.Aircraft.Add(plane);
            await _db.SaveChangesAsync();
            GenerateSeats(plane, extraRows);
        }

        await _db.SaveChangesAsync();
    }

    private void GenerateSeats(Aircraft plane, string extraRows)
    {
        var extraRowNums = extraRows.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToHashSet();

        var useColumns = plane.SeatLayout switch
        {
            "3-4-3" => new[] { "A", "B", "C", "D", "E", "F", "G", "H", "J" },
            "3-3-3" => new[] { "A", "B", "C", "D", "E", "F", "G", "H", "J" },
            _ => new[] { "A", "B", "C", "D", "E", "F" }
        };

        for (int row = 1; row <= plane.TotalRows; row++)
        {
            var isExtra = extraRowNums.Contains(row);
            var isBiz = row >= plane.BusinessStartRow && row <= plane.BusinessEndRow;

            foreach (var col in useColumns)
            {
                var isWindow = col == "A" || col == useColumns.Last();
                var isAisle = col == "C" || col == "D" ||
                              (useColumns.Length > 6 && (col == "F" || col == "G"));

                _db.AircraftSeats.Add(new AircraftSeat
                {
                    AircraftId = plane.Id, Row = row, Column = col,
                    SeatNumber = $"{row}{col}",
                    Class = isBiz ? SeatClass.Business : SeatClass.Economy,
                    IsExtraLegroom = isExtra, IsExitRow = isExtra,
                    IsWindowSeat = isWindow, IsAisleSeat = isAisle,
                    IsMiddleSeat = !isWindow && !isAisle
                });
            }
        }
    }

    // ── Routes ────────────────────────────────────────────────────────────

    private async Task SeedRoutesAsync()
    {
        if (await _db.Routes.AnyAsync()) return;

        var airports = await _db.Airports.ToDictionaryAsync(a => a.IATACode, a => a.Id);

        // (Origin, Destination, DurationMins)
        var routes = new[]
        {
            // LHR long haul
            ("LHR","JFK",435), ("JFK","LHR",420),
            ("LHR","LAX",665), ("LAX","LHR",610),
            ("LHR","BOS",400), ("BOS","LHR",380),
            ("LHR","MIA",545), ("MIA","LHR",565),
            ("LHR","ORD",490), ("ORD","LHR",510),
            ("LHR","SFO",670), ("SFO","LHR",620),
            ("LHR","YYZ",470), ("YYZ","LHR",455),
            ("LHR","DXB",395), ("DXB","LHR",435),
            ("LHR","DOH",385), ("DOH","LHR",415),
            ("LHR","SIN",770), ("SIN","LHR",800),
            ("LHR","BKK",690), ("BKK","LHR",720),
            ("LHR","HKG",700), ("HKG","LHR",730),
            ("LHR","NRT",740), ("NRT","LHR",760),
            ("LHR","JNB",660), ("JNB","LHR",680),
            ("LHR","SYD",1330), ("SYD","LHR",1310),
            ("LHR","GRU",685), ("GRU","LHR",720),
            ("LHR","EZE",815), ("EZE","LHR",840),
            ("LHR","DEL",545), ("DEL","LHR",555),
            ("LHR","BOM",520), ("BOM","LHR",530),

            // LHR European (BA + EasyJet + Ryanair)
            ("LHR","CDG",75),  ("CDG","LHR",70),
            ("LHR","AMS",70),  ("AMS","LHR",65),
            ("LHR","FRA",105), ("FRA","LHR",110),
            ("LHR","MAD",140), ("MAD","LHR",135),
            ("LHR","BCN",145), ("BCN","LHR",135),
            ("LHR","FCO",155), ("FCO","LHR",150),
            ("LHR","MXP",130), ("MXP","LHR",125),
            ("LHR","ZRH",100), ("ZRH","LHR",95),
            ("LHR","VIE",145), ("VIE","LHR",145),
            ("LHR","LIS",150), ("LIS","LHR",145),
            ("LHR","DUB",80),  ("DUB","LHR",75),
            ("LHR","NCE",115), ("NCE","LHR",110),
            ("LHR","ATH",215), ("ATH","LHR",220),
            ("LHR","CPH",115), ("CPH","LHR",110),
            ("LHR","ARN",145), ("ARN","LHR",140),
            ("LHR","PRG",130), ("PRG","LHR",125),
            ("LHR","WAW",175), ("WAW","LHR",170),

            // Gatwick / Stansted low-cost
            ("LGW","PMI",150), ("PMI","LGW",145),
            ("LGW","AGP",160), ("AGP","LGW",155),
            ("LGW","TFS",240), ("TFS","LGW",235),
            ("LGW","LPA",235), ("LPA","LGW",230),
            ("LGW","FAO",155), ("FAO","LGW",150),
            ("LGW","BCN",125), ("BCN","LGW",120),
            ("LGW","FCO",160), ("FCO","LGW",155),
            ("LGW","MAD",140), ("MAD","LGW",135),
            ("LGW","AMS",65),  ("AMS","LGW",60),
            ("LGW","CDG",70),  ("CDG","LGW",65),
            ("LGW","DUB",75),  ("DUB","LGW",70),
            ("LGW","HER",215), ("HER","LGW",220),
            ("LGW","RHO",210), ("RHO","LGW",215),
            ("STN","DUB",70),  ("DUB","STN",65),
            ("STN","AMS",65),  ("AMS","STN",60),
            ("STN","BCN",130), ("BCN","STN",125),
            ("STN","MAD",135), ("MAD","STN",130),
            ("STN","FCO",155), ("FCO","STN",150),
            ("STN","MXP",130), ("MXP","STN",125),
            ("STN","PMI",145), ("PMI","STN",140),
            ("STN","TFS",240), ("TFS","STN",235),
            ("STN","LIS",150), ("LIS","STN",145),
            ("STN","CMN",195), ("CMN","STN",190),

            // MAN routes
            ("MAN","JFK",445), ("JFK","MAN",430),
            ("MAN","DXB",400), ("DXB","MAN",440),
            ("MAN","CDG",80),  ("CDG","MAN",75),
            ("MAN","AMS",75),  ("AMS","MAN",70),
            ("MAN","BCN",145), ("BCN","MAN",140),
            ("MAN","MAD",145), ("MAD","MAN",140),
            ("MAN","DUB",55),  ("DUB","MAN",50),
            ("MAN","FCO",165), ("FCO","MAN",160),
            ("MAN","PMI",160), ("PMI","MAN",155),
            ("MAN","TFS",245), ("TFS","MAN",240),
            ("MAN","LPA",240), ("LPA","MAN",235),

            // Trans-Atlantic direct
            ("CDG","JFK",445), ("JFK","CDG",430),
            ("FRA","JFK",460), ("JFK","FRA",450),
            ("AMS","JFK",455), ("JFK","AMS",445),
            ("MAD","JFK",475), ("JFK","MAD",460),
            ("FCO","JFK",495), ("JFK","FCO",480),
        };

        int idx = 100;
        foreach (var (orig, dest, dur) in routes)
        {
            if (!airports.ContainsKey(orig) || !airports.ContainsKey(dest)) continue;
            _db.Routes.Add(new Route
            {
                OriginAirportId = airports[orig],
                DestinationAirportId = airports[dest],
                DurationMinutes = dur,
                RouteCode = $"R{idx++:D4}",
                IsActive = true
            });
        }

        await _db.SaveChangesAsync();
    }

    // ── Flights ───────────────────────────────────────────────────────────

    private async Task SeedFlightsAsync()
    {
        if (await _db.Flights.AnyAsync()) return;

        var routes = await _db.Routes
            .Include(r => r.OriginAirport)
            .Include(r => r.DestinationAirport)
            .ToListAsync();
        var aircraft = await _db.Aircraft.Where(a => a.IsActive).ToListAsync();
        var airlines = await _db.Airlines.ToDictionaryAsync(a => a.IATACode, a => a.Id);

        if (!aircraft.Any()) return;

        var rng = new Random(42);

        // Airline assignments per route type
        // key = "ORIG-DEST" or "ORIG-*" wildcard, value = list of (airlineCode, flightPrefix, hourOffsets, aircraftModel, ecoPrice, bizPrice)
        var schedules = BuildSchedules();
        // Real BA flight number lookup: route key -> ordered flight numbers per departure slot
        var baFlightNumbers = BuildBaFlightNumbers();
        var today = DateTime.Today;
        int flightCounter = 100;

        foreach (var route in routes)
        {
            var key = $"{route.OriginAirport.IATACode}-{route.DestinationAirport.IATACode}";

            if (!schedules.TryGetValue(key, out var ops) &&
                !schedules.TryGetValue($"{route.OriginAirport.IATACode}-*", out ops))
                continue;

            foreach (var op in ops)
            {
                if (!airlines.TryGetValue(op.AirlineCode, out int airlineId)) continue;

                var plane = aircraft.FirstOrDefault(a => a.Model.Contains(op.AircraftModel))
                    ?? aircraft[rng.Next(aircraft.Count)];

                // Pre-resolve real BA flight numbers for this route/operator
                baFlightNumbers.TryGetValue(key, out var realBaNumbers);
                int slotIndex = 0;

                for (int day = 0; day < 90; day++)
                {
                    var date = today.AddDays(day);
                    slotIndex = 0;

                    foreach (int hour in op.DepartureHours)
                    {
                        var dep = new DateTime(date.Year, date.Month, date.Day,
                            hour, rng.Next(0, 4) * 5, 0, DateTimeKind.Utc);
                        var arr = dep.AddMinutes(route.DurationMinutes);

                        int ecoSeats = Math.Max(10, (plane.TotalRows - plane.BusinessEndRow) * plane.SeatsPerRow);
                        int bizSeats = Math.Max(0, plane.BusinessEndRow * plane.SeatsPerRow);

                        decimal mult = 1.0m + (decimal)(rng.NextDouble() * 0.35);

                        // Use real BA flight numbers where available; fall back to generated
                        string flightNum;
                        if (op.AirlineCode == "BA" && realBaNumbers != null && slotIndex < realBaNumbers.Length)
                            flightNum = realBaNumbers[slotIndex];
                        else
                            flightNum = $"{op.AirlineCode}{flightCounter++}";

                        // Append date suffix on day > 0 to maintain uniqueness
                        if (day > 0)
                            flightNum = $"{flightNum}/{date:yyyyMMdd}";

                        slotIndex++;

                        var flight = new Flight
                        {
                            FlightNumber = flightNum,
                            AirlineId = airlineId,
                            RouteId = route.Id,
                            AircraftId = plane.Id,
                            DepartureTime = dep,
                            ArrivalTime = arr,
                            Status = FlightStatus.Scheduled,
                            AvailableEconomySeats = ecoSeats,
                            AvailableBusinessSeats = bizSeats,
                            AvailableFirstSeats = 0
                        };
                        _db.Flights.Add(flight);
                        await _db.SaveChangesAsync();

                        // Prices
                        var prices = new List<FlightSeatPrice>
                        {
                            new() { FlightId = flight.Id, Class = SeatClass.Economy,
                                BasePrice = op.EcoBasePrice, DynamicMultiplier = mult,
                                SeatSelectionFee = op.IsLowCost ? 0 : 10,
                                ExtraLegroomFee = op.IsLowCost ? 12 : 25,
                                BaggageFee = op.IsLowCost ? 30 : 35 }
                        };

                        if (bizSeats > 0 && op.BizBasePrice > 0)
                            prices.Add(new FlightSeatPrice
                            {
                                FlightId = flight.Id, Class = SeatClass.Business,
                                BasePrice = op.BizBasePrice, DynamicMultiplier = mult
                            });

                        _db.FlightSeatPrices.AddRange(prices);
                    }
                }

                await _db.SaveChangesAsync();
            }
        }
    }

    private record AirlineOp(
        string AirlineCode, string AircraftModel,
        int[] DepartureHours, decimal EcoBasePrice, decimal BizBasePrice,
        bool IsLowCost = false);

    private static Dictionary<string, List<AirlineOp>> BuildSchedules() => new()
    {
        // ── LHR → North America (BA + VS + AA + UA)
        ["LHR-JFK"] = new()
        {
            new("BA", "Boeing 777-300ER", [7, 10, 13, 18], 299, 1299),
            new("VS", "Boeing 787-9",     [8, 14, 20],     279, 1199),
            new("AA", "Boeing 777-300ER", [9, 17],          289, 1249),
        },
        ["JFK-LHR"] = new()
        {
            new("BA", "Boeing 777-300ER", [9, 19, 23],     289, 1249),
            new("VS", "Boeing 787-9",     [21, 0],          279, 1199),
            new("AA", "Boeing 777-300ER", [20, 23],         289, 1199),
        },
        ["LHR-BOS"] = new()
        {
            new("BA", "Boeing 777-300ER", [9, 13], 279, 1149),
            new("VS", "Boeing 787-9",     [11],    269, 1099),
        },
        ["BOS-LHR"] = new() { new("BA", "Boeing 777-300ER", [19, 22], 279, 1149), new("VS", "Boeing 787-9", [21], 269, 1099) },
        ["LHR-LAX"] = new()
        {
            new("BA", "Boeing 787-9", [10, 15], 549, 2199),
            new("VS", "Boeing 787-9", [12],     529, 2099),
        },
        ["LAX-LHR"] = new() { new("BA", "Boeing 787-9", [16, 20], 549, 2199), new("VS", "Boeing 787-9", [17], 529, 2099) },
        ["LHR-MIA"] = new() { new("BA", "Boeing 777-300ER", [10, 14], 399, 1599), new("AA", "Boeing 777-300ER", [11], 379, 1499) },
        ["MIA-LHR"] = new() { new("BA", "Boeing 777-300ER", [18, 22], 399, 1599), new("AA", "Boeing 777-300ER", [19], 379, 1499) },
        ["LHR-ORD"] = new() { new("BA", "Boeing 777-300ER", [9, 14], 319, 1299), new("UA", "Boeing 777-300ER", [11], 299, 1249) },
        ["ORD-LHR"] = new() { new("BA", "Boeing 777-300ER", [18, 21], 319, 1299), new("UA", "Boeing 777-300ER", [20], 299, 1249) },
        ["LHR-SFO"] = new() { new("BA", "Boeing 787-9", [10, 15], 549, 2199), new("UA", "Boeing 787-9", [12], 529, 2099) },
        ["SFO-LHR"] = new() { new("BA", "Boeing 787-9", [16, 20], 549, 2199) },
        ["LHR-YYZ"] = new() { new("BA", "Boeing 777-300ER", [9, 14], 359, 1399) },
        ["YYZ-LHR"] = new() { new("BA", "Boeing 777-300ER", [17, 21], 359, 1399) },

        // ── LHR → Gulf + Asia (BA + EK + QR + SQ)
        ["LHR-DXB"] = new()
        {
            new("BA", "Boeing 777-300ER", [8, 14, 22], 349, 1599),
            new("EK", "Airbus A380-800",  [8, 14, 21], 329, 1499),
        },
        ["DXB-LHR"] = new()
        {
            new("BA", "Boeing 777-300ER", [3, 9, 15], 349, 1599),
            new("EK", "Airbus A380-800",  [8, 14, 22], 329, 1499),
        },
        ["LHR-DOH"] = new() { new("BA", "Boeing 777-300ER", [9, 15], 329, 1499), new("QR", "Boeing 787-9", [8, 23], 319, 1449) },
        ["DOH-LHR"] = new() { new("BA", "Boeing 777-300ER", [8, 14], 329, 1499), new("QR", "Boeing 787-9", [7, 23], 319, 1449) },
        ["LHR-SIN"] = new() { new("BA", "Boeing 787-9", [21, 23], 699, 2499), new("SQ", "Airbus A380-800", [22, 0], 679, 2399) },
        ["SIN-LHR"] = new() { new("BA", "Boeing 787-9", [22, 1], 699, 2499), new("SQ", "Airbus A380-800", [23], 679, 2399) },
        ["LHR-BKK"] = new() { new("BA", "Boeing 777-300ER", [21, 22], 599, 2299), new("TK", "Boeing 787-9", [22], 579, 2199) },
        ["BKK-LHR"] = new() { new("BA", "Boeing 777-300ER", [21, 23], 599, 2299) },
        ["LHR-HKG"] = new() { new("BA", "Boeing 777-300ER", [21], 649, 2399) },
        ["HKG-LHR"] = new() { new("BA", "Boeing 777-300ER", [22], 649, 2399) },
        ["LHR-NRT"] = new() { new("BA", "Boeing 777-300ER", [10], 699, 2599) },
        ["NRT-LHR"] = new() { new("BA", "Boeing 777-300ER", [12], 699, 2599) },
        ["LHR-JNB"] = new() { new("BA", "Boeing 777-300ER", [20, 21], 599, 2199) },
        ["JNB-LHR"] = new() { new("BA", "Boeing 777-300ER", [20, 21], 599, 2199) },
        ["LHR-SYD"] = new() { new("BA", "Boeing 787-9", [21], 1099, 3999) },
        ["SYD-LHR"] = new() { new("BA", "Boeing 787-9", [15], 1099, 3999) },
        ["LHR-GRU"] = new() { new("BA", "Boeing 777-300ER", [21], 699, 2599) },
        ["GRU-LHR"] = new() { new("BA", "Boeing 777-300ER", [22], 699, 2599) },
        ["LHR-DEL"] = new() { new("BA", "Boeing 777-300ER", [8, 21], 449, 1799) },
        ["DEL-LHR"] = new() { new("BA", "Boeing 777-300ER", [2, 14], 449, 1799) },
        ["LHR-BOM"] = new() { new("BA", "Boeing 777-300ER", [8, 22], 429, 1699) },
        ["BOM-LHR"] = new() { new("BA", "Boeing 777-300ER", [2, 16], 429, 1699) },

        // ── LHR European (BA + U2 + LH + AF + KL + IB)
        ["LHR-CDG"] = new()
        {
            new("BA", "Airbus A320neo", [6, 8, 10, 12, 14, 16, 18, 20], 89, 399),
            new("AF", "Airbus A320neo", [7, 9, 11, 13, 15, 17, 19],     79, 349),
        },
        ["CDG-LHR"] = new()
        {
            new("BA", "Airbus A320neo", [7, 9, 11, 13, 15, 17, 19, 21], 79, 349),
            new("AF", "Airbus A320neo", [8, 10, 12, 14, 16, 18, 20],    79, 349),
        },
        ["LHR-AMS"] = new()
        {
            new("BA", "Airbus A320neo", [6, 8, 10, 12, 14, 16, 18, 20], 79, 349),
            new("KL", "Airbus A320neo", [7, 9, 11, 13, 15, 17, 19],     69, 329),
        },
        ["AMS-LHR"] = new()
        {
            new("BA", "Airbus A320neo", [7, 9, 11, 13, 15, 17, 19], 79, 349),
            new("KL", "Airbus A320neo", [8, 10, 12, 14, 16, 18],    69, 329),
        },
        ["LHR-FRA"] = new()
        {
            new("BA", "Airbus A319",   [6, 9, 12, 15, 18, 20], 99, 449),
            new("LH", "Airbus A319",   [7, 10, 13, 16, 19],    89, 419),
        },
        ["FRA-LHR"] = new()
        {
            new("BA", "Airbus A319",   [8, 11, 14, 17, 20], 99, 449),
            new("LH", "Airbus A319",   [9, 12, 15, 18, 21], 89, 419),
        },
        ["LHR-MAD"] = new()
        {
            new("BA", "Airbus A321neo", [7, 10, 13, 17, 20], 119, 499),
            new("IB", "Airbus A321neo", [8, 11, 14, 18, 21], 109, 469),
        },
        ["MAD-LHR"] = new()
        {
            new("BA", "Airbus A321neo", [8, 11, 14, 18, 21], 109, 469),
            new("IB", "Airbus A321neo", [9, 12, 15, 19, 22], 99, 449),
        },
        ["LHR-BCN"] = new()
        {
            new("BA", "Airbus A321neo", [7, 11, 15, 19], 109, 469),
            new("IB", "Airbus A321neo", [8, 12, 16, 20], 99, 449),
            new("U2", "Airbus A320neo", [6, 13, 19],     49, 0, true),
        },
        ["BCN-LHR"] = new()
        {
            new("BA", "Airbus A321neo", [8, 12, 16, 20], 109, 469),
            new("IB", "Airbus A321neo", [9, 13, 17, 21], 99, 449),
            new("U2", "Airbus A320neo", [7, 14, 20],     49, 0, true),
        },
        ["LHR-FCO"] = new()
        {
            new("BA", "Airbus A321neo", [7, 11, 15, 19], 129, 549),
            new("U2", "Airbus A320neo", [6, 14, 20],     59, 0, true),
        },
        ["FCO-LHR"] = new()
        {
            new("BA", "Airbus A321neo", [8, 12, 16, 20], 129, 549),
            new("U2", "Airbus A320neo", [7, 15, 21],     59, 0, true),
        },
        ["LHR-LIS"] = new()
        {
            new("BA", "Airbus A321neo", [7, 11, 15, 19], 119, 499),
            new("TP", "Airbus A320neo", [8, 12, 16, 20], 109, 469),
        },
        ["LIS-LHR"] = new()
        {
            new("BA", "Airbus A321neo", [8, 12, 16, 20], 119, 499),
            new("TP", "Airbus A320neo", [9, 13, 17, 21], 109, 469),
        },
        ["LHR-DUB"] = new()
        {
            new("BA", "Airbus A319",   [6, 8, 10, 12, 14, 16, 18, 20], 69, 299),
            new("U2", "Airbus A319",   [7, 9, 11, 13, 15, 17, 19],     39, 0, true),
            new("FR", "Boeing 737-800",[6, 9, 12, 16, 20],              25, 0, true),
        },
        ["DUB-LHR"] = new()
        {
            new("BA", "Airbus A319",   [7, 9, 11, 13, 15, 17, 19, 21], 69, 299),
            new("U2", "Airbus A319",   [8, 10, 12, 14, 16, 18, 20],    39, 0, true),
            new("FR", "Boeing 737-800",[7, 10, 13, 17, 21],             25, 0, true),
        },
        ["LHR-ZRH"] = new() { new("BA", "Airbus A319", [7, 10, 14, 18], 99, 449), new("LH", "Airbus A319", [8, 11, 15, 19], 89, 419) },
        ["ZRH-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 15, 19], 99, 449), new("LH", "Airbus A319", [9, 12, 16, 20], 89, 419) },
        ["LHR-VIE"] = new() { new("BA", "Airbus A319", [7, 11, 16], 109, 469) },
        ["VIE-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17], 109, 469) },
        ["LHR-ATH"] = new() { new("BA", "Airbus A321neo", [7, 13, 20], 149, 599), new("U2", "Airbus A321neo", [8, 15], 79, 0, true) },
        ["ATH-LHR"] = new() { new("BA", "Airbus A321neo", [8, 14, 21], 149, 599), new("U2", "Airbus A321neo", [9, 16], 79, 0, true) },
        ["LHR-CPH"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 109, 469) },
        ["CPH-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 109, 469) },
        ["LHR-ARN"] = new() { new("BA", "Airbus A319", [7, 13], 139, 549) },
        ["ARN-LHR"] = new() { new("BA", "Airbus A319", [8, 14], 139, 549) },
        ["LHR-NCE"] = new() { new("BA", "Airbus A319", [8, 12, 17], 109, 469), new("U2", "Airbus A319", [7, 15], 59, 0, true) },
        ["NCE-LHR"] = new() { new("BA", "Airbus A319", [9, 13, 18], 109, 469), new("U2", "Airbus A319", [8, 16], 59, 0, true) },
        ["LHR-PRG"] = new() { new("BA", "Airbus A319", [7, 14], 119, 499) },
        ["PRG-LHR"] = new() { new("BA", "Airbus A319", [8, 15], 119, 499) },
        ["LHR-WAW"] = new() { new("BA", "Airbus A319", [7, 14], 129, 499), new("W6", "Airbus A320neo", [9, 17], 45, 0, true) },
        ["WAW-LHR"] = new() { new("BA", "Airbus A319", [8, 15], 129, 499), new("W6", "Airbus A320neo", [10, 18], 45, 0, true) },
        ["LHR-MXP"] = new() { new("BA", "Airbus A320neo", [7, 11, 15, 19], 119, 499) },
        ["MXP-LHR"] = new() { new("BA", "Airbus A320neo", [8, 12, 16, 20], 119, 499) },

        // ── Gatwick (EasyJet + Ryanair holiday routes)
        ["LGW-PMI"] = new()
        {
            new("U2", "Airbus A320neo", [6, 8, 11, 14, 17, 20], 55, 0, true),
            new("BA", "Airbus A321neo", [7, 12, 18],             129, 0),
        },
        ["PMI-LGW"] = new()
        {
            new("U2", "Airbus A320neo", [7, 9, 12, 15, 18, 21], 55, 0, true),
            new("BA", "Airbus A321neo", [8, 13, 19],             129, 0),
        },
        ["LGW-AGP"] = new() { new("U2", "Airbus A320neo", [6, 11, 16], 65, 0, true), new("FR", "Boeing 737-800", [7, 14, 20], 35, 0, true) },
        ["AGP-LGW"] = new() { new("U2", "Airbus A320neo", [7, 12, 17], 65, 0, true), new("FR", "Boeing 737-800", [8, 15, 21], 35, 0, true) },
        ["LGW-TFS"] = new() { new("U2", "Airbus A320neo", [6, 13, 20], 109, 0, true), new("FR", "Boeing 737-800", [7, 14], 55, 0, true) },
        ["TFS-LGW"] = new() { new("U2", "Airbus A320neo", [7, 14, 21], 109, 0, true), new("FR", "Boeing 737-800", [8, 15], 55, 0, true) },
        ["LGW-LPA"] = new() { new("U2", "Airbus A320neo", [6, 14], 109, 0, true), new("FR", "Boeing 737-800", [7, 15], 49, 0, true) },
        ["LPA-LGW"] = new() { new("U2", "Airbus A320neo", [7, 15], 109, 0, true), new("FR", "Boeing 737-800", [8, 16], 49, 0, true) },
        ["LGW-FAO"] = new() { new("U2", "Airbus A320neo", [6, 13, 19], 79, 0, true), new("FR", "Boeing 737-800", [7, 14, 20], 39, 0, true) },
        ["FAO-LGW"] = new() { new("U2", "Airbus A320neo", [7, 14, 20], 79, 0, true), new("FR", "Boeing 737-800", [8, 15, 21], 39, 0, true) },
        ["LGW-BCN"] = new() { new("U2", "Airbus A320neo", [6, 10, 14, 18], 59, 0, true), new("FR", "Boeing 737-800", [7, 12, 17], 29, 0, true) },
        ["BCN-LGW"] = new() { new("U2", "Airbus A320neo", [7, 11, 15, 19], 59, 0, true), new("FR", "Boeing 737-800", [8, 13, 18], 29, 0, true) },
        ["LGW-FCO"] = new() { new("U2", "Airbus A320neo", [6, 11, 16], 79, 0, true), new("FR", "Boeing 737-800", [7, 14], 39, 0, true) },
        ["FCO-LGW"] = new() { new("U2", "Airbus A320neo", [7, 12, 17], 79, 0, true), new("FR", "Boeing 737-800", [8, 15], 39, 0, true) },
        ["LGW-MAD"] = new() { new("U2", "Airbus A320neo", [6, 12, 17], 75, 0, true), new("FR", "Boeing 737-800", [7, 14], 35, 0, true) },
        ["MAD-LGW"] = new() { new("U2", "Airbus A320neo", [7, 13, 18], 75, 0, true), new("FR", "Boeing 737-800", [8, 15], 35, 0, true) },
        ["LGW-HER"] = new() { new("U2", "Airbus A320neo", [6, 14], 109, 0, true), new("FR", "Boeing 737-800", [7, 15], 59, 0, true) },
        ["HER-LGW"] = new() { new("U2", "Airbus A320neo", [7, 15], 109, 0, true), new("FR", "Boeing 737-800", [8, 16], 59, 0, true) },
        ["LGW-RHO"] = new() { new("U2", "Airbus A320neo", [6, 14], 109, 0, true) },
        ["RHO-LGW"] = new() { new("U2", "Airbus A320neo", [7, 15], 109, 0, true) },
        ["LGW-AMS"] = new() { new("U2", "Airbus A319", [6, 9, 12, 16, 19], 39, 0, true) },
        ["AMS-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 13, 17, 20], 39, 0, true) },
        ["LGW-CDG"] = new() { new("U2", "Airbus A319", [6, 9, 13, 17, 20], 45, 0, true) },
        ["CDG-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 14, 18, 21], 45, 0, true) },
        ["LGW-DUB"] = new() { new("U2", "Airbus A319", [6, 9, 12, 15, 18, 21], 39, 0, true) },
        ["DUB-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 13, 16, 19, 22], 39, 0, true) },

        // ── Stansted Ryanair
        ["STN-DUB"] = new() { new("FR", "Boeing 737-800", [6, 8, 10, 12, 14, 16, 18, 20], 25, 0, true) },
        ["DUB-STN"] = new() { new("FR", "Boeing 737-800", [7, 9, 11, 13, 15, 17, 19, 21], 25, 0, true) },
        ["STN-AMS"] = new() { new("FR", "Boeing 737-800", [6, 9, 12, 16, 20], 35, 0, true) },
        ["AMS-STN"] = new() { new("FR", "Boeing 737-800", [7, 10, 13, 17, 21], 35, 0, true) },
        ["STN-BCN"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18], 25, 0, true) },
        ["BCN-STN"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19], 25, 0, true) },
        ["STN-MAD"] = new() { new("FR", "Boeing 737-800", [6, 11, 16], 25, 0, true) },
        ["MAD-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17], 25, 0, true) },
        ["STN-FCO"] = new() { new("FR", "Boeing 737-800", [6, 12, 18], 29, 0, true) },
        ["FCO-STN"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 29, 0, true) },
        ["STN-MXP"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18], 25, 0, true) },
        ["MXP-STN"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19], 25, 0, true) },
        ["STN-PMI"] = new() { new("FR", "Boeing 737-800", [6, 11, 16], 29, 0, true) },
        ["PMI-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17], 29, 0, true) },
        ["STN-TFS"] = new() { new("FR", "Boeing 737-800", [6, 14], 45, 0, true) },
        ["TFS-STN"] = new() { new("FR", "Boeing 737-800", [7, 15], 45, 0, true) },
        ["STN-LIS"] = new() { new("FR", "Boeing 737-800", [6, 13, 20], 35, 0, true) },
        ["LIS-STN"] = new() { new("FR", "Boeing 737-800", [7, 14, 21], 35, 0, true) },
        ["STN-CMN"] = new() { new("FR", "Boeing 737-800", [7, 15], 45, 0, true) },
        ["CMN-STN"] = new() { new("FR", "Boeing 737-800", [8, 16], 45, 0, true) },

        // ── MAN routes
        ["MAN-JFK"] = new() { new("BA", "Boeing 777-300ER", [10], 279, 1149), new("VS", "Boeing 787-9", [11], 259, 1099) },
        ["JFK-MAN"] = new() { new("BA", "Boeing 777-300ER", [21], 279, 1149), new("VS", "Boeing 787-9", [22], 259, 1099) },
        ["MAN-DXB"] = new() { new("BA", "Boeing 777-300ER", [9, 15], 319, 1499), new("EK", "Airbus A380-800", [10, 23], 299, 1399) },
        ["DXB-MAN"] = new() { new("BA", "Boeing 777-300ER", [3, 14], 319, 1499), new("EK", "Airbus A380-800", [9, 14], 299, 1399) },
        ["MAN-CDG"] = new() { new("BA", "Airbus A319", [6, 10, 14, 18], 89, 299), new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["CDG-MAN"] = new() { new("BA", "Airbus A319", [7, 11, 15, 19], 89, 299), new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["MAN-AMS"] = new() { new("BA", "Airbus A319", [6, 10, 14, 18], 79, 289), new("KL", "Airbus A319", [7, 11, 15, 19], 69, 269) },
        ["AMS-MAN"] = new() { new("BA", "Airbus A319", [7, 11, 15, 19], 79, 289), new("KL", "Airbus A319", [8, 12, 16, 20], 69, 269) },
        ["MAN-BCN"] = new() { new("U2", "Airbus A320neo", [6, 11, 16], 55, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["BCN-MAN"] = new() { new("U2", "Airbus A320neo", [7, 12, 17], 55, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["MAN-MAD"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 69, 0, true) },
        ["MAD-MAN"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 69, 0, true) },
        ["MAN-DUB"] = new() { new("FR", "Boeing 737-800", [6, 9, 12, 16, 20], 19, 0, true), new("U2", "Airbus A319", [7, 11, 15, 19], 35, 0, true) },
        ["DUB-MAN"] = new() { new("FR", "Boeing 737-800", [7, 10, 13, 17, 21], 19, 0, true), new("U2", "Airbus A319", [8, 12, 16, 20], 35, 0, true) },
        ["MAN-FCO"] = new() { new("U2", "Airbus A320neo", [6, 14], 69, 0, true), new("BA", "Airbus A321neo", [8, 17], 139, 549) },
        ["FCO-MAN"] = new() { new("U2", "Airbus A320neo", [7, 15], 69, 0, true), new("BA", "Airbus A321neo", [9, 18], 139, 549) },
        ["MAN-PMI"] = new() { new("U2", "Airbus A320neo", [6, 10, 14, 18], 49, 0, true), new("FR", "Boeing 737-800", [7, 12, 17], 25, 0, true) },
        ["PMI-MAN"] = new() { new("U2", "Airbus A320neo", [7, 11, 15, 19], 49, 0, true), new("FR", "Boeing 737-800", [8, 13, 18], 25, 0, true) },
        ["MAN-TFS"] = new() { new("U2", "Airbus A320neo", [6, 15], 89, 0, true), new("FR", "Boeing 737-800", [7, 16], 45, 0, true) },
        ["TFS-MAN"] = new() { new("U2", "Airbus A320neo", [7, 16], 89, 0, true), new("FR", "Boeing 737-800", [8, 17], 45, 0, true) },
        ["MAN-LPA"] = new() { new("U2", "Airbus A320neo", [6, 15], 89, 0, true) },
        ["LPA-MAN"] = new() { new("U2", "Airbus A320neo", [7, 16], 89, 0, true) },

        // ── Trans-Atlantic non-LHR
        ["CDG-JFK"] = new() { new("AF", "Boeing 777-300ER", [9, 14, 23], 369, 1499), new("AA", "Boeing 777-300ER", [10, 22], 349, 1399) },
        ["JFK-CDG"] = new() { new("AF", "Boeing 777-300ER", [19, 22, 1], 369, 1499), new("AA", "Boeing 777-300ER", [18, 21], 349, 1399) },
        ["FRA-JFK"] = new() { new("LH", "Boeing 747-8", [10, 14], 379, 1549), new("UA", "Boeing 777-300ER", [9, 15], 349, 1399) },
        ["JFK-FRA"] = new() { new("LH", "Boeing 747-8", [18, 22], 379, 1549), new("UA", "Boeing 777-300ER", [17, 21], 349, 1399) },
        ["AMS-JFK"] = new() { new("KL", "Boeing 787-9", [10, 14], 359, 1449), new("UA", "Boeing 787-9", [11], 339, 1399) },
        ["JFK-AMS"] = new() { new("KL", "Boeing 787-9", [19, 23], 359, 1449) },
        ["MAD-JFK"] = new() { new("IB", "Airbus A350-900", [11, 16], 389, 1599), new("AA", "Boeing 777-300ER", [12], 369, 1499) },
        ["JFK-MAD"] = new() { new("IB", "Airbus A350-900", [19, 22], 389, 1599), new("AA", "Boeing 777-300ER", [20], 369, 1499) },
        ["FCO-JFK"] = new() { new("BA", "Boeing 777-300ER", [10, 14], 399, 1649), new("AA", "Boeing 777-300ER", [11], 379, 1549) },
        ["JFK-FCO"] = new() { new("BA", "Boeing 777-300ER", [19, 23], 399, 1649), new("AA", "Boeing 777-300ER", [20], 379, 1549) },
    };

    /// <summary>
    /// Maps route keys to ordered real BA flight numbers aligned with departure hour slots.
    /// Sources: FlightAware, FlightRadar24, Speedbird Online timetable data (2025).
    /// Each array element corresponds to the matching index in the departure hours array.
    /// </summary>
    private static Dictionary<string, string[]> BuildBaFlightNumbers() => new()
    {
        // LHR → New York JFK: BA175 (09:35), BA177 (11:00), BA179 (14:00), BA183 (18:00)
        ["LHR-JFK"] = ["BA175", "BA177", "BA179", "BA183"],
        // JFK → LHR: BA176 (21:30), BA178 (22:45), BA180 (23:15)
        ["JFK-LHR"] = ["BA176", "BA178", "BA180"],
        // LHR → Los Angeles: BA269 (10:15), BA271 (15:00)
        ["LHR-LAX"] = ["BA269", "BA271"],
        ["LAX-LHR"] = ["BA268", "BA270"],
        // LHR → Boston: BA213 (09:25), BA215 (13:10)
        ["LHR-BOS"] = ["BA213", "BA215"],
        ["BOS-LHR"] = ["BA214", "BA216"],
        // LHR → Miami: BA207 (10:00), BA209 (14:05)
        ["LHR-MIA"] = ["BA207", "BA209"],
        ["MIA-LHR"] = ["BA208", "BA210"],
        // LHR → Chicago O'Hare: BA295 (09:10), BA297 (14:00)
        ["LHR-ORD"] = ["BA295", "BA297"],
        ["ORD-LHR"] = ["BA296", "BA298"],
        // LHR → San Francisco: BA285 (10:15), BA287 (15:00)
        ["LHR-SFO"] = ["BA285", "BA287"],
        ["SFO-LHR"] = ["BA284", "BA286"],
        // LHR → Toronto: BA93 (11:55 LHR dep), BA95 (14:30)
        ["LHR-YYZ"] = ["BA93", "BA95"],
        ["YYZ-LHR"] = ["BA94", "BA96"],
        // LHR → Dubai: BA105 (08:00), BA107 (14:00), BA109 (21:40)
        ["LHR-DXB"] = ["BA105", "BA107", "BA109"],
        ["DXB-LHR"] = ["BA106", "BA108", "BA110"],
        // LHR → Doha: BA123 (09:00), BA125 (15:00)
        ["LHR-DOH"] = ["BA123", "BA125"],
        ["DOH-LHR"] = ["BA124", "BA126"],
        // LHR → Singapore: BA11 (18:40), BA13 (23:00)
        ["LHR-SIN"] = ["BA11", "BA13"],
        ["SIN-LHR"] = ["BA12", "BA16"],
        // LHR → Bangkok: BA9 (21:00), BA11 codeshare (22:00)
        ["LHR-BKK"] = ["BA9", "BA11"],
        ["BKK-LHR"] = ["BA10", "BA12"],
        // LHR → Hong Kong: BA25 (18:35)
        ["LHR-HKG"] = ["BA25"],
        ["HKG-LHR"] = ["BA26"],
        // LHR → Tokyo Narita: BA5 (10:30)
        ["LHR-NRT"] = ["BA5"],
        ["NRT-LHR"] = ["BA6"],
        // LHR → Johannesburg: BA55 (18:30), BA57 (20:00)
        ["LHR-JNB"] = ["BA55", "BA57"],
        ["JNB-LHR"] = ["BA56", "BA58"],
        // LHR → Sydney (via Singapore): BA15 (21:00)
        ["LHR-SYD"] = ["BA15"],
        ["SYD-LHR"] = ["BA16"],
        // LHR → São Paulo: BA247 (21:00)
        ["LHR-GRU"] = ["BA247"],
        ["GRU-LHR"] = ["BA248"],
        // LHR → Delhi: BA141 (08:30), BA143 (21:30)
        ["LHR-DEL"] = ["BA141", "BA143"],
        ["DEL-LHR"] = ["BA142", "BA144"],
        // LHR → Mumbai: BA139 (08:30), BA141 (22:00)
        ["LHR-BOM"] = ["BA139", "BA141"],
        ["BOM-LHR"] = ["BA138", "BA140"],
        // LHR → Paris CDG: BA306, BA308, BA310, BA312, BA314, BA316, BA318, BA320
        ["LHR-CDG"] = ["BA306", "BA308", "BA310", "BA312", "BA314", "BA316", "BA318", "BA320"],
        ["CDG-LHR"] = ["BA307", "BA309", "BA311", "BA313", "BA315", "BA317", "BA319", "BA321"],
        // LHR → Amsterdam: BA428, BA430, BA432, BA434, BA436, BA438, BA440, BA442
        ["LHR-AMS"] = ["BA428", "BA430", "BA432", "BA434", "BA436", "BA438", "BA440", "BA442"],
        ["AMS-LHR"] = ["BA429", "BA431", "BA433", "BA435", "BA437", "BA439", "BA441"],
        // LHR → Frankfurt: BA902, BA904, BA906, BA908, BA910, BA912
        ["LHR-FRA"] = ["BA902", "BA904", "BA906", "BA908", "BA910", "BA912"],
        ["FRA-LHR"] = ["BA903", "BA905", "BA907", "BA909", "BA911"],
        // LHR → Madrid: BA460, BA462, BA464, BA466, BA468
        ["LHR-MAD"] = ["BA460", "BA462", "BA464", "BA466", "BA468"],
        ["MAD-LHR"] = ["BA461", "BA463", "BA465", "BA467", "BA469"],
        // LHR → Barcelona: BA478, BA480, BA482, BA484
        ["LHR-BCN"] = ["BA478", "BA480", "BA482", "BA484"],
        ["BCN-LHR"] = ["BA479", "BA481", "BA483", "BA485"],
        // LHR → Rome FCO: BA548, BA550, BA552, BA554
        ["LHR-FCO"] = ["BA548", "BA550", "BA552", "BA554"],
        ["FCO-LHR"] = ["BA549", "BA551", "BA553", "BA555"],
        // LHR → Lisbon: BA500, BA502, BA504, BA506
        ["LHR-LIS"] = ["BA500", "BA502", "BA504", "BA506"],
        ["LIS-LHR"] = ["BA501", "BA503", "BA505", "BA507"],
        // LHR → Dublin: BA828, BA830, BA832, BA834, BA836, BA838, BA840, BA842
        ["LHR-DUB"] = ["BA828", "BA830", "BA832", "BA834", "BA836", "BA838", "BA840", "BA842"],
        ["DUB-LHR"] = ["BA829", "BA831", "BA833", "BA835", "BA837", "BA839", "BA841"],
        // LHR → Zurich: BA712, BA714, BA716, BA718
        ["LHR-ZRH"] = ["BA712", "BA714", "BA716", "BA718"],
        ["ZRH-LHR"] = ["BA713", "BA715", "BA717", "BA719"],
        // LHR → Vienna: BA700, BA702, BA704
        ["LHR-VIE"] = ["BA700", "BA702", "BA704"],
        ["VIE-LHR"] = ["BA701", "BA703", "BA705"],
        // LHR → Athens: BA628, BA630, BA632
        ["LHR-ATH"] = ["BA628", "BA630", "BA632"],
        ["ATH-LHR"] = ["BA629", "BA631", "BA633"],
        // LHR → Copenhagen: BA814, BA816, BA818, BA820
        ["LHR-CPH"] = ["BA814", "BA816", "BA818", "BA820"],
        ["CPH-LHR"] = ["BA815", "BA817", "BA819", "BA821"],
        // LHR → Stockholm: BA776, BA778
        ["LHR-ARN"] = ["BA776", "BA778"],
        ["ARN-LHR"] = ["BA777", "BA779"],
        // LHR → Nice: BA338, BA340, BA342
        ["LHR-NCE"] = ["BA338", "BA340", "BA342"],
        ["NCE-LHR"] = ["BA339", "BA341", "BA343"],
        // LHR → Prague: BA860, BA862
        ["LHR-PRG"] = ["BA860", "BA862"],
        ["PRG-LHR"] = ["BA861", "BA863"],
        // LHR → Warsaw: BA848, BA850
        ["LHR-WAW"] = ["BA848", "BA850"],
        ["WAW-LHR"] = ["BA849", "BA851"],
        // LHR → Milan MXP: BA572, BA574, BA576, BA578
        ["LHR-MXP"] = ["BA572", "BA574", "BA576", "BA578"],
        ["MXP-LHR"] = ["BA573", "BA575", "BA577", "BA579"],
        // Rome FCO → New York (codeshare operated by BA): BA2015, BA2017
        ["FCO-JFK"] = ["BA2015", "BA2017"],
        ["JFK-FCO"] = ["BA2016", "BA2018"],
    };
}
