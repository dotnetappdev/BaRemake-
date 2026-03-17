# ✈ BaRemake Airways

> A full-stack travel booking platform built with **.NET 10 + Blazor + MudBlazor** — inspired by **EasyJet's booking UX** with **British Airways' colour palette**, and the comprehensive functionality of **Expedia + Booking.com + Airbnb**.

---

## 🌟 Feature Overview

### ✈ Flight Booking (EasyJet-style UX, BA aesthetic)
- **Smart search** — autocomplete airport search (IATA code, city, country)
- **One-way and return** flights
- **Multiple flights per day** displayed with departure times, duration, availability
- **Cabin classes** — Economy, Business, First
- **Dynamic pricing** — admin-controlled base price + occupancy multiplier
- **Tier pricing** — Economy, Economy Plus, Business, First
- **Extras** — speedy boarding, fast-track security, priority boarding
- **Interactive seat map** — EasyJet-style visual cabin with colour-coded seats:
  - Window / aisle / middle seat labels
  - Extra legroom rows (exit row)
  - Business class seats
  - Occupied / available / selected states
  - Click to select / deselect
- **Real-time seat availability** — seats marked occupied on booking
- **Multi-passenger booking** — separate seat selection per passenger
- **Return trip** seat selection (outbound + return)

### 🏨 Hotel Booking (Booking.com-style)
- **Hotel search** by city, country, name
- **Filter** by star rating, price range, amenities
- **Sort** by rating, price, stars
- **Rich hotel profiles** — description, amenities, photos, reviews
- **Amenities** — pool, spa, gym, restaurant, bar, parking, beach access, airport shuttle, pet-friendly, room service, concierge, free Wi-Fi, breakfast
- **Room types** — Standard, Superior, Suite, etc.
- **Room details** — bed type (Single/Twin/Double/Queen/King/Super King), size m², max occupancy, bath/shower, balcony, sea view, city view, kitchenette, breakfast included
- **Hotel categories** — Hotel, Apartment, Villa, Resort, Hostel, B&B, Boutique

### 📦 Holiday Packages (Expedia/TUI-style)
- **Flight + Hotel** packages
- Optional **airport transfers**, **breakfast**, **car hire**
- Discounted bundle pricing with "was / now" pricing display
- Package booking with single reference

### 👤 Customer Experience
- **ASP.NET Identity** full authentication with custom claims
- **Register** — title, first/last name, DOB, phone, email, password
- **Login** with remember me, lockout protection, demo account buttons
- **Profile** — passport details, nationality, seat preferences
- **My Bookings** — filter by upcoming/past/cancelled, grouped by flight
- **Booking confirmation** — printable receipt with full details
- **Cancel booking** with seat release
- **Email confirmation** (receipt page, print-ready)

### 💳 Payment
- **Stripe** card payments (Payment Element)
- **Apple Pay** express checkout
- **Google Pay** express checkout
- Mastercard, Visa, Amex support
- 256-bit SSL via Stripe
- Print/save receipt after booking

### 👑 Admin Panel (MudBlazor UI)
- **Dashboard** — KPI cards: flights today, bookings today, revenue today, pending bookings
- **Flight pricing management** — edit economy/business base prices, dynamic multipliers, extra legroom fees, baggage fees per flight with date filter
- **Hotel management** — full CRUD with MudBlazor data grids and dialogs
- **Booking overview** — recent bookings table with status badges
- **User management** — view/edit users, roles, tenant assignments
- **Data management** — seed all data, seed hotels only, clear booking data, clear all data (except logins)
- **Branding / settings** — colour theme presets (BA blue, BookIt purple, green, red), custom colour pickers (primary + secondary), light/dark mode toggle

### 🎨 Theming (Like BookIt)
| Preset | Primary | Secondary |
|--------|---------|-----------|
| **BA (default)** | `#002157` Navy | `#c6a84b` Gold |
| **BookIt Purple** | `#6b21a8` Purple | `#a855f7` Lilac |
| **Forest Green** | `#065f46` Green | `#34d399` Mint |
| **Red** | `#991b1b` Red | `#f87171` Rose |
| **Custom** | User picker | User picker |

- Full **dark mode** with MudBlazor dark palette
- Persisted theme preference

### 🏢 Multi-Tenancy (Travel Agent Portal)
- **Tenant model** — travel agencies, corporate clients, white-label partners
- **TenantId on ApplicationUser** — linked to AspNetUsers table
- **Travel agent role** — agents can manage their customers' bookings
- **Commission rates** per tenant
- **IATA agency code** support
- Customers scoped to their tenant

