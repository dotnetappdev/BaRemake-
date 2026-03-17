# ✈ BaRemake

> A full-stack travel booking platform built with **.NET 10 + Blazor + MudBlazor** — Expedia-style multi-airline search with British Airways colours, EasyJet-style seat picking, Booking.com-style hotel search, and a fully customisable white-label admin panel.

---

## Screenshots

| Page | Description |
|------|-------------|
| **Home / Search** | Smart airport search with autocomplete, one-way/return/multi-city tabs, popular destinations |
| **Search Results** | Airline filter chips, sidebar filters (price, time, airline), sorted flight cards with airline logos and brand colours |
| **Seat Map** | EasyJet-style interactive cabin — available/occupied/selected/extra-legroom/business colour-coded |
| **Passenger Details** | Per-passenger form, baggage extras, speedy boarding upsell |
| **Payment** | Stripe Payment Element, Apple Pay, Google Pay express checkout |
| **Booking Confirmation** | Printable receipt with booking reference, all passenger details |
| **Hotels** | Booking.com-style search with amenity filters, star rating, price range |
| **Admin Dashboard** | MudBlazor KPI cards — bookings today, revenue, flights, pending |
| **Admin Settings** | Theme presets (BA/BookIt/Green/Red), live preview strip, branding controls |

> Run the app and navigate to each page to see the full UI. No external screenshots needed — all pages are responsive and render in the browser.

---

## Feature Overview

### ✈ Multi-Airline Flight Search (Expedia-style)

- **16 airlines seeded** — BA, easyJet, Ryanair, Virgin Atlantic, Lufthansa, Air France, Emirates, Iberia, KLM, Wizz Air, TAP, Singapore Airlines, American Airlines, United, Turkish Airlines, Qatar Airways
- **Airline logos** pulled from Wikimedia Commons SVG sources
- **Brand colour accents** per airline on flight cards (header band, select button, route line)
- **Airline filter chips** — click to filter by one or more carriers
- **Sidebar filters** — max price slider, departure time (morning/afternoon/evening), airline checkboxes
- **Sort** by price, departure time, duration, or airline name
- **60+ airports** across UK, Europe, Middle East, Asia, Americas, Africa, Australia
- **120+ routes** including long-haul, short-haul, budget, and charter
- **Low-cost carrier badge** on budget airline cards (easyJet, Ryanair, Wizz Air)
- Outbound flight count shows "X flights across Y airlines"

### 💺 Seat Selection (EasyJet-style)

- **Visual cabin layout** with correct seat numbering
- **Colour-coded seats** — available (green), selected (BA blue), occupied (grey), extra legroom (amber), business (navy)
- **Window / middle / aisle** labels
- **Exit row** extra-legroom upcharge
- **Multi-passenger** — pick seats for each passenger independently
- **Return flight** seat selection in same screen
- **Skip seat selection** option (assigned at check-in)

### 🏨 Hotel Booking (Booking.com-style)

- Hotel search by destination (city/country/name)
- Check-in / check-out date picker, guest count, room count
- **Filter sidebar** — star rating (1–5), price range, amenities (pool, spa, gym, Wi-Fi, breakfast, parking, pets)
- **Sort** — top rated, price asc/desc, stars
- **Rich hotel cards** — name, stars, review score badge, amenity chips, price per night + total
- **Room types** — Standard, Superior, Suite with bed type, size m², max occupancy, inclusions
- **Amenities** — pool, spa, gym, restaurant, bar, parking, beach access, airport shuttle, room service, concierge, pet-friendly, free Wi-Fi, air conditioning

### 🌍 Seeded Hotels (10)

| Hotel | City | Stars |
|-------|------|-------|
| The Langham London | London | 5 |
| citizenM London Tower Hill | London | 4 |
| Le Meurice | Paris | 5 |
| Hotel de Crillon | Paris | 5 |
| Burj Al Arab | Dubai | 5 |
| The Plaza | New York | 5 |
| Hotel Arts Barcelona | Barcelona | 5 |
| Waldorf Astoria Amsterdam | Amsterdam | 5 |
| Marina Bay Sands | Singapore | 5 |
| Rome Cavalieri | Rome | 5 |

### 📦 Holiday Packages

