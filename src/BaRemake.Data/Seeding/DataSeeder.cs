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
            // UK domestic
            ("NCL", "Newcastle International Airport", "Newcastle", "United Kingdom", "GB", "Europe/London", 55.0375, -1.6917),
            ("LBA", "Leeds Bradford Airport", "Leeds", "United Kingdom", "GB", "Europe/London", 53.8659, -1.6606),
            ("LCY", "London City Airport", "London", "United Kingdom", "GB", "Europe/London", 51.5053, 0.0553),
            ("ABZ", "Aberdeen International Airport", "Aberdeen", "United Kingdom", "GB", "Europe/London", 57.2019, -2.1978),
            ("BFS", "Belfast International Airport", "Belfast", "United Kingdom", "GB", "Europe/London", 54.6575, -6.2158),
            ("BHD", "George Best Belfast City Airport", "Belfast", "United Kingdom", "GB", "Europe/London", 54.6181, -5.8725),
            ("SOU", "Southampton Airport", "Southampton", "United Kingdom", "GB", "Europe/London", 50.9503, -1.3567),
            ("EXT", "Exeter Airport", "Exeter", "United Kingdom", "GB", "Europe/London", 50.7344, -3.4139),
            ("LCY", "London City Airport", "London", "United Kingdom", "GB", "Europe/London", 51.5053, 0.0553),
            ("NWI", "Norwich Airport", "Norwich", "United Kingdom", "GB", "Europe/London", 52.6758, 1.2828),
            ("CWL", "Cardiff Airport", "Cardiff", "United Kingdom", "GB", "Europe/London", 51.3967, -3.3433),
            ("INV", "Inverness Airport", "Inverness", "United Kingdom", "GB", "Europe/London", 57.5425, -4.0475),
            // More Europe
            ("BER", "Berlin Brandenburg Airport", "Berlin", "Germany", "DE", "Europe/Berlin", 52.3667, 13.5033),
            ("GVA", "Geneva Airport", "Geneva", "Switzerland", "CH", "Europe/Zurich", 46.2381, 6.1089),
            ("BRU", "Brussels Airport", "Brussels", "Belgium", "BE", "Europe/Brussels", 50.9010, 4.4844),
            ("LYS", "Lyon-Saint Exupéry Airport", "Lyon", "France", "FR", "Europe/Paris", 45.7256, 5.0811),
            ("HEL", "Helsinki-Vantaa Airport", "Helsinki", "Finland", "FI", "Europe/Helsinki", 60.3172, 24.9633),
            ("HAM", "Hamburg Airport", "Hamburg", "Germany", "DE", "Europe/Berlin", 53.6304, 9.9882),
            ("DUS", "Düsseldorf Airport", "Düsseldorf", "Germany", "DE", "Europe/Berlin", 51.2895, 6.7668),
            ("NAP", "Naples International Airport", "Naples", "Italy", "IT", "Europe/Rome", 40.8860, 14.2908),
            ("OPO", "Porto Airport", "Porto", "Portugal", "PT", "Europe/Lisbon", 41.2481, -8.6814),
            ("SKG", "Thessaloniki Airport", "Thessaloniki", "Greece", "GR", "Europe/Athens", 40.5197, 22.9709),
            ("CFU", "Corfu Airport", "Corfu", "Greece", "GR", "Europe/Athens", 39.6019, 19.9117),
            ("KRK", "Kraków Airport", "Kraków", "Poland", "PL", "Europe/Warsaw", 50.0777, 19.7848),
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

            // LHR → new European cities
            ("LHR","BER",120), ("BER","LHR",115),
            ("LHR","GVA",100), ("GVA","LHR",95),
            ("LHR","BRU",80),  ("BRU","LHR",75),
            ("LHR","LYS",115), ("LYS","LHR",110),
            ("LHR","HEL",165), ("HEL","LHR",175),
            ("LHR","HAM",110), ("HAM","LHR",105),
            ("LHR","DUS",105), ("DUS","LHR",100),
            ("LHR","OPO",145), ("OPO","LHR",140),
            ("LHR","NAP",165), ("NAP","LHR",160),
            ("LHR","VCE",135), ("VCE","LHR",130),

            // UK domestic (LHR hub)
            ("LHR","EDI",80),  ("EDI","LHR",75),
            ("LHR","MAN",65),  ("MAN","LHR",60),
            ("LHR","GLA",90),  ("GLA","LHR",85),
            ("LHR","BHX",45),  ("BHX","LHR",45),
            ("LHR","NCL",75),  ("NCL","LHR",70),
            ("LHR","BRS",45),  ("BRS","LHR",45),
            ("LHR","ABZ",100), ("ABZ","LHR",95),
            ("LHR","BFS",75),  ("BFS","LHR",70),
            ("LHR","BHD",80),  ("BHD","LHR",75),
            ("LHR","LBA",60),  ("LBA","LHR",55),
            ("LHR","SOU",35),  ("SOU","LHR",35),
            ("LHR","CWL",45),  ("CWL","LHR",40),
            ("LHR","INV",105), ("INV","LHR",100),

            // London City Airport (LCY) - short-haul BA CityFlyer + easyJet
            ("LCY","EDI",75),  ("EDI","LCY",70),
            ("LCY","GLA",85),  ("GLA","LCY",80),
            ("LCY","BFS",75),  ("BFS","LCY",70),
            ("LCY","BHD",75),  ("BHD","LCY",70),
            ("LCY","ABZ",90),  ("ABZ","LCY",85),
            ("LCY","CDG",65),  ("CDG","LCY",60),
            ("LCY","AMS",60),  ("AMS","LCY",55),
            ("LCY","FRA",90),  ("FRA","LCY",85),
            ("LCY","DUB",70),  ("DUB","LCY",65),
            ("LCY","MAD",125), ("MAD","LCY",120),
            ("LCY","BCN",135), ("BCN","LCY",130),
            ("LCY","FCO",150), ("FCO","LCY",145),
            ("LCY","BER",110), ("BER","LCY",105),
            ("LCY","GVA",90),  ("GVA","LCY",85),

            // Belfast BFS (International) extra routes
            ("LGW","BFS",80),  ("BFS","LGW",75),
            ("STN","BFS",80),  ("BFS","STN",75),
            ("MAN","BFS",40),  ("BFS","MAN",40),
            ("BRS","BFS",60),  ("BFS","BRS",55),
            ("BHX","BFS",50),  ("BFS","BHX",50),
            ("EDI","BFS",55),  ("BFS","EDI",55),
            ("GLA","BFS",50),  ("BFS","GLA",50),

            // Belfast BHD (City) extra routes
            ("LGW","BHD",80),  ("BHD","LGW",75),
            ("STN","BHD",80),  ("BHD","STN",75),
            ("MAN","BHD",45),  ("BHD","MAN",45),
            ("BRS","BHD",60),  ("BHD","BRS",55),
            ("BHX","BHD",50),  ("BHD","BHX",50),
            ("EDI","BHD",60),  ("BHD","EDI",60),
            ("GLA","BHD",50),  ("BHD","GLA",50),
            ("NCL","BHD",60),  ("BHD","NCL",60),

            // Leeds Bradford extra routes
            ("LGW","LBA",65),  ("LBA","LGW",60),
            ("STN","LBA",70),  ("LBA","STN",65),
            ("MAN","LBA",25),  ("LBA","MAN",25),
            ("DUB","LBA",65),  ("LBA","DUB",60),
            ("LBA","BCN",135), ("BCN","LBA",130),
            ("LBA","AGP",160), ("AGP","LBA",155),
            ("LBA","PMI",150), ("PMI","LBA",145),
            ("LBA","TFS",245), ("TFS","LBA",240),
            ("LBA","LPA",240), ("LPA","LBA",235),
            ("LBA","FAO",155), ("FAO","LBA",150),
            ("LBA","NCE",120), ("NCE","LBA",115),
            ("LBA","FCO",165), ("FCO","LBA",160),
            ("LBA","ATH",225), ("ATH","LBA",230),

            // Newcastle extra routes
            ("LGW","NCL",75),  ("NCL","LGW",70),
            ("STN","NCL",75),  ("NCL","STN",70),
            ("DUB","NCL",60),  ("NCL","DUB",60),
            ("NCL","BCN",155), ("BCN","NCL",150),
            ("NCL","AGP",175), ("AGP","NCL",170),
            ("NCL","PMI",165), ("PMI","NCL",160),
            ("NCL","TFS",255), ("TFS","NCL",250),
            ("NCL","LPA",250), ("LPA","NCL",245),
            ("NCL","FAO",165), ("FAO","NCL",160),
            ("NCL","AMS",75),  ("AMS","NCL",70),
            ("NCL","CDG",85),  ("CDG","NCL",80),
            ("NCL","DXB",415), ("DXB","NCL",445),

            // Bristol extra routes
            ("LGW","BRS",40),  ("BRS","LGW",40),
            ("DUB","BRS",60),  ("BRS","DUB",60),
            ("BRS","BCN",135), ("BCN","BRS",130),
            ("BRS","AGP",165), ("AGP","BRS",160),
            ("BRS","PMI",155), ("PMI","BRS",150),
            ("BRS","TFS",250), ("TFS","BRS",245),
            ("BRS","LPA",245), ("LPA","BRS",240),
            ("BRS","FAO",155), ("FAO","BRS",150),
            ("BRS","NCE",125), ("NCE","BRS",120),
            ("BRS","AMS",80),  ("AMS","BRS",75),
            ("BRS","CDG",75),  ("CDG","BRS",70),
            ("BRS","DUB",55),  ("DUB","BRS",50),
            ("BRS","ATH",225), ("ATH","BRS",230),

            // Birmingham extra European
            ("BHX","BCN",145), ("BCN","BHX",140),
            ("BHX","AGP",165), ("AGP","BHX",160),
            ("BHX","PMI",155), ("PMI","BHX",150),
            ("BHX","TFS",250), ("TFS","BHX",245),
            ("BHX","LPA",245), ("LPA","BHX",240),
            ("BHX","FAO",155), ("FAO","BHX",150),
            ("BHX","DUB",55),  ("DUB","BHX",50),
            ("BHX","AMS",80),  ("AMS","BHX",75),
            ("BHX","CDG",80),  ("CDG","BHX",75),
            ("BHX","FCO",165), ("FCO","BHX",160),
            ("BHX","MAD",145), ("MAD","BHX",140),
            ("BHX","ATH",225), ("ATH","BHX",230),
            ("BHX","DXB",415), ("DXB","BHX",445),

            // Edinburgh extra European (easyJet hub)
            ("EDI","AMS",100), ("AMS","EDI",95),
            ("EDI","CDG",95),  ("CDG","EDI",90),
            ("EDI","BCN",155), ("BCN","EDI",150),
            ("EDI","FCO",175), ("FCO","EDI",170),
            ("EDI","MAD",155), ("MAD","EDI",150),
            ("EDI","AGP",175), ("AGP","EDI",170),
            ("EDI","PMI",165), ("PMI","EDI",160),
            ("EDI","TFS",265), ("TFS","EDI",260),
            ("EDI","LPA",260), ("LPA","EDI",255),
            ("EDI","DUB",60),  ("DUB","EDI",55),
            ("EDI","FAO",165), ("FAO","EDI",160),
            ("EDI","NCE",140), ("NCE","EDI",135),
            ("EDI","PRG",145), ("PRG","EDI",140),
            ("EDI","BUD",185), ("BUD","EDI",180),
            ("EDI","WAW",185), ("WAW","EDI",180),
            ("EDI","ATH",225), ("ATH","EDI",230),

            // Glasgow extra
            ("GLA","DUB",50),  ("DUB","GLA",50),
            ("GLA","BCN",160), ("BCN","GLA",155),
            ("GLA","PMI",170), ("PMI","GLA",165),
            ("GLA","TFS",270), ("TFS","GLA",265),
            ("GLA","LPA",265), ("LPA","GLA",260),
            ("GLA","AMS",105), ("AMS","GLA",100),
            ("GLA","CDG",100), ("CDG","GLA",95),
            ("GLA","FCO",180), ("FCO","GLA",175),

            // Cardiff routes
            ("CWL","DUB",55),  ("DUB","CWL",55),
            ("CWL","AMS",85),  ("AMS","CWL",80),
            ("CWL","CDG",80),  ("CDG","CWL",75),
            ("CWL","BCN",145), ("BCN","CWL",140),
            ("CWL","AGP",165), ("AGP","CWL",160),
            ("CWL","PMI",155), ("PMI","CWL",150),
            ("CWL","TFS",250), ("TFS","CWL",245),

            // Southampton routes
            ("SOU","DUB",75),  ("DUB","SOU",70),
            ("SOU","AMS",70),  ("AMS","SOU",65),
            ("SOU","CDG",60),  ("CDG","SOU",55),
            ("SOU","PMI",155), ("PMI","SOU",150),
            ("SOU","AGP",165), ("AGP","SOU",160),

            // MAN expanded European
            ("MAN","BER",130), ("BER","MAN",125),
            ("MAN","GVA",110), ("GVA","MAN",105),
            ("MAN","VIE",165), ("VIE","MAN",160),
            ("MAN","ZRH",115), ("ZRH","MAN",110),
            ("MAN","ATH",220), ("ATH","MAN",225),
            ("MAN","HER",220), ("HER","MAN",225),
            ("MAN","RHO",215), ("RHO","MAN",220),
            ("MAN","LIS",165), ("LIS","MAN",160),
            ("MAN","NCE",130), ("NCE","MAN",125),

            // LGW expanded
            ("LGW","BER",130), ("BER","LGW",125),
            ("LGW","GVA",105), ("GVA","LGW",100),
            ("LGW","BRU",80),  ("BRU","LGW",75),
            ("LGW","NAP",175), ("NAP","LGW",170),
            ("LGW","OPO",150), ("OPO","LGW",145),
            ("LGW","SKG",225), ("SKG","LGW",230),
            ("LGW","CFU",210), ("CFU","LGW",215),
            ("LGW","KRK",175), ("KRK","LGW",170),

            // STN expanded
            ("STN","BER",130), ("BER","STN",125),
            ("STN","GVA",100), ("GVA","STN",95),
            ("STN","PRG",130), ("PRG","STN",125),
            ("STN","KRK",175), ("KRK","STN",170),
            ("STN","BUD",175), ("BUD","STN",170),
            ("STN","WAW",175), ("WAW","STN",170),
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

                for (int day = 0; day < 365; day++)
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

        // ── LHR new European schedules
        ["LHR-BER"] = new() { new("BA", "Airbus A320neo", [6, 9, 13, 17, 20], 109, 469), new("U2", "Airbus A320neo", [7, 12, 19], 49, 0, true) },
        ["BER-LHR"] = new() { new("BA", "Airbus A320neo", [7, 10, 14, 18, 21], 109, 469), new("U2", "Airbus A320neo", [8, 13, 20], 49, 0, true) },
        ["LHR-GVA"] = new() { new("BA", "Airbus A319", [7, 10, 13, 17, 20], 99, 449), new("U2", "Airbus A319", [8, 14, 19], 55, 0, true) },
        ["GVA-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 14, 18, 21], 99, 449), new("U2", "Airbus A319", [9, 15, 20], 55, 0, true) },
        ["LHR-BRU"] = new() { new("BA", "Airbus A319", [6, 8, 11, 14, 17, 20], 79, 349), new("U2", "Airbus A319", [7, 12, 19], 45, 0, true) },
        ["BRU-LHR"] = new() { new("BA", "Airbus A319", [7, 9, 12, 15, 18, 21], 79, 349), new("U2", "Airbus A319", [8, 13, 20], 45, 0, true) },
        ["LHR-LYS"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 99, 429) },
        ["LYS-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 99, 429) },
        ["LHR-HEL"] = new() { new("BA", "Airbus A319", [7, 13, 19], 129, 529) },
        ["HEL-LHR"] = new() { new("BA", "Airbus A319", [8, 14, 20], 129, 529) },
        ["LHR-HAM"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 109, 469) },
        ["HAM-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 109, 469) },
        ["LHR-DUS"] = new() { new("BA", "Airbus A319", [7, 10, 13, 17, 20], 99, 449) },
        ["DUS-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 14, 18, 21], 99, 449) },
        ["LHR-OPO"] = new() { new("BA", "Airbus A320neo", [7, 12, 18], 99, 429), new("U2", "Airbus A320neo", [8, 16], 49, 0, true) },
        ["OPO-LHR"] = new() { new("BA", "Airbus A320neo", [8, 13, 19], 99, 429), new("U2", "Airbus A320neo", [9, 17], 49, 0, true) },
        ["LHR-NAP"] = new() { new("BA", "Airbus A321neo", [7, 14], 129, 549), new("U2", "Airbus A320neo", [8, 17], 65, 0, true) },
        ["NAP-LHR"] = new() { new("BA", "Airbus A321neo", [8, 15], 129, 549), new("U2", "Airbus A320neo", [9, 18], 65, 0, true) },
        ["LHR-VCE"] = new() { new("BA", "Airbus A321neo", [7, 12, 17], 119, 499), new("U2", "Airbus A320neo", [8, 15], 59, 0, true) },
        ["VCE-LHR"] = new() { new("BA", "Airbus A321neo", [8, 13, 18], 119, 499), new("U2", "Airbus A320neo", [9, 16], 59, 0, true) },

        // ── LHR UK domestic schedules
        ["LHR-EDI"] = new() { new("BA", "Airbus A319", [6, 8, 10, 12, 14, 16, 18, 20], 59, 239) },
        ["EDI-LHR"] = new() { new("BA", "Airbus A319", [7, 9, 11, 13, 15, 17, 19, 21], 59, 239) },
        ["LHR-MAN"] = new() { new("BA", "Airbus A319", [6, 8, 10, 12, 14, 16, 18, 20], 59, 199) },
        ["MAN-LHR"] = new() { new("BA", "Airbus A319", [7, 9, 11, 13, 15, 17, 19, 21], 59, 199) },
        ["LHR-GLA"] = new() { new("BA", "Airbus A319", [6, 9, 12, 15, 18, 21], 59, 239) },
        ["GLA-LHR"] = new() { new("BA", "Airbus A319", [7, 10, 13, 16, 19, 22], 59, 239) },
        ["LHR-BHX"] = new() { new("BA", "Airbus A319", [6, 8, 10, 12, 14, 16, 18, 20, 22], 45, 179) },
        ["BHX-LHR"] = new() { new("BA", "Airbus A319", [7, 9, 11, 13, 15, 17, 19, 21, 23], 45, 179) },
        ["LHR-NCL"] = new() { new("BA", "Airbus A319", [7, 10, 13, 17, 20], 55, 219) },
        ["NCL-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 14, 18, 21], 55, 219) },
        ["LHR-BRS"] = new() { new("BA", "Airbus A319", [6, 9, 12, 15, 18, 21], 45, 179) },
        ["BRS-LHR"] = new() { new("BA", "Airbus A319", [7, 10, 13, 16, 19, 22], 45, 179) },
        ["LHR-ABZ"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 65, 249) },
        ["ABZ-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 65, 249) },
        ["LHR-BFS"] = new() { new("BA", "Airbus A319", [7, 10, 13, 17, 20], 59, 229) },
        ["BFS-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 14, 18, 21], 59, 229) },
        ["LHR-BHD"] = new() { new("BA", "Airbus A319", [7, 11, 15, 19], 59, 229) },
        ["BHD-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 16, 20], 59, 229) },
        ["LHR-LBA"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 49, 199) },
        ["LBA-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 49, 199) },
        ["LHR-SOU"] = new() { new("BA", "Airbus A319", [7, 10, 13, 16, 19], 39, 159) },
        ["SOU-LHR"] = new() { new("BA", "Airbus A319", [8, 11, 14, 17, 20], 39, 159) },
        ["LHR-CWL"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 45, 179) },
        ["CWL-LHR"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 45, 179) },
        ["LHR-INV"] = new() { new("BA", "Airbus A319", [7, 13, 19], 75, 259) },
        ["INV-LHR"] = new() { new("BA", "Airbus A319", [8, 14, 20], 75, 259) },

        // ── London City Airport (BA CityFlyer + easyJet)
        ["LCY-EDI"] = new() { new("BA", "Airbus A319", [7, 9, 13, 17, 19], 55, 219), new("U2", "Airbus A319", [8, 14, 20], 39, 0, true) },
        ["EDI-LCY"] = new() { new("BA", "Airbus A319", [8, 10, 14, 18, 20], 55, 219), new("U2", "Airbus A319", [9, 15, 21], 39, 0, true) },
        ["LCY-GLA"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 55, 229) },
        ["GLA-LCY"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 55, 229) },
        ["LCY-BFS"] = new() { new("BA", "Airbus A319", [7, 11, 16], 55, 219), new("U2", "Airbus A319", [8, 14], 35, 0, true) },
        ["BFS-LCY"] = new() { new("BA", "Airbus A319", [8, 12, 17], 55, 219), new("U2", "Airbus A319", [9, 15], 35, 0, true) },
        ["LCY-BHD"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 55, 219), new("U2", "Airbus A319", [8, 14], 35, 0, true) },
        ["BHD-LCY"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 55, 219), new("U2", "Airbus A319", [9, 15], 35, 0, true) },
        ["LCY-ABZ"] = new() { new("BA", "Airbus A319", [7, 13, 19], 65, 249) },
        ["ABZ-LCY"] = new() { new("BA", "Airbus A319", [8, 14, 20], 65, 249) },
        ["LCY-CDG"] = new() { new("BA", "Airbus A319", [6, 9, 12, 15, 18, 20], 75, 319), new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["CDG-LCY"] = new() { new("BA", "Airbus A319", [7, 10, 13, 16, 19, 21], 75, 319), new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["LCY-AMS"] = new() { new("BA", "Airbus A319", [6, 9, 12, 16, 19], 69, 299), new("U2", "Airbus A319", [7, 13, 18], 39, 0, true) },
        ["AMS-LCY"] = new() { new("BA", "Airbus A319", [7, 10, 13, 17, 20], 69, 299), new("U2", "Airbus A319", [8, 14, 19], 39, 0, true) },
        ["LCY-FRA"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 89, 389) },
        ["FRA-LCY"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 89, 389) },
        ["LCY-DUB"] = new() { new("BA", "Airbus A319", [6, 9, 12, 15, 18, 21], 65, 279), new("U2", "Airbus A319", [7, 11, 16, 20], 35, 0, true) },
        ["DUB-LCY"] = new() { new("BA", "Airbus A319", [7, 10, 13, 16, 19, 22], 65, 279), new("U2", "Airbus A319", [8, 12, 17, 21], 35, 0, true) },
        ["LCY-MAD"] = new() { new("BA", "Airbus A320neo", [7, 13, 19], 109, 469) },
        ["MAD-LCY"] = new() { new("BA", "Airbus A320neo", [8, 14, 20], 109, 469) },
        ["LCY-BCN"] = new() { new("BA", "Airbus A320neo", [7, 13, 19], 109, 469), new("U2", "Airbus A320neo", [8, 16], 55, 0, true) },
        ["BCN-LCY"] = new() { new("BA", "Airbus A320neo", [8, 14, 20], 109, 469), new("U2", "Airbus A320neo", [9, 17], 55, 0, true) },
        ["LCY-FCO"] = new() { new("BA", "Airbus A320neo", [7, 12, 18], 119, 499) },
        ["FCO-LCY"] = new() { new("BA", "Airbus A320neo", [8, 13, 19], 119, 499) },
        ["LCY-BER"] = new() { new("BA", "Airbus A319", [7, 13, 19], 99, 429), new("U2", "Airbus A319", [8, 16], 49, 0, true) },
        ["BER-LCY"] = new() { new("BA", "Airbus A319", [8, 14, 20], 99, 429), new("U2", "Airbus A319", [9, 17], 49, 0, true) },
        ["LCY-GVA"] = new() { new("BA", "Airbus A319", [7, 11, 16, 20], 89, 389), new("U2", "Airbus A319", [8, 15], 49, 0, true) },
        ["GVA-LCY"] = new() { new("BA", "Airbus A319", [8, 12, 17, 21], 89, 389), new("U2", "Airbus A319", [9, 16], 49, 0, true) },

        // ── Belfast BFS extra (easyJet + Ryanair)
        ["LGW-BFS"] = new() { new("U2", "Airbus A319", [6, 9, 12, 15, 18, 21], 35, 0, true), new("BA", "Airbus A319", [7, 14], 69, 279) },
        ["BFS-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 13, 16, 19, 22], 35, 0, true), new("BA", "Airbus A319", [8, 15], 69, 279) },
        ["STN-BFS"] = new() { new("FR", "Boeing 737-800", [6, 9, 13, 17, 20], 19, 0, true) },
        ["BFS-STN"] = new() { new("FR", "Boeing 737-800", [7, 10, 14, 18, 21], 19, 0, true) },
        ["MAN-BFS"] = new() { new("BA", "Airbus A319", [7, 10, 14, 17, 20], 55, 219), new("U2", "Airbus A319", [8, 13, 19], 35, 0, true) },
        ["BFS-MAN"] = new() { new("BA", "Airbus A319", [8, 11, 15, 18, 21], 55, 219), new("U2", "Airbus A319", [9, 14, 20], 35, 0, true) },
        ["BRS-BFS"] = new() { new("U2", "Airbus A319", [7, 13, 19], 39, 0, true) },
        ["BFS-BRS"] = new() { new("U2", "Airbus A319", [8, 14, 20], 39, 0, true) },
        ["BHX-BFS"] = new() { new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["BFS-BHX"] = new() { new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["EDI-BFS"] = new() { new("U2", "Airbus A319", [7, 13, 18], 35, 0, true) },
        ["BFS-EDI"] = new() { new("U2", "Airbus A319", [8, 14, 19], 35, 0, true) },
        ["GLA-BFS"] = new() { new("U2", "Airbus A319", [8, 14, 19], 35, 0, true) },
        ["BFS-GLA"] = new() { new("U2", "Airbus A319", [9, 15, 20], 35, 0, true) },

        // ── Belfast BHD extra (easyJet + BA)
        ["LGW-BHD"] = new() { new("U2", "Airbus A319", [6, 9, 12, 15, 18, 21], 35, 0, true) },
        ["BHD-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 13, 16, 19, 22], 35, 0, true) },
        ["STN-BHD"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18, 21], 19, 0, true) },
        ["BHD-STN"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19, 22], 19, 0, true) },
        ["MAN-BHD"] = new() { new("U2", "Airbus A319", [7, 11, 15, 19], 35, 0, true), new("BA", "Airbus A319", [8, 14, 20], 55, 219) },
        ["BHD-MAN"] = new() { new("U2", "Airbus A319", [8, 12, 16, 20], 35, 0, true), new("BA", "Airbus A319", [9, 15, 21], 55, 219) },
        ["BRS-BHD"] = new() { new("U2", "Airbus A319", [7, 13, 19], 39, 0, true) },
        ["BHD-BRS"] = new() { new("U2", "Airbus A319", [8, 14, 20], 39, 0, true) },
        ["BHX-BHD"] = new() { new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["BHD-BHX"] = new() { new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["EDI-BHD"] = new() { new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["BHD-EDI"] = new() { new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["GLA-BHD"] = new() { new("U2", "Airbus A319", [8, 14, 19], 35, 0, true) },
        ["BHD-GLA"] = new() { new("U2", "Airbus A319", [9, 15, 20], 35, 0, true) },
        ["NCL-BHD"] = new() { new("U2", "Airbus A319", [7, 13, 18], 35, 0, true) },
        ["BHD-NCL"] = new() { new("U2", "Airbus A319", [8, 14, 19], 35, 0, true) },

        // ── Leeds Bradford (U2 + FR holiday hub)
        ["LGW-LBA"] = new() { new("U2", "Airbus A319", [6, 10, 14, 18], 39, 0, true) },
        ["LBA-LGW"] = new() { new("U2", "Airbus A319", [7, 11, 15, 19], 39, 0, true) },
        ["STN-LBA"] = new() { new("FR", "Boeing 737-800", [6, 11, 16], 19, 0, true) },
        ["LBA-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17], 19, 0, true) },
        ["MAN-LBA"] = new() { new("U2", "Airbus A319", [7, 11, 15, 19, 21], 25, 0, true) },
        ["LBA-MAN"] = new() { new("U2", "Airbus A319", [8, 12, 16, 20, 22], 25, 0, true) },
        ["DUB-LBA"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 25, 0, true) },
        ["LBA-DUB"] = new() { new("FR", "Boeing 737-800", [8, 14, 20], 25, 0, true) },
        ["LBA-BCN"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 55, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["BCN-LBA"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 55, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["LBA-AGP"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true), new("FR", "Boeing 737-800", [7, 15], 35, 0, true) },
        ["AGP-LBA"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true), new("FR", "Boeing 737-800", [8, 16], 35, 0, true) },
        ["LBA-PMI"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 55, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["PMI-LBA"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 55, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["LBA-TFS"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true), new("FR", "Boeing 737-800", [7, 16], 49, 0, true) },
        ["TFS-LBA"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true), new("FR", "Boeing 737-800", [8, 17], 49, 0, true) },
        ["LBA-LPA"] = new() { new("U2", "Airbus A320neo", [6, 16], 99, 0, true) },
        ["LPA-LBA"] = new() { new("U2", "Airbus A320neo", [7, 17], 99, 0, true) },
        ["LBA-FAO"] = new() { new("U2", "Airbus A320neo", [6, 14], 69, 0, true), new("FR", "Boeing 737-800", [7, 15], 39, 0, true) },
        ["FAO-LBA"] = new() { new("U2", "Airbus A320neo", [7, 15], 69, 0, true), new("FR", "Boeing 737-800", [8, 16], 39, 0, true) },
        ["LBA-NCE"] = new() { new("U2", "Airbus A320neo", [7, 15], 79, 0, true) },
        ["NCE-LBA"] = new() { new("U2", "Airbus A320neo", [8, 16], 79, 0, true) },
        ["LBA-FCO"] = new() { new("U2", "Airbus A320neo", [6, 14], 75, 0, true) },
        ["FCO-LBA"] = new() { new("U2", "Airbus A320neo", [7, 15], 75, 0, true) },
        ["LBA-ATH"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true) },
        ["ATH-LBA"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true) },

        // ── Newcastle extra routes (U2 + FR + BA)
        ["LGW-NCL"] = new() { new("U2", "Airbus A319", [6, 10, 14, 18], 39, 0, true) },
        ["NCL-LGW"] = new() { new("U2", "Airbus A319", [7, 11, 15, 19], 39, 0, true) },
        ["STN-NCL"] = new() { new("FR", "Boeing 737-800", [6, 11, 16], 25, 0, true) },
        ["NCL-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17], 25, 0, true) },
        ["DUB-NCL"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 25, 0, true), new("U2", "Airbus A319", [8, 15], 35, 0, true) },
        ["NCL-DUB"] = new() { new("FR", "Boeing 737-800", [8, 14, 20], 25, 0, true), new("U2", "Airbus A319", [9, 16], 35, 0, true) },
        ["NCL-BCN"] = new() { new("U2", "Airbus A320neo", [6, 13], 59, 0, true), new("FR", "Boeing 737-800", [7, 15], 29, 0, true) },
        ["BCN-NCL"] = new() { new("U2", "Airbus A320neo", [7, 14], 59, 0, true), new("FR", "Boeing 737-800", [8, 16], 29, 0, true) },
        ["NCL-AGP"] = new() { new("U2", "Airbus A320neo", [6, 15], 69, 0, true), new("FR", "Boeing 737-800", [7, 16], 35, 0, true) },
        ["AGP-NCL"] = new() { new("U2", "Airbus A320neo", [7, 16], 69, 0, true), new("FR", "Boeing 737-800", [8, 17], 35, 0, true) },
        ["NCL-PMI"] = new() { new("U2", "Airbus A320neo", [6, 14], 59, 0, true), new("FR", "Boeing 737-800", [7, 15], 29, 0, true) },
        ["PMI-NCL"] = new() { new("U2", "Airbus A320neo", [7, 15], 59, 0, true), new("FR", "Boeing 737-800", [8, 16], 29, 0, true) },
        ["NCL-TFS"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true) },
        ["TFS-NCL"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true) },
        ["NCL-LPA"] = new() { new("U2", "Airbus A320neo", [6, 16], 99, 0, true) },
        ["LPA-NCL"] = new() { new("U2", "Airbus A320neo", [7, 17], 99, 0, true) },
        ["NCL-FAO"] = new() { new("U2", "Airbus A320neo", [7, 15], 75, 0, true) },
        ["FAO-NCL"] = new() { new("U2", "Airbus A320neo", [8, 16], 75, 0, true) },
        ["NCL-AMS"] = new() { new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["AMS-NCL"] = new() { new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["NCL-CDG"] = new() { new("U2", "Airbus A319", [7, 14], 55, 0, true) },
        ["CDG-NCL"] = new() { new("U2", "Airbus A319", [8, 15], 55, 0, true) },
        ["NCL-DXB"] = new() { new("EK", "Airbus A380-800", [8, 21], 299, 1299) },
        ["DXB-NCL"] = new() { new("EK", "Airbus A380-800", [3, 14], 299, 1299) },

        // ── Bristol extra (U2 hub)
        ["LGW-BRS"] = new() { new("U2", "Airbus A319", [6, 9, 12, 16, 19], 29, 0, true) },
        ["BRS-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 13, 17, 20], 29, 0, true) },
        ["DUB-BRS"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18, 21], 25, 0, true), new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["BRS-DUB"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19, 22], 25, 0, true), new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["BRS-BCN"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 55, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["BCN-BRS"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 55, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["BRS-AGP"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true), new("FR", "Boeing 737-800", [7, 15], 35, 0, true) },
        ["AGP-BRS"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true), new("FR", "Boeing 737-800", [8, 16], 35, 0, true) },
        ["BRS-PMI"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 59, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["PMI-BRS"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 59, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["BRS-TFS"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true) },
        ["TFS-BRS"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true) },
        ["BRS-LPA"] = new() { new("U2", "Airbus A320neo", [6, 16], 99, 0, true) },
        ["LPA-BRS"] = new() { new("U2", "Airbus A320neo", [7, 17], 99, 0, true) },
        ["BRS-FAO"] = new() { new("U2", "Airbus A320neo", [7, 14], 69, 0, true), new("FR", "Boeing 737-800", [8, 15], 39, 0, true) },
        ["FAO-BRS"] = new() { new("U2", "Airbus A320neo", [8, 15], 69, 0, true), new("FR", "Boeing 737-800", [9, 16], 39, 0, true) },
        ["BRS-NCE"] = new() { new("U2", "Airbus A320neo", [7, 15], 79, 0, true) },
        ["NCE-BRS"] = new() { new("U2", "Airbus A320neo", [8, 16], 79, 0, true) },
        ["BRS-AMS"] = new() { new("U2", "Airbus A319", [7, 12, 18], 39, 0, true) },
        ["AMS-BRS"] = new() { new("U2", "Airbus A319", [8, 13, 19], 39, 0, true) },
        ["BRS-CDG"] = new() { new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["CDG-BRS"] = new() { new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["BRS-ATH"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true) },
        ["ATH-BRS"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true) },

        // ── Birmingham extra (U2 + FR + BA + TUI)
        ["BHX-BCN"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 59, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["BCN-BHX"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 59, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["BHX-AGP"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true), new("FR", "Boeing 737-800", [7, 15], 35, 0, true) },
        ["AGP-BHX"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true), new("FR", "Boeing 737-800", [8, 16], 35, 0, true) },
        ["BHX-PMI"] = new() { new("U2", "Airbus A320neo", [6, 12, 18], 59, 0, true), new("FR", "Boeing 737-800", [7, 14], 29, 0, true) },
        ["PMI-BHX"] = new() { new("U2", "Airbus A320neo", [7, 13, 19], 59, 0, true), new("FR", "Boeing 737-800", [8, 15], 29, 0, true) },
        ["BHX-TFS"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true), new("FR", "Boeing 737-800", [7, 16], 49, 0, true) },
        ["TFS-BHX"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true), new("FR", "Boeing 737-800", [8, 17], 49, 0, true) },
        ["BHX-LPA"] = new() { new("U2", "Airbus A320neo", [6, 16], 99, 0, true) },
        ["LPA-BHX"] = new() { new("U2", "Airbus A320neo", [7, 17], 99, 0, true) },
        ["BHX-FAO"] = new() { new("U2", "Airbus A320neo", [7, 14], 69, 0, true), new("FR", "Boeing 737-800", [8, 15], 39, 0, true) },
        ["FAO-BHX"] = new() { new("U2", "Airbus A320neo", [8, 15], 69, 0, true), new("FR", "Boeing 737-800", [9, 16], 39, 0, true) },
        ["BHX-DUB"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18, 21], 19, 0, true), new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["DUB-BHX"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19, 22], 19, 0, true), new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["BHX-AMS"] = new() { new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["AMS-BHX"] = new() { new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["BHX-CDG"] = new() { new("U2", "Airbus A319", [7, 13, 19], 49, 0, true) },
        ["CDG-BHX"] = new() { new("U2", "Airbus A319", [8, 14, 20], 49, 0, true) },
        ["BHX-FCO"] = new() { new("U2", "Airbus A320neo", [6, 14], 69, 0, true) },
        ["FCO-BHX"] = new() { new("U2", "Airbus A320neo", [7, 15], 69, 0, true) },
        ["BHX-MAD"] = new() { new("U2", "Airbus A320neo", [7, 14], 65, 0, true) },
        ["MAD-BHX"] = new() { new("U2", "Airbus A320neo", [8, 15], 65, 0, true) },
        ["BHX-ATH"] = new() { new("U2", "Airbus A320neo", [6, 15], 99, 0, true) },
        ["ATH-BHX"] = new() { new("U2", "Airbus A320neo", [7, 16], 99, 0, true) },
        ["BHX-DXB"] = new() { new("EK", "Airbus A380-800", [8, 21], 299, 1299) },
        ["DXB-BHX"] = new() { new("EK", "Airbus A380-800", [3, 14], 299, 1299) },

        // ── Edinburgh extra European (U2 hub)
        ["EDI-AMS"] = new() { new("U2", "Airbus A319", [6, 11, 16, 20], 49, 0, true) },
        ["AMS-EDI"] = new() { new("U2", "Airbus A319", [7, 12, 17, 21], 49, 0, true) },
        ["EDI-CDG"] = new() { new("U2", "Airbus A319", [7, 13, 19], 55, 0, true) },
        ["CDG-EDI"] = new() { new("U2", "Airbus A319", [8, 14, 20], 55, 0, true) },
        ["EDI-BCN"] = new() { new("U2", "Airbus A320neo", [6, 13, 19], 65, 0, true), new("FR", "Boeing 737-800", [7, 15], 29, 0, true) },
        ["BCN-EDI"] = new() { new("U2", "Airbus A320neo", [7, 14, 20], 65, 0, true), new("FR", "Boeing 737-800", [8, 16], 29, 0, true) },
        ["EDI-FCO"] = new() { new("U2", "Airbus A320neo", [6, 15], 75, 0, true) },
        ["FCO-EDI"] = new() { new("U2", "Airbus A320neo", [7, 16], 75, 0, true) },
        ["EDI-MAD"] = new() { new("U2", "Airbus A320neo", [7, 15], 75, 0, true) },
        ["MAD-EDI"] = new() { new("U2", "Airbus A320neo", [8, 16], 75, 0, true) },
        ["EDI-AGP"] = new() { new("U2", "Airbus A320neo", [6, 15], 79, 0, true) },
        ["AGP-EDI"] = new() { new("U2", "Airbus A320neo", [7, 16], 79, 0, true) },
        ["EDI-PMI"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true) },
        ["PMI-EDI"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true) },
        ["EDI-TFS"] = new() { new("U2", "Airbus A320neo", [6, 16], 109, 0, true) },
        ["TFS-EDI"] = new() { new("U2", "Airbus A320neo", [7, 17], 109, 0, true) },
        ["EDI-LPA"] = new() { new("U2", "Airbus A320neo", [6, 17], 109, 0, true) },
        ["LPA-EDI"] = new() { new("U2", "Airbus A320neo", [7, 18], 109, 0, true) },
        ["EDI-DUB"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18, 21], 19, 0, true), new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["DUB-EDI"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19, 22], 19, 0, true), new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["EDI-FAO"] = new() { new("U2", "Airbus A320neo", [7, 16], 79, 0, true) },
        ["FAO-EDI"] = new() { new("U2", "Airbus A320neo", [8, 17], 79, 0, true) },
        ["EDI-NCE"] = new() { new("U2", "Airbus A320neo", [7, 16], 79, 0, true) },
        ["NCE-EDI"] = new() { new("U2", "Airbus A320neo", [8, 17], 79, 0, true) },
        ["EDI-PRG"] = new() { new("U2", "Airbus A320neo", [7, 15], 69, 0, true) },
        ["PRG-EDI"] = new() { new("U2", "Airbus A320neo", [8, 16], 69, 0, true) },
        ["EDI-BUD"] = new() { new("W6", "Airbus A320neo", [7, 16], 49, 0, true) },
        ["BUD-EDI"] = new() { new("W6", "Airbus A320neo", [8, 17], 49, 0, true) },
        ["EDI-WAW"] = new() { new("W6", "Airbus A320neo", [7, 16], 49, 0, true) },
        ["WAW-EDI"] = new() { new("W6", "Airbus A320neo", [8, 17], 49, 0, true) },
        ["EDI-ATH"] = new() { new("U2", "Airbus A320neo", [6, 16], 109, 0, true) },
        ["ATH-EDI"] = new() { new("U2", "Airbus A320neo", [7, 17], 109, 0, true) },

        // ── Glasgow extra (U2 + FR + BA)
        ["GLA-DUB"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18], 19, 0, true), new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["DUB-GLA"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19], 19, 0, true), new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["GLA-BCN"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true), new("FR", "Boeing 737-800", [7, 15], 29, 0, true) },
        ["BCN-GLA"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true), new("FR", "Boeing 737-800", [8, 16], 29, 0, true) },
        ["GLA-PMI"] = new() { new("U2", "Airbus A320neo", [6, 14], 65, 0, true) },
        ["PMI-GLA"] = new() { new("U2", "Airbus A320neo", [7, 15], 65, 0, true) },
        ["GLA-TFS"] = new() { new("U2", "Airbus A320neo", [6, 16], 109, 0, true) },
        ["TFS-GLA"] = new() { new("U2", "Airbus A320neo", [7, 17], 109, 0, true) },
        ["GLA-LPA"] = new() { new("U2", "Airbus A320neo", [6, 16], 109, 0, true) },
        ["LPA-GLA"] = new() { new("U2", "Airbus A320neo", [7, 17], 109, 0, true) },
        ["GLA-AMS"] = new() { new("U2", "Airbus A319", [7, 14], 55, 0, true) },
        ["AMS-GLA"] = new() { new("U2", "Airbus A319", [8, 15], 55, 0, true) },
        ["GLA-CDG"] = new() { new("U2", "Airbus A319", [7, 14], 59, 0, true) },
        ["CDG-GLA"] = new() { new("U2", "Airbus A319", [8, 15], 59, 0, true) },
        ["GLA-FCO"] = new() { new("U2", "Airbus A320neo", [7, 16], 75, 0, true) },
        ["FCO-GLA"] = new() { new("U2", "Airbus A320neo", [8, 17], 75, 0, true) },

        // ── Cardiff (U2 + FR holiday)
        ["CWL-DUB"] = new() { new("U2", "Airbus A319", [7, 13, 19], 35, 0, true), new("FR", "Boeing 737-800", [8, 15], 19, 0, true) },
        ["DUB-CWL"] = new() { new("U2", "Airbus A319", [8, 14, 20], 35, 0, true), new("FR", "Boeing 737-800", [9, 16], 19, 0, true) },
        ["CWL-AMS"] = new() { new("U2", "Airbus A319", [7, 14], 45, 0, true) },
        ["AMS-CWL"] = new() { new("U2", "Airbus A319", [8, 15], 45, 0, true) },
        ["CWL-CDG"] = new() { new("U2", "Airbus A319", [7, 14], 49, 0, true) },
        ["CDG-CWL"] = new() { new("U2", "Airbus A319", [8, 15], 49, 0, true) },
        ["CWL-BCN"] = new() { new("U2", "Airbus A320neo", [6, 14], 59, 0, true) },
        ["BCN-CWL"] = new() { new("U2", "Airbus A320neo", [7, 15], 59, 0, true) },
        ["CWL-AGP"] = new() { new("U2", "Airbus A320neo", [6, 15], 65, 0, true) },
        ["AGP-CWL"] = new() { new("U2", "Airbus A320neo", [7, 16], 65, 0, true) },
        ["CWL-PMI"] = new() { new("U2", "Airbus A320neo", [6, 14], 59, 0, true) },
        ["PMI-CWL"] = new() { new("U2", "Airbus A320neo", [7, 15], 59, 0, true) },
        ["CWL-TFS"] = new() { new("U2", "Airbus A320neo", [6, 16], 99, 0, true) },
        ["TFS-CWL"] = new() { new("U2", "Airbus A320neo", [7, 17], 99, 0, true) },

        // ── Southampton (U2)
        ["SOU-DUB"] = new() { new("U2", "Airbus A319", [7, 13, 19], 35, 0, true) },
        ["DUB-SOU"] = new() { new("U2", "Airbus A319", [8, 14, 20], 35, 0, true) },
        ["SOU-AMS"] = new() { new("U2", "Airbus A319", [7, 13], 45, 0, true) },
        ["AMS-SOU"] = new() { new("U2", "Airbus A319", [8, 14], 45, 0, true) },
        ["SOU-CDG"] = new() { new("U2", "Airbus A319", [7, 13, 19], 45, 0, true) },
        ["CDG-SOU"] = new() { new("U2", "Airbus A319", [8, 14, 20], 45, 0, true) },
        ["SOU-PMI"] = new() { new("U2", "Airbus A320neo", [6, 14], 55, 0, true) },
        ["PMI-SOU"] = new() { new("U2", "Airbus A320neo", [7, 15], 55, 0, true) },
        ["SOU-AGP"] = new() { new("U2", "Airbus A320neo", [6, 15], 65, 0, true) },
        ["AGP-SOU"] = new() { new("U2", "Airbus A320neo", [7, 16], 65, 0, true) },

        // ── MAN new European
        ["MAN-BER"] = new() { new("U2", "Airbus A320neo", [7, 14], 59, 0, true), new("BA", "Airbus A319", [8, 17], 119, 499) },
        ["BER-MAN"] = new() { new("U2", "Airbus A320neo", [8, 15], 59, 0, true), new("BA", "Airbus A319", [9, 18], 119, 499) },
        ["MAN-GVA"] = new() { new("U2", "Airbus A320neo", [7, 14, 19], 65, 0, true) },
        ["GVA-MAN"] = new() { new("U2", "Airbus A320neo", [8, 15, 20], 65, 0, true) },
        ["MAN-VIE"] = new() { new("BA", "Airbus A319", [7, 14], 129, 499), new("W6", "Airbus A320neo", [8, 17], 55, 0, true) },
        ["VIE-MAN"] = new() { new("BA", "Airbus A319", [8, 15], 129, 499), new("W6", "Airbus A320neo", [9, 18], 55, 0, true) },
        ["MAN-ZRH"] = new() { new("U2", "Airbus A320neo", [7, 14], 79, 0, true) },
        ["ZRH-MAN"] = new() { new("U2", "Airbus A320neo", [8, 15], 79, 0, true) },
        ["MAN-ATH"] = new() { new("U2", "Airbus A320neo", [6, 15], 109, 0, true), new("FR", "Boeing 737-800", [7, 16], 49, 0, true) },
        ["ATH-MAN"] = new() { new("U2", "Airbus A320neo", [7, 16], 109, 0, true), new("FR", "Boeing 737-800", [8, 17], 49, 0, true) },
        ["MAN-HER"] = new() { new("U2", "Airbus A320neo", [6, 15], 109, 0, true) },
        ["HER-MAN"] = new() { new("U2", "Airbus A320neo", [7, 16], 109, 0, true) },
        ["MAN-RHO"] = new() { new("U2", "Airbus A320neo", [6, 15], 109, 0, true) },
        ["RHO-MAN"] = new() { new("U2", "Airbus A320neo", [7, 16], 109, 0, true) },
        ["MAN-LIS"] = new() { new("U2", "Airbus A320neo", [7, 15], 89, 0, true), new("TP", "Airbus A320neo", [8, 16], 99, 399) },
        ["LIS-MAN"] = new() { new("U2", "Airbus A320neo", [8, 16], 89, 0, true), new("TP", "Airbus A320neo", [9, 17], 99, 399) },
        ["MAN-NCE"] = new() { new("U2", "Airbus A320neo", [7, 15], 79, 0, true) },
        ["NCE-MAN"] = new() { new("U2", "Airbus A320neo", [8, 16], 79, 0, true) },

        // ── LGW new European (U2 + FR)
        ["LGW-BER"] = new() { new("U2", "Airbus A320neo", [6, 10, 14, 18], 49, 0, true), new("FR", "Boeing 737-800", [7, 13, 19], 25, 0, true) },
        ["BER-LGW"] = new() { new("U2", "Airbus A320neo", [7, 11, 15, 19], 49, 0, true), new("FR", "Boeing 737-800", [8, 14, 20], 25, 0, true) },
        ["LGW-GVA"] = new() { new("U2", "Airbus A320neo", [6, 10, 14, 18], 55, 0, true) },
        ["GVA-LGW"] = new() { new("U2", "Airbus A320neo", [7, 11, 15, 19], 55, 0, true) },
        ["LGW-BRU"] = new() { new("U2", "Airbus A319", [6, 9, 13, 17, 20], 39, 0, true), new("FR", "Boeing 737-800", [7, 12, 18], 19, 0, true) },
        ["BRU-LGW"] = new() { new("U2", "Airbus A319", [7, 10, 14, 18, 21], 39, 0, true), new("FR", "Boeing 737-800", [8, 13, 19], 19, 0, true) },
        ["LGW-NAP"] = new() { new("U2", "Airbus A320neo", [6, 14], 75, 0, true), new("FR", "Boeing 737-800", [7, 15], 39, 0, true) },
        ["NAP-LGW"] = new() { new("U2", "Airbus A320neo", [7, 15], 75, 0, true), new("FR", "Boeing 737-800", [8, 16], 39, 0, true) },
        ["LGW-OPO"] = new() { new("U2", "Airbus A320neo", [6, 13, 19], 65, 0, true), new("FR", "Boeing 737-800", [7, 14, 20], 35, 0, true) },
        ["OPO-LGW"] = new() { new("U2", "Airbus A320neo", [7, 14, 20], 65, 0, true), new("FR", "Boeing 737-800", [8, 15, 21], 35, 0, true) },
        ["LGW-SKG"] = new() { new("U2", "Airbus A320neo", [6, 15], 109, 0, true) },
        ["SKG-LGW"] = new() { new("U2", "Airbus A320neo", [7, 16], 109, 0, true) },
        ["LGW-CFU"] = new() { new("U2", "Airbus A320neo", [6, 15], 109, 0, true), new("FR", "Boeing 737-800", [7, 16], 55, 0, true) },
        ["CFU-LGW"] = new() { new("U2", "Airbus A320neo", [7, 16], 109, 0, true), new("FR", "Boeing 737-800", [8, 17], 55, 0, true) },
        ["LGW-KRK"] = new() { new("U2", "Airbus A320neo", [7, 16], 55, 0, true), new("FR", "Boeing 737-800", [8, 17], 25, 0, true) },
        ["KRK-LGW"] = new() { new("U2", "Airbus A320neo", [8, 17], 55, 0, true), new("FR", "Boeing 737-800", [9, 18], 25, 0, true) },

        // ── Stansted new (FR)
        ["STN-BER"] = new() { new("FR", "Boeing 737-800", [6, 10, 14, 18], 19, 0, true) },
        ["BER-STN"] = new() { new("FR", "Boeing 737-800", [7, 11, 15, 19], 19, 0, true) },
        ["STN-GVA"] = new() { new("FR", "Boeing 737-800", [6, 11, 16], 25, 0, true) },
        ["GVA-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17], 25, 0, true) },
        ["STN-PRG"] = new() { new("FR", "Boeing 737-800", [6, 11, 16, 20], 19, 0, true) },
        ["PRG-STN"] = new() { new("FR", "Boeing 737-800", [7, 12, 17, 21], 19, 0, true) },
        ["STN-KRK"] = new() { new("FR", "Boeing 737-800", [6, 12, 18], 19, 0, true) },
        ["KRK-STN"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 19, 0, true) },
        ["STN-BUD"] = new() { new("FR", "Boeing 737-800", [6, 12, 18], 25, 0, true), new("W6", "Airbus A320neo", [7, 14], 29, 0, true) },
        ["BUD-STN"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 25, 0, true), new("W6", "Airbus A320neo", [8, 15], 29, 0, true) },
        ["STN-WAW"] = new() { new("FR", "Boeing 737-800", [6, 12, 18], 19, 0, true), new("W6", "Airbus A320neo", [7, 15], 25, 0, true) },
        ["WAW-STN"] = new() { new("FR", "Boeing 737-800", [7, 13, 19], 19, 0, true), new("W6", "Airbus A320neo", [8, 16], 25, 0, true) },

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

        // ── New European city routes ──────────────────────────────────────────
        // LHR → Berlin Brandenburg: BA980, BA982, BA984, BA986
        ["LHR-BER"] = ["BA980", "BA982", "BA984", "BA986"],
        ["BER-LHR"] = ["BA981", "BA983", "BA985", "BA987"],
        // LHR → Geneva: BA730, BA732, BA734, BA736
        ["LHR-GVA"] = ["BA730", "BA732", "BA734", "BA736"],
        ["GVA-LHR"] = ["BA731", "BA733", "BA735", "BA737"],
        // LHR → Brussels: BA392, BA394, BA396, BA398, BA400
        ["LHR-BRU"] = ["BA392", "BA394", "BA396", "BA398", "BA400"],
        ["BRU-LHR"] = ["BA393", "BA395", "BA397", "BA399", "BA401"],
        // LHR → Lyon: BA366, BA368, BA370
        ["LHR-LYS"] = ["BA366", "BA368", "BA370"],
        ["LYS-LHR"] = ["BA367", "BA369", "BA371"],
        // LHR → Helsinki: BA454, BA456
        ["LHR-HEL"] = ["BA454", "BA456"],
        ["HEL-LHR"] = ["BA455", "BA457"],
        // LHR → Hamburg: BA944, BA946, BA948
        ["LHR-HAM"] = ["BA944", "BA946", "BA948"],
        ["HAM-LHR"] = ["BA945", "BA947", "BA949"],
        // LHR → Düsseldorf: BA928, BA930, BA932, BA934
        ["LHR-DUS"] = ["BA928", "BA930", "BA932", "BA934"],
        ["DUS-LHR"] = ["BA929", "BA931", "BA933", "BA935"],
        // LHR → Porto: BA494, BA496
        ["LHR-OPO"] = ["BA494", "BA496"],
        ["OPO-LHR"] = ["BA495", "BA497"],
        // LHR → Naples: BA562, BA564
        ["LHR-NAP"] = ["BA562", "BA564"],
        ["NAP-LHR"] = ["BA563", "BA565"],
        // LHR → Venice VCE: BA556, BA558, BA560
        ["LHR-VCE"] = ["BA556", "BA558", "BA560"],
        ["VCE-LHR"] = ["BA557", "BA559", "BA561"],

        // ── UK domestic flights ────────────────────────────────────────────────
        // LHR → Edinburgh: BA1470, BA1472, BA1474, BA1476, BA1478, BA1480
        ["LHR-EDI"] = ["BA1470", "BA1472", "BA1474", "BA1476", "BA1478", "BA1480"],
        ["EDI-LHR"] = ["BA1471", "BA1473", "BA1475", "BA1477", "BA1479", "BA1481"],
        // LHR → Manchester: BA1382, BA1384, BA1386, BA1388, BA1390, BA1392, BA1394
        ["LHR-MAN"] = ["BA1382", "BA1384", "BA1386", "BA1388", "BA1390", "BA1392", "BA1394"],
        ["MAN-LHR"] = ["BA1383", "BA1385", "BA1387", "BA1389", "BA1391", "BA1393", "BA1395"],
        // LHR → Glasgow: BA1490, BA1492, BA1494, BA1496
        ["LHR-GLA"] = ["BA1490", "BA1492", "BA1494", "BA1496"],
        ["GLA-LHR"] = ["BA1491", "BA1493", "BA1495", "BA1497"],
        // LHR → Birmingham: BA5900, BA5902, BA5904, BA5906, BA5908
        ["LHR-BHX"] = ["BA5900", "BA5902", "BA5904", "BA5906", "BA5908"],
        ["BHX-LHR"] = ["BA5901", "BA5903", "BA5905", "BA5907", "BA5909"],
        // LHR → Newcastle: BA1340, BA1342, BA1344
        ["LHR-NCL"] = ["BA1340", "BA1342", "BA1344"],
        ["NCL-LHR"] = ["BA1341", "BA1343", "BA1345"],
        // LHR → Bristol: BA5930, BA5932, BA5934
        ["LHR-BRS"] = ["BA5930", "BA5932", "BA5934"],
        ["BRS-LHR"] = ["BA5931", "BA5933", "BA5935"],
        // LHR → Aberdeen: BA1324, BA1326, BA1328
        ["LHR-ABZ"] = ["BA1324", "BA1326", "BA1328"],
        ["ABZ-LHR"] = ["BA1325", "BA1327", "BA1329"],
        // LHR → Belfast: BA1420, BA1422, BA1424
        ["LHR-BFS"] = ["BA1420", "BA1422", "BA1424"],
        ["BFS-LHR"] = ["BA1421", "BA1423", "BA1425"],

        // ── Additional UK domestic ─────────────────────────────────────────────
        // LHR → Belfast City BHD: BA1432, BA1434, BA1436, BA1438
        ["LHR-BHD"] = ["BA1432", "BA1434", "BA1436", "BA1438"],
        ["BHD-LHR"] = ["BA1433", "BA1435", "BA1437", "BA1439"],
        // LHR → Leeds Bradford: BA1350, BA1352, BA1354, BA1356
        ["LHR-LBA"] = ["BA1350", "BA1352", "BA1354", "BA1356"],
        ["LBA-LHR"] = ["BA1351", "BA1353", "BA1355", "BA1357"],
        // LHR → Southampton: BA5940, BA5942, BA5944, BA5946, BA5948
        ["LHR-SOU"] = ["BA5940", "BA5942", "BA5944", "BA5946", "BA5948"],
        ["SOU-LHR"] = ["BA5941", "BA5943", "BA5945", "BA5947", "BA5949"],
        // LHR → Cardiff: BA5960, BA5962, BA5964, BA5966
        ["LHR-CWL"] = ["BA5960", "BA5962", "BA5964", "BA5966"],
        ["CWL-LHR"] = ["BA5961", "BA5963", "BA5965", "BA5967"],
        // LHR → Inverness: BA1360, BA1362, BA1364
        ["LHR-INV"] = ["BA1360", "BA1362", "BA1364"],
        ["INV-LHR"] = ["BA1361", "BA1363", "BA1365"],
        // LCY domestics (BA CityFlyer)
        ["LCY-EDI"] = ["BA8700", "BA8702", "BA8704", "BA8706", "BA8708"],
        ["EDI-LCY"] = ["BA8701", "BA8703", "BA8705", "BA8707", "BA8709"],
        ["LCY-GLA"] = ["BA8720", "BA8722", "BA8724", "BA8726"],
        ["GLA-LCY"] = ["BA8721", "BA8723", "BA8725", "BA8727"],
        ["LCY-BFS"] = ["BA8730", "BA8732", "BA8734"],
        ["BFS-LCY"] = ["BA8731", "BA8733", "BA8735"],
        ["LCY-BHD"] = ["BA8740", "BA8742", "BA8744", "BA8746"],
        ["BHD-LCY"] = ["BA8741", "BA8743", "BA8745", "BA8747"],
        ["LCY-ABZ"] = ["BA8750", "BA8752", "BA8754"],
        ["ABZ-LCY"] = ["BA8751", "BA8753", "BA8755"],

        // ── Buenos Aires route ──────────────────────────────────────────────────
        // LHR → Buenos Aires: BA245 (21:30)
        ["LHR-EZE"] = ["BA245"],
        ["EZE-LHR"] = ["BA246"],

        // ── Cape Town ──────────────────────────────────────────────────────────
        // LHR → Cape Town: BA59 (20:00)
        ["LHR-CPT"] = ["BA59"],
        ["CPT-LHR"] = ["BA60"],

        // ── Melbourne ──────────────────────────────────────────────────────────
        // LHR → Melbourne (via Singapore): BA17
        ["LHR-MEL"] = ["BA17"],
        ["MEL-LHR"] = ["BA18"],
    };
}
