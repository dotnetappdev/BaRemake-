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
        await SeedAirportsAsync();
        await SeedAircraftAsync();
        await SeedRoutesAsync();
        await SeedFlightsAsync();
        await SeedUsersAsync();
    }

    // ── Roles ──────────────────────────────────────────────────────────────

    private async Task SeedRolesAsync()
    {
        foreach (var role in new[] { "Admin", "Customer" })
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
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
                await _userManager.AddToRolesAsync(admin, new[] { "Admin", "Customer" });
                await _userManager.AddClaimsAsync(admin, new[]
                {
                    new System.Security.Claims.Claim("FullName", admin.FullName),
                    new System.Security.Claims.Claim("FirstName", admin.FirstName),
                    new System.Security.Claims.Claim("LastName", admin.LastName),
                    new System.Security.Claims.Claim("IsAdmin", "true")
                });
            }
        }

        // Sample customers
        var customers = new[]
        {
            ("john.smith@example.com", "John", "Smith", "Mr", "Customer123!", new DateTime(1988, 3, 15)),
            ("sarah.jones@example.com", "Sarah", "Jones", "Mrs", "Customer123!", new DateTime(1992, 7, 22)),
            ("james.brown@example.com", "James", "Brown", "Mr", "Customer123!", new DateTime(1975, 11, 8)),
            ("emma.wilson@example.com", "Emma", "Wilson", "Ms", "Customer123!", new DateTime(1995, 4, 30)),
            ("oliver.taylor@example.com", "Oliver", "Taylor", "Mr", "Customer123!", new DateTime(1983, 9, 12))
        };

        foreach (var (email, first, last, title, pass, dob) in customers)
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
                var result = await _userManager.CreateAsync(user, pass);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    await _userManager.AddClaimsAsync(user, new[]
                    {
                        new System.Security.Claims.Claim("FullName", user.FullName),
                        new System.Security.Claims.Claim("FirstName", user.FirstName),
                        new System.Security.Claims.Claim("LastName", user.LastName),
                        new System.Security.Claims.Claim("IsAdmin", "false")
                    });
                }
            }
        }
    }

    // ── Airports ──────────────────────────────────────────────────────────

    private async Task SeedAirportsAsync()
    {
        if (await _db.Airports.AnyAsync()) return;

        var airports = new[]
        {
            ("LHR", "Heathrow Airport", "London", "United Kingdom", "GB", "Europe/London", 51.4775, -0.4614),
            ("LGW", "Gatwick Airport", "London", "United Kingdom", "GB", "Europe/London", 51.1481, -0.1903),
            ("JFK", "John F. Kennedy International Airport", "New York", "United States", "US", "America/New_York", 40.6413, -73.7781),
            ("CDG", "Charles de Gaulle Airport", "Paris", "France", "FR", "Europe/Paris", 49.0097, 2.5479),
            ("AMS", "Amsterdam Airport Schiphol", "Amsterdam", "Netherlands", "NL", "Europe/Amsterdam", 52.3105, 4.7683),
            ("MAD", "Adolfo Suárez Madrid–Barajas Airport", "Madrid", "Spain", "ES", "Europe/Madrid", 40.4936, -3.5668),
            ("BCN", "El Prat de Llobregat Airport", "Barcelona", "Spain", "ES", "Europe/Madrid", 41.2971, 2.0785),
            ("FCO", "Leonardo da Vinci International Airport", "Rome", "Italy", "IT", "Europe/Rome", 41.8003, 12.2389),
            ("DXB", "Dubai International Airport", "Dubai", "United Arab Emirates", "AE", "Asia/Dubai", 25.2532, 55.3657),
            ("SIN", "Singapore Changi Airport", "Singapore", "Singapore", "SG", "Asia/Singapore", 1.3644, 103.9915),
            ("JNB", "O.R. Tambo International Airport", "Johannesburg", "South Africa", "ZA", "Africa/Johannesburg", -26.1367, 28.2411),
            ("BOS", "Logan International Airport", "Boston", "United States", "US", "America/New_York", 42.3656, -71.0096),
            ("ORD", "O'Hare International Airport", "Chicago", "United States", "US", "America/Chicago", 41.9742, -87.9073),
            ("LAX", "Los Angeles International Airport", "Los Angeles", "United States", "US", "America/Los_Angeles", 33.9425, -118.4081),
            ("MIA", "Miami International Airport", "Miami", "United States", "US", "America/New_York", 25.7959, -80.2870)
        };

        _db.Airports.AddRange(airports.Select(a => new Airport
        {
            IATACode = a.Item1,
            Name = a.Item2,
            City = a.Item3,
            Country = a.Item4,
            CountryCode = a.Item5,
            Timezone = a.Item6,
            Latitude = a.Item7,
            Longitude = a.Item8,
            IsActive = true
        }));

        await _db.SaveChangesAsync();
    }

    // ── Aircraft ──────────────────────────────────────────────────────────

    private async Task SeedAircraftAsync()
    {
        if (await _db.Aircraft.AnyAsync()) return;

        var aircraft = new[]
        {
            // (Model, Registration, TotalRows, Layout, BizEnd, EcoStart, ExtraLegroomRows)
            ("Airbus A320neo", "G-TTNA", 30, "3-3", 1, 5, 12, "8,9"),
            ("Airbus A321neo", "G-NEOA", 37, "3-3", 1, 7, 14, "8,9,10"),
            ("Boeing 777-300ER", "G-VIIA", 50, "3-4-3", 1, 8, 50, "12,36,37"),
            ("Boeing 787-9", "G-ZBKA", 44, "3-3-3", 1, 8, 44, "11,31,32"),
            ("Airbus A380-800", "G-XLEB", 60, "3-4-3", 1, 10, 60, "12,43,44,45"),
        };

        foreach (var (model, reg, rows, layout, bizStart, bizEnd, ecoEnd, extraRows) in aircraft)
        {
            var plane = new Aircraft
            {
                Model = model,
                Registration = reg,
                TotalRows = rows,
                SeatsPerRow = layout.Split('-').Sum(int.Parse),
                SeatLayout = layout,
                BusinessStartRow = bizStart,
                BusinessEndRow = bizEnd,
                EconomyStartRow = bizEnd + 1,
                EconomyEndRow = ecoEnd,
                ExtraLegroomRows = extraRows,
                IsActive = true
            };
            _db.Aircraft.Add(plane);
            await _db.SaveChangesAsync();

            // Generate seats
            GenerateSeats(plane, extraRows);
        }

        await _db.SaveChangesAsync();
    }

    private void GenerateSeats(Aircraft plane, string extraRows)
    {
        var extraRowNums = extraRows.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToHashSet();

        var columns = plane.SeatLayout.Split('-').SelectMany((group, gi) =>
        {
            var cols = new List<string>();
            char c = gi == 0 ? 'A' : gi == 1 ? (group == "3" ? 'D' : 'E') : 'H';
            for (int i = 0; i < int.Parse(group); i++, c++)
                cols.Add(c.ToString());
            return cols;
        }).ToList();

        // Simple 3-3 or 3-4-3 column generation
        var cols3_3 = new[] { "A", "B", "C", "D", "E", "F" };
        var cols3_4_3 = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "J" };
        var cols3_3_3 = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "J" };

        var useColumns = plane.SeatLayout switch
        {
            "3-4-3" => cols3_4_3,
            "3-3-3" => cols3_3_3,
            _ => cols3_3
        };

        for (int row = 1; row <= plane.TotalRows; row++)
        {
            var isExtraLegroom = extraRowNums.Contains(row);
            var isBusiness = row >= plane.BusinessStartRow && row <= plane.BusinessEndRow;
            var seatClass = isBusiness ? SeatClass.Business : SeatClass.Economy;

            foreach (var col in useColumns)
            {
                var isWindow = col == "A" || col == useColumns.Last();
                var isAisle = col == "C" || col == "D" || (useColumns.Length > 6 && (col == "F" || col == "G"));

                _db.AircraftSeats.Add(new AircraftSeat
                {
                    AircraftId = plane.Id,
                    Row = row,
                    Column = col,
                    SeatNumber = $"{row}{col}",
                    Class = seatClass,
                    IsExtraLegroom = isExtraLegroom,
                    IsExitRow = isExtraLegroom,
                    IsWindowSeat = isWindow,
                    IsAisleSeat = isAisle,
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

        var routes = new[]
        {
            // (Origin, Destination, DurationMins)
            ("LHR", "JFK", 435), ("JFK", "LHR", 420),
            ("LHR", "CDG", 75),  ("CDG", "LHR", 70),
            ("LHR", "AMS", 70),  ("AMS", "LHR", 65),
            ("LHR", "MAD", 140), ("MAD", "LHR", 135),
            ("LHR", "BCN", 145), ("BCN", "LHR", 135),
            ("LHR", "FCO", 155), ("FCO", "LHR", 150),
            ("LHR", "DXB", 395), ("DXB", "LHR", 435),
            ("LHR", "SIN", 770), ("SIN", "LHR", 800),
            ("LHR", "JNB", 660), ("JNB", "LHR", 680),
            ("LHR", "BOS", 400), ("BOS", "LHR", 380),
            ("LHR", "ORD", 490), ("ORD", "LHR", 510),
            ("LHR", "LAX", 665), ("LAX", "LHR", 610),
            ("LHR", "MIA", 545), ("MIA", "LHR", 565),
        };

        int routeIdx = 1;
        foreach (var (orig, dest, dur) in routes)
        {
            if (!airports.ContainsKey(orig) || !airports.ContainsKey(dest)) continue;
            _db.Routes.Add(new Route
            {
                OriginAirportId = airports[orig],
                DestinationAirportId = airports[dest],
                DurationMinutes = dur,
                RouteCode = $"BA{routeIdx++:D3}",
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
        if (!aircraft.Any()) return;

        var rng = new Random(42);
        int flightCounter = 200;

        // Schedule flights for next 90 days
        var today = DateTime.Today;

        // Define daily schedules per route (departure hours)
        var routeSchedules = new Dictionary<string, (int[] Hours, string AircraftModel, decimal EcoBase, decimal BizBase)>
        {
            ["LHR-JFK"] = ([7, 10, 14, 18], "Boeing 777-300ER", 299, 1299),
            ["JFK-LHR"] = ([9, 19, 23], "Boeing 777-300ER", 289, 1249),
            ["LHR-CDG"] = ([6, 8, 10, 12, 14, 16, 18, 20], "Airbus A320neo", 89, 399),
            ["CDG-LHR"] = ([7, 9, 11, 13, 15, 17, 19, 21], "Airbus A320neo", 79, 349),
            ["LHR-AMS"] = ([6, 8, 10, 12, 14, 16, 18, 20], "Airbus A320neo", 79, 349),
            ["AMS-LHR"] = ([7, 9, 11, 13, 15, 17, 19], "Airbus A320neo", 79, 349),
            ["LHR-MAD"] = ([7, 10, 13, 17, 20], "Airbus A321neo", 119, 499),
            ["MAD-LHR"] = ([8, 11, 14, 18, 21], "Airbus A321neo", 109, 469),
            ["LHR-BCN"] = ([7, 11, 15, 19], "Airbus A321neo", 109, 469),
            ["BCN-LHR"] = ([8, 12, 16, 20], "Airbus A321neo", 109, 469),
            ["LHR-FCO"] = ([7, 11, 15, 19], "Airbus A321neo", 129, 549),
            ["FCO-LHR"] = ([8, 12, 16, 20], "Airbus A321neo", 129, 549),
            ["LHR-DXB"] = ([8, 14, 22], "Boeing 777-300ER", 349, 1599),
            ["DXB-LHR"] = ([3, 9, 15], "Boeing 777-300ER", 349, 1599),
            ["LHR-SIN"] = ([21, 23], "Boeing 787-9", 699, 2499),
            ["SIN-LHR"] = ([22, 1], "Boeing 787-9", 699, 2499),
            ["LHR-JNB"] = ([20, 21], "Boeing 777-300ER", 599, 2199),
            ["JNB-LHR"] = ([20, 21], "Boeing 777-300ER", 599, 2199),
            ["LHR-BOS"] = ([9, 13], "Boeing 777-300ER", 279, 1149),
            ["BOS-LHR"] = ([19, 22], "Boeing 777-300ER", 279, 1149),
            ["LHR-ORD"] = ([9, 14], "Boeing 777-300ER", 319, 1299),
            ["ORD-LHR"] = ([18, 21], "Boeing 777-300ER", 319, 1299),
            ["LHR-LAX"] = ([10, 15], "Boeing 787-9", 549, 2199),
            ["LAX-LHR"] = ([16, 20], "Boeing 787-9", 549, 2199),
            ["LHR-MIA"] = ([10, 14], "Boeing 777-300ER", 399, 1599),
            ["MIA-LHR"] = ([18, 22], "Boeing 777-300ER", 399, 1599),
        };

        foreach (var route in routes)
        {
            var key = $"{route.OriginAirport.IATACode}-{route.DestinationAirport.IATACode}";
            if (!routeSchedules.TryGetValue(key, out var sched)) continue;

            var plane = aircraft.FirstOrDefault(a => a.Model == sched.AircraftModel)
                ?? aircraft[rng.Next(aircraft.Count)];

            for (int day = 0; day < 90; day++)
            {
                var date = today.AddDays(day);

                foreach (var hour in sched.Hours)
                {
                    var dep = new DateTime(date.Year, date.Month, date.Day, hour, rng.Next(0, 4) * 5, 0, DateTimeKind.Utc);
                    var arr = dep.AddMinutes(route.DurationMinutes);

                    var ecoSeats = plane.TotalRows * plane.SeatsPerRow - (plane.BusinessEndRow * plane.SeatsPerRow);
                    var bizSeats = plane.BusinessEndRow * plane.SeatsPerRow;

                    // Apply small demand variation
                    var multiplier = 1.0m + (decimal)(rng.NextDouble() * 0.3);

                    var flight = new Flight
                    {
                        FlightNumber = $"BA{flightCounter++}",
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

                    // Pricing
                    _db.FlightSeatPrices.AddRange(new[]
                    {
                        new FlightSeatPrice
                        {
                            FlightId = flight.Id,
                            Class = SeatClass.Economy,
                            BasePrice = sched.EcoBase,
                            DynamicMultiplier = multiplier,
                            SeatSelectionFee = 10,
                            ExtraLegroomFee = 25,
                            BaggageFee = 35
                        },
                        new FlightSeatPrice
                        {
                            FlightId = flight.Id,
                            Class = SeatClass.Business,
                            BasePrice = sched.BizBase,
                            DynamicMultiplier = multiplier,
                            SeatSelectionFee = 0,
                            ExtraLegroomFee = 0,
                            BaggageFee = 0
                        }
                    });
                }
            }

            await _db.SaveChangesAsync();
        }
    }
}