- **Paris City Break** — LHR→CDG + Le Meurice (3 nights)
- **Barcelona Beach & City** — LHR→BCN + Hotel Arts (7 nights)
- **Dubai Luxury Escape** — LHR→DXB + Burj Al Arab (5 nights)
- **New York City Package** — LHR→JFK + The Plaza (7 nights)
- Bundle discounts with "was / now" pricing
- Includes flight + hotel in single booking reference

### 💳 Payment

- **Stripe Payment Element** — card, Klarna, BACS
- **Apple Pay** express checkout
- **Google Pay** express checkout
- Price summary with tax, fees, and extras breakdown
- 256-bit SSL via Stripe

### 👤 Customer

- ASP.NET Identity with custom claims
- Register with title, first/last name, DOB, phone
- Login with remember-me, lockout protection, demo account buttons
- Profile — passport details, nationality, seat preference
- **My Bookings** — filter upcoming/past/cancelled, cancel booking with seat release
- Print/save confirmation receipt

### 👑 Admin Panel

- **Dashboard** — KPI cards: bookings today, revenue today, flights today, pending bookings
- **Flight Pricing** — edit economy/business base prices per flight, dynamic multipliers, extra legroom fee, baggage fee
- **Settings** — full white-label branding controls:
  - Site name, tagline, logo URL, logo icon
  - Theme presets: BA, BookIt Purple, Forest Green, Red, Custom
  - Custom MudColorPicker for primary + secondary + header colours
  - Live preview header strip
  - Dark mode default toggle
  - Feature toggles: Hotels, Packages, Multi-Airline, Car Hire
  - Footer copyright and tech credit lines
  - SEO meta description
- **Data Management** — seed all data, seed hotels only, clear bookings, clear all

### 🎨 Themes

| Preset | Primary | Secondary | Style |
|--------|---------|-----------|-------|
| **BA** (default) | `#002157` Navy | `#c6a84b` Gold | British Airways |
| **BookIt** | `#6b21a8` Purple | `#a855f7` Lilac | BookIt / Skyscanner-style |
| **Green** | `#065f46` Dark Green | `#10b981` Mint | Eco / nature |
| **Red** | `#991b1b` Dark Red | `#ef4444` Red | Virgin-style |
| **Custom** | User picker | User picker | Full custom |

All colours save to the `BrandingSettings` DB row and load app-wide via `BrandingService`.

### 🏢 Multi-Tenancy

- `Tenant` model for travel agencies, corporate accounts, white-label partners
- `TenantId` FK on `ApplicationUser`
- Travel agent role manages their tenant's customers
- Commission rates and IATA agency code per tenant

---

## Demo Logins

All passwords follow the same pattern. Copy directly into the login form.

| Email | Password | Role | Notes |
|-------|----------|------|-------|
| `admin@baremake.com` | `Admin123!` | **Admin** + Customer | Full admin access, all panels |
| `manager@baremake.com` | `Manager123!` | **Manager** | Dashboard and bookings read-only |
| `agent@travelco.com` | `Agent123!` | **TravelAgent** | Manages TravelCo tenant customers |
| `john.smith@example.com` | `Customer123!` | Customer | Demo customer with sample bookings |
| `sarah.jones@example.com` | `Customer123!` | Customer | Demo customer |
| `james.brown@example.com` | `Customer123!` | Customer | Demo customer |
| `emma.wilson@example.com` | `Customer123!` | Customer | Demo customer |
| `oliver.taylor@example.com` | `Customer123!` | Customer | Demo customer |

> **Tip:** On the `/account/login` page there are **quick-fill buttons** to auto-populate admin or customer credentials.

### Role Permissions

| Role | What they can do |
|------|-----------------|
| `Admin` | Everything — pricing, users, settings, seeding, all bookings |
| `Manager` | View dashboard, all bookings, flight list. No pricing edits or settings |
| `TravelAgent` | View/manage bookings for their tenant's customers only |
| `Customer` | Search + book flights/hotels, view own bookings, manage profile |

---

## Architecture