### 🔐 Roles & Claims
| Role | Permissions |
|------|-------------|
| `Admin` | Full system access, pricing, users, settings |
| `Manager` | Dashboard, bookings, flights view |
| `TravelAgent` | Manage own tenant's customers and bookings |
| `Customer` | Book flights/hotels, manage own bookings |

**Custom Claims** added at sign-in via `ApplicationUserClaimsPrincipalFactory`:
- `FullName` — "John Smith"
- `FirstName` — "John"
- `LastName` — "Smith"
- `IsAdmin` — "true" / "false"
- `Title` — "Mr" / "Mrs" etc.
- Standard `ClaimTypes.Role` claims for all roles

---

## 🏗 Architecture

```
BaRemake.sln
├── src/
│   ├── BaRemake.Shared/          # Domain models, DTOs, Enums
│   │   ├── Models/               # EF entities (Airport, Aircraft, Flight, Hotel, Booking, etc.)
│   │   ├── DTOs/                 # Data transfer objects
│   │   └── Enums/                # Enumerations
│   │
│   ├── BaRemake.Data/            # Data access layer
│   │   ├── ApplicationDbContext.cs
│   │   ├── DbContextFactory.cs   # Multi-provider registration
│   │   ├── Repositories/         # IFlightRepository, IBookingRepository
│   │   └── Seeding/              # DataSeeder, HotelSeeder
│   │
│   ├── BaRemake.Api/             # ASP.NET Core Web API (JWT auth)
│   │   ├── Controllers/          # Auth, Flights, Bookings, Admin
│   │   └── appsettings.json
│   │
│   └── BaRemake.Web/             # Blazor Web App (Interactive Server)
│       ├── Pages/                # Home, SearchResults, SeatSelection, PassengerDetails, Payment,
│       │   ├── Account/          # Login, Register, Profile
│       │   └── Admin/            # Dashboard, FlightPricing, Settings
│       ├── Components/           # SeatMap, FlightCard, BookingSteps, etc.
│       ├── Layout/               # MainLayout (with MudBlazor providers)
│       ├── Services/             # BookingSessionService, ThemeService, AirportSearchService
│       └── wwwroot/css/app.css   # BA colour palette + booking UI styles
```

---

## 🗄 Database Schema

### Core Tables
| Table | Description |
|-------|-------------|
| `Users` | ASP.NET Identity users + custom fields (TenantId, DOB, passport, etc.) |
| `AspNetRoles` | Admin, Manager, TravelAgent, Customer |
| `AspNetUserRoles` | User → Role mapping |
| `AspNetUserClaims` | FullName, FirstName, LastName, IsAdmin claims |
| `Tenants` | Multi-tenant travel agency records |
| `Airports` | 15 seeded airports (LHR, JFK, CDG, DXB, SIN, etc.) |
| `Aircraft` | 5 aircraft types (A320, A321, B777, B787, A380) |
| `AircraftSeats` | All seats per aircraft with class, window/aisle, exit row |
| `Routes` | 26 routes between seeded airports |
| `Flights` | 90 days × multiple daily departures per route |
| `FlightSeatPrices` | Economy + Business pricing per flight with dynamic multiplier |

### Booking Tables
| Table | Description |
|-------|-------------|
| `Bookings` | Master booking record with reference, total, status |
| `BookingPassengers` | Per-passenger per-flight records with seat assignment |

### Hotel Tables
| Table | Description |
|-------|-------------|
| `Hotels` | 10+ seeded luxury hotels worldwide with full amenities |
| `HotelRooms` | Multiple room types per hotel with pricing |
| `HotelBookings` | Hotel room reservations |
| `Packages` | Flight + hotel bundle definitions |
| `PackageBookings` | Booked packages |

---

## 🌱 Seed Data

### Users (auto-seeded on startup)
| Email | Password | Role |
|-------|----------|------|
| `admin@baremake.com` | `Admin123!` | Admin + Customer |
| `john.smith@example.com` | `Customer123!` | Customer |
| `sarah.jones@example.com` | `Customer123!` | Customer |
| `james.brown@example.com` | `Customer123!` | Customer |
| `emma.wilson@example.com` | `Customer123!` | Customer |
| `oliver.taylor@example.com` | `Customer123!` | Customer |

### Airports (15)
LHR, LGW, JFK, CDG, AMS, MAD, BCN, FCO, DXB, SIN, JNB, BOS, ORD, LAX, MIA

### Aircraft (5)
Airbus A320neo (3-3), A321neo (3-3), Boeing 777-300ER (3-4-3), B787-9 (3-3-3), A380-800 (3-4-3)