```
BaRemake.sln
├── src/
│   ├── BaRemake.Shared/           # Domain models, DTOs, Enums (shared library)
│   │   ├── Models/                # EF entities
│   │   │   ├── Airline.cs         # 16 airlines with brand colours, logos, baggage policies
│   │   │   ├── Airport.cs
│   │   │   ├── Aircraft.cs + AircraftSeat.cs
│   │   │   ├── Route.cs
│   │   │   ├── Flight.cs          # FK to Airline
│   │   │   ├── FlightSeatPrice.cs
│   │   │   ├── ApplicationUser.cs # Identity user + TenantId, DOB, passport
│   │   │   ├── Booking.cs + BookingPassenger.cs
│   │   │   ├── Hotel.cs + HotelRoom.cs + HotelBooking.cs
│   │   │   ├── Package.cs + PackageBooking.cs
│   │   │   ├── Tenant.cs
│   │   │   └── BrandingSettings.cs  # Single row for white-label config
│   │   ├── DTOs/
│   │   │   ├── FlightSearchResult.cs  # FlightDto (with airline fields), AirlineFilterDto
│   │   │   └── BookingDtos.cs
│   │   └── Enums/Enums.cs         # SeatClass, FlightStatus, AirlineType, HotelCategory, etc.
│   │
│   ├── BaRemake.Data/             # Data access layer
│   │   ├── ApplicationDbContext.cs
│   │   ├── DbContextFactory.cs    # AddBaRemakeDbContext() — reads DatabaseProvider config
│   │   ├── Repositories/
│   │   │   ├── IFlightRepository.cs + FlightRepository.cs
│   │   │   └── IBookingRepository.cs + BookingRepository.cs
│   │   └── Seeding/
│   │       ├── DataSeeder.cs      # Roles, users, 16 airlines, 60+ airports, aircraft, routes, 90d flights
│   │       └── HotelSeeder.cs     # 10 luxury hotels + 4 packages
│   │
│   ├── BaRemake.Api/              # ASP.NET Core Web API (JWT auth)
│   │   ├── Controllers/
│   │   └── appsettings.json
│   │
│   └── BaRemake.Web/              # Blazor Web App (Interactive Server)
│       ├── Pages/
│       │   ├── Home.razor         # Search form + popular destinations
│       │   ├── SearchResults.razor  # Multi-airline results, filter chips, sidebar
│       │   ├── SeatSelection.razor
│       │   ├── PassengerDetails.razor
│       │   ├── Payment.razor
│       │   ├── BookingConfirmation.razor
│       │   ├── MyBookings.razor
│       │   ├── Hotels.razor
│       │   ├── Account/Login.razor + Register.razor
│       │   └── Admin/
│       │       ├── Dashboard.razor
│       │       ├── FlightPricing.razor
│       │       └── Settings.razor  # Full branding controls + data management
│       ├── Components/
│       │   ├── FlightCard.razor   # Airline logo, brand colour accent, city names
│       │   ├── SeatMap.razor
│       │   └── BookingSteps.razor
│       ├── Layout/MainLayout.razor  # MudBlazor providers + dark mode toggle
│       ├── Services/
│       │   ├── BookingSessionService.cs
│       │   ├── ThemeService.cs     # MudBlazor preset themes + custom colours
│       │   ├── BrandingService.cs  # Loads/caches/saves BrandingSettings DB row
│       │   └── AirportSearchService.cs
│       └── wwwroot/css/app.css    # Full BA palette + booking UX styles
```

---

## Database Schema

### Core Tables