### Flights
~2,000+ flights seeded — 90 days of scheduled service on 26 routes with 2-8 daily departures each. Dynamic pricing variation per flight.

### Hotels (10+)
The Langham London · Le Meurice Paris · Hotel de Crillon · Burj Al Arab Dubai · The Plaza New York · Hotel Arts Barcelona · Waldorf Astoria Amsterdam · Marina Bay Sands Singapore · Rome Cavalieri · Faena Miami

### Packages (4)
Paris City Break · Barcelona Beach & City · Dubai Luxury Escape · New York City Package

---

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB, full, Azure) **or** PostgreSQL **or** MySQL
- Optional: Stripe account for real payments

### 1. Clone and configure

```bash
git clone https://github.com/your-org/BaRemake.git
cd BaRemake
```

Edit `src/BaRemake.Web/appsettings.json`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=BaRemake;Trusted_Connection=True;"
  },
  "Stripe": {
    "PublishableKey": "pk_test_YOUR_KEY",
    "SecretKey": "sk_test_YOUR_KEY"
  }
}
```

To use **PostgreSQL**, change `DatabaseProvider` to `"PostgreSQL"` and set the `PostgreSQL` connection string.
To use **MySQL**, change to `"MySQL"` and set the `MySQL` connection string.

### 2. Run database migrations

```bash
cd src/BaRemake.Data

# SQL Server (default)
dotnet ef database update --startup-project ../BaRemake.Web

# PostgreSQL
dotnet ef database update --startup-project ../BaRemake.Web -- --DatabaseProvider PostgreSQL

# MySQL
dotnet ef database update --startup-project ../BaRemake.Web -- --DatabaseProvider MySQL
```

### 3. Run the application

```bash
# Run Blazor Web App (includes seed on startup)
cd src/BaRemake.Web
dotnet run

# Run API separately (optional)
cd src/BaRemake.Api
dotnet run
```

Open: `https://localhost:5001`

The app auto-seeds all data on first run.

### 4. Generate Migrations (if needed)

```bash
cd src/BaRemake.Data

# SQL Server
dotnet ef migrations add InitialCreate --startup-project ../BaRemake.Web

# PostgreSQL
dotnet ef migrations add InitialCreate --startup-project ../BaRemake.Web -o Migrations/PostgreSQL
```

---

## 📱 Booking Flow

```
1. 🔍 SEARCH        → Enter origin/destination, dates, passengers, cabin class
2. ✈ SELECT FLIGHT  → Choose outbound flight (multiple shown per day)
3. 🔄 RETURN        → Choose return flight (if return trip)
4. 💺 SEAT MAP      → Interactive visual seat picker (skip option available)
5. 👤 PASSENGERS    → Fill details: name, DOB, passport, baggage extras
6. 💳 PAYMENT       → Stripe card / Apple Pay / Google Pay
7. ✅ CONFIRMATION  → Booking reference, printable receipt, email
```

---

## 🎨 Design Decisions (vs Real BA Website)

Based on common complaints about British Airways' website:

| BA Website Issue | BaRemake Solution |
|-----------------|-------------------|
| Slow, complex search | Single-page search with instant autocomplete |
| Confusing seat selection | EasyJet-style visual map, click to select |
| Hidden fees revealed late | All fees shown upfront on results page |
| Poor mobile experience | Fully responsive, flexbox/grid layout |
| Slow page loads | Blazor Server with SSR, no full page reloads |
| Cluttered UI | Clean card-based design, clear visual hierarchy |
| No price comparison | Economy and Business prices both shown per flight |
| Poor error messages | Contextual validation with clear error text |

---

## 🔧 Technology Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | .NET 10 |
| **Frontend** | Blazor Web App (Interactive Server) |
| **UI Components** | MudBlazor 8 |
| **Styling** | Custom CSS (BA colour palette) + Bootstrap utilities |
| **Backend API** | ASP.NET Core Web API |
| **Auth** | ASP.NET Core Identity + JWT (API) + Cookie (Blazor) |
| **ORM** | Entity Framework Core 10 |
| **SQL Server** | Microsoft.EntityFrameworkCore.SqlServer |
| **PostgreSQL** | Npgsql.EntityFrameworkCore.PostgreSQL |
| **MySQL** | Pomelo.EntityFrameworkCore.MySql |
| **Payments** | Stripe.js + Payment Element |
| **API Docs** | Swagger / OpenAPI |

---

## 🌐 API Endpoints

```
POST   /api/auth/register          Register new customer
POST   /api/auth/login             Login → JWT token
GET    /api/auth/profile           Get current user profile [Auth]
PUT    /api/auth/profile           Update profile [Auth]

GET    /api/flights/search         Search flights ?origin=LHR&destination=JFK&date=...
GET    /api/flights/{id}           Get flight by ID
GET    /api/flights/{id}/seatmap   Interactive seat map
GET    /api/flights/airports       List all airports (+ ?search=)

POST   /api/bookings               Create booking [Auth]
GET    /api/bookings/my            My bookings [Auth]
GET    /api/bookings/{ref}         Get by reference [Auth]
DELETE /api/bookings/{id}          Cancel booking [Auth]

GET    /api/admin/dashboard        Dashboard stats [Admin]
GET    /api/admin/flights          All flights for pricing [Admin]
PUT    /api/admin/flights/{id}/pricing  Update flight pricing [Admin]
GET    /api/admin/bookings         All bookings [Admin]
```

---

## 🏢 Multi-Tenancy Model

```
Tenant (Travel Agency)
  │
  ├── ApplicationUser (TravelAgent role) ─── manages ──► Customers
  │     └── TenantId FK
  │
  └── ApplicationUser (Customer) ── TenantId FK ──► Tenant
        └── Bookings scoped to tenant
```

Travel agents at `tenantId=2` can only see/manage customers where `customer.TenantId == 2`.

---

## 🗂 Project Structure Details

### BaRemake.Shared
- `Models/` — Airport, Aircraft, AircraftSeat, Route, Flight, FlightSeatPrice, ApplicationUser, Booking, BookingPassenger, Hotel, HotelRoom, HotelBooking, Package, PackageBooking, Tenant
- `DTOs/` — FlightSearchRequest, FlightSearchResult, FlightDto, SeatMapDto, SeatRowDto, SeatDto, BookingSessionDto, PassengerInputDto, BookingConfirmationDto, AdminFlightPricingDto, AdminDashboardDto, RegisterDto, LoginDto
- `Enums/` — SeatClass, BookingStatus, FlightStatus, SeatStatus, TripType, CabinBaggage, DatabaseProvider, HotelCategory, BedType, TenantType

### BaRemake.Data
- `ApplicationDbContext` — IdentityDbContext with all entities and EF configurations
- `DbContextFactory.cs` — `AddBaRemakeDbContext()` extension — registers correct provider from config
- `Repositories/IFlightRepository` + `FlightRepository` — search, seat maps, pricing
- `Repositories/IBookingRepository` + `BookingRepository` — create, list, cancel, dashboard stats
- `Seeding/DataSeeder` — roles, users, airports, aircraft (with seats), routes, 90 days of flights
- `Seeding/HotelSeeder` — 10+ luxury hotels with rooms and 4 holiday packages

### BaRemake.Api
- `AuthController` — register, login (JWT), profile, update
- `FlightsController` — search, get, seat map, airports
- `BookingsController` — create, list, get, cancel
- `AdminController` — dashboard, flights (pricing), bookings

### BaRemake.Web
- `Services/BookingSessionService` — scoped in-memory booking state across pages
- `Services/ThemeService` — MudBlazor theme management (presets + custom + dark mode)
- `Services/AirportSearchService` — cached airport search
- `Services/ApplicationUserClaimsPrincipalFactory` — custom claims on sign-in
- `Pages/Home` — search form with airport autocomplete and popular destinations
- `Pages/SearchResults` — flight results with sort, multiple flights per day
- `Pages/SeatSelection` — dual seat map (outbound + return) with visual aircraft cabin
- `Pages/PassengerDetails` — per-passenger form with extras
- `Pages/Payment` — Stripe, Apple Pay, Google Pay
- `Pages/BookingConfirmation` — receipt page (print-ready)
- `Pages/MyBookings` — filter by status, cancel
- `Pages/Hotels` — hotel search with filters
- `Pages/Account/Login` + `Register` — full auth pages
- `Pages/Admin/Dashboard` — MudBlazor KPI dashboard
- `Pages/Admin/FlightPricing` — bulk pricing management
- `Pages/Admin/Settings` — theme presets, colour pickers, data seeding
- `Components/SeatMap` — fully interactive visual seat picker
- `Components/FlightCard` — flight result card with prices
- `Components/BookingSteps` — progress indicator

---

## 📝 Licence

MIT — Use freely for learning, demos or as a foundation for production systems.

---

*Built on .NET 10 · Blazor Interactive Server · MudBlazor 8 · EF Core 10 · ASP.NET Identity*