| Table | Key Fields |
|-------|-----------|
| `Users` | IdentityUser + FirstName, LastName, Title, DOB, PassportNumber, TenantId |
| `Tenants` | Name, Slug (unique), Type, CommissionRate, IATAAgencyCode |
| `Airlines` | Name, IATACode (unique), LogoUrl, BrandColor, AirlineType, CabinBagPolicy |
| `Airports` | IATACode (unique 3-char), City, Country, Name |
| `Aircraft` | Model, TotalSeats, EconomySeats, BusinessSeats, FirstSeats |
| `AircraftSeats` | AircraftId, SeatNumber, Class, IsWindowSeat, IsAisleSeat, IsExtraLegroom |
| `Routes` | OriginAirportId + DestinationAirportId (unique pair) |
| `Flights` | RouteId, AircraftId, AirlineId, DepartureTime, ArrivalTime, FlightNumber |
| `FlightSeatPrices` | FlightId + Class (unique), BasePrice, DynamicMultiplier, EffectivePrice |
| `Bookings` | BookingReference (unique), UserId, TotalPrice, TaxAmount, Status |
| `BookingPassengers` | BookingId, FlightId, AircraftSeatId, PassengerName, LuggageKg, ExtrasPrice |
| `Hotels` | Name, City, Country, StarRating, ReviewScore, boolean amenities |
| `HotelRooms` | HotelId, Name, BedType, PricePerNight, MaxOccupancy, RoomSizeM2 |
| `HotelBookings` | UserId, HotelRoomId, CheckIn, CheckOut, TotalPrice |
| `Packages` | Name, FlightRouteDesc, HotelId, OriginAirportId, DestAirportId, BasePrice |
| `PackageBookings` | UserId, PackageId, DepartureDate, TotalPrice |
| `BrandingSettings` | Id=1 (single row), SiteName, LogoUrl, PrimaryColor, ThemePreset, feature toggles |

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server (LocalDB works), **or** PostgreSQL, **or** MySQL
- Optional: [Stripe account](https://stripe.com) for live payments

### 1. Clone

```bash
git clone https://github.com/your-org/BaRemake.git
cd BaRemake
```

### 2. Configure database

Edit `src/BaRemake.Web/appsettings.json`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=BaRemake;Trusted_Connection=True;",
    "PostgreSQL": "Host=localhost;Database=BaRemake;Username=postgres;Password=yourpass",
    "MySQL":      "Server=localhost;Database=BaRemake;User=root;Password=yourpass;"
  },
  "Stripe": {
    "PublishableKey": "pk_test_YOUR_KEY_HERE",
    "SecretKey":      "sk_test_YOUR_KEY_HERE"
  }
}
```

### 3. Run EF Core Migrations

Migrations live in `BaRemake.Data`. Run from the repo root:

#### SQL Server (default)

```bash
dotnet ef migrations add InitialCreate \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web

dotnet ef database update \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web
```

#### PostgreSQL

```bash
dotnet ef migrations add InitialCreate \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web \
  --output-dir Migrations/PostgreSQL

# Set provider in environment before updating:
dotnet ef database update \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web
  # (set DatabaseProvider=PostgreSQL in appsettings first)
```

#### MySQL

```bash
dotnet ef migrations add InitialCreate \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web \
  --output-dir Migrations/MySQL

dotnet ef database update \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web
  # (set DatabaseProvider=MySQL in appsettings first)
```

#### Adding new migrations after model changes

```bash
# After changing a model or DbContext:
dotnet ef migrations add <MigrationName> \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web

# Apply:
dotnet ef database update \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web

# Revert last migration (before applying):
dotnet ef migrations remove \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web

# Rollback to a specific migration:
dotnet ef database update <PreviousMigrationName> \
  --project src/BaRemake.Data \
  --startup-project src/BaRemake.Web
```

> **Note:** The EF tools need `Microsoft.EntityFrameworkCore.Design` in the startup project. If it's missing:
> ```bash
> dotnet add src/BaRemake.Web package Microsoft.EntityFrameworkCore.Design
> ```

### 4. Run the application

```bash
# Blazor Web App (auto-seeds data on first run)
dotnet run --project src/BaRemake.Web

# API (optional, for external clients / JWT)
dotnet run --project src/BaRemake.Api
```

Open: **`https://localhost:5001`**

Seed data runs automatically on startup — all airlines, airports, aircraft, 90 days of flights, hotels, packages, and demo users are created if they don't exist.

---

## Booking Flow

```
1.  HOME            Search form — origin/destination autocomplete, dates, passengers, cabin
2.  SEARCH RESULTS  Multi-airline results — filter chips, sidebar, sort by price/time/airline
3.  SEAT MAP        Visual cabin — click to pick seat (or skip)
4.  PASSENGERS      Name, DOB, passport, baggage, extras per passenger
5.  PAYMENT         Stripe / Apple Pay / Google Pay
6.  CONFIRMATION    Booking reference PNR, printable receipt
```

---

## Multi-Airline Seed Data

### Airlines (16)

| Code | Airline | Type | Hub |
|------|---------|------|-----|
| BA | British Airways | Full Service | LHR |
| U2 | easyJet | Low Cost | LGW |
| FR | Ryanair | Ultra Low Cost | STN |
| VS | Virgin Atlantic | Full Service | LHR |
| LH | Lufthansa | Full Service | FRA |
| AF | Air France | Full Service | CDG |
| EK | Emirates | Full Service | DXB |
| IB | Iberia | Full Service | MAD |
| KL | KLM | Full Service | AMS |
| W6 | Wizz Air | Ultra Low Cost | various |
| TP | TAP Air Portugal | Full Service | LIS |
| SQ | Singapore Airlines | Full Service | SIN |
| AA | American Airlines | Full Service | JFK |
| UA | United Airlines | Full Service | ORD |
| TK | Turkish Airlines | Full Service | IST |
| QR | Qatar Airways | Full Service | DOH |

### Airports (60+)

**UK:** LHR, LGW, STN, MAN, EDI, BHX, GLA, BRS
**W. Europe:** CDG, ORY, AMS, FRA, MUC, MAD, BCN, PMI, AGP, FCO, MXP, VCE, ATH, HER, RHO, PRG, BUD, WAW, LIS, FAO, VIE, ZRH, CPH, ARN, OSL, DUB, NCE
**Canaries:** TFS, LPA
**Middle East / Africa:** DXB, AUH, DOH, CAI, JNB, CPT, CMN
**Asia:** SIN, BKK, HKG, NRT, BOM, DEL
**Americas:** JFK, EWR, BOS, MIA, ORD, LAX, SFO, YYZ, YVR, GRU, GIG, EZE
**Australia:** SYD, MEL

### Aircraft (10)

| Model | Seats | Economy | Business | First |
|-------|-------|---------|----------|-------|
| A319 | 124 | 120 | 4 | — |
| A320neo | 180 | 174 | 6 | — |
| A321neo | 220 | 213 | 7 | — |
| A321XLR | 220 | 213 | 7 | — |
| B737-800 | 189 | 183 | 6 | — |
| B737 MAX 8 | 178 | 172 | 6 | — |
| B777-300ER | 396 | 280 | 56 | 14 + lounge |
| B787-9 | 296 | 216 | 42 | 8 |
| B787-10 | 318 | 232 | 48 | 10 |
| A380-800 | 469 | 360 | 70 | 14 |

---

## Technology Stack

| Layer | Technology |
|-------|-----------|
| Framework | .NET 10 |
| Frontend | Blazor Web App (Interactive Server) |
| UI Components | MudBlazor 8 |
| CSS | Custom BA palette + Bootstrap utilities |
| Backend API | ASP.NET Core Web API |
| Auth | ASP.NET Core Identity + JWT (API) + Cookie (Blazor) |
| ORM | Entity Framework Core 10 |
| SQL Server | Microsoft.EntityFrameworkCore.SqlServer |
| PostgreSQL | Npgsql.EntityFrameworkCore.PostgreSQL |
| MySQL | Pomelo.EntityFrameworkCore.MySql |
| Payments | Stripe.js + Payment Element |
| API Docs | Swagger / OpenAPI |

---

## API Endpoints

```
POST /api/auth/register            Register new customer
POST /api/auth/login               Login → JWT token
GET  /api/auth/profile             Current user [Auth]
PUT  /api/auth/profile             Update profile [Auth]

GET  /api/flights/search           Search ?origin=LHR&destination=JFK&date=...
GET  /api/flights/{id}             Flight by ID
GET  /api/flights/{id}/seatmap     Seat map for flight
GET  /api/flights/airports         All airports (+ ?search=)

POST /api/bookings                 Create booking [Auth]
GET  /api/bookings/my              My bookings [Auth]
GET  /api/bookings/{ref}           Get by reference [Auth]
DELETE /api/bookings/{id}          Cancel [Auth]

GET  /api/admin/dashboard          Stats [Admin]
GET  /api/admin/flights            All flights [Admin]
PUT  /api/admin/flights/{id}/pricing  Update pricing [Admin]
GET  /api/admin/bookings           All bookings [Admin]
```

---

## Customising the Brand

1. Log in as `admin@baremake.com` / `Admin123!`
2. Go to **Admin → Settings**
3. Edit:
   - **Site name** (e.g. "SkyBook", "JetWay")
   - **Logo URL** — paste any image URL; leave blank for emoji icon
   - **Tagline** — one-liner shown in header
   - **Theme preset** — click a colour swatch
   - **Custom colours** — use the colour picker for full control
4. Click **Save branding settings**

All settings are saved to the `BrandingSettings` table (row Id=1) and loaded app-wide at startup via `BrandingService`.

---

## Licence

MIT — Free for learning, demos, and as a production foundation.

---

*Built with .NET 10 · Blazor Interactive Server · MudBlazor 8 · EF Core 10 · ASP.NET Core Identity*
