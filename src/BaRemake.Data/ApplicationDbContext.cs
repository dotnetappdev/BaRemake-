using BaRemake.Shared.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaRemake.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Aircraft> Aircraft => Set<Aircraft>();
    public DbSet<AircraftSeat> AircraftSeats => Set<AircraftSeat>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<FlightSeatPrice> FlightSeatPrices => Set<FlightSeatPrice>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingPassenger> BookingPassengers => Set<BookingPassenger>();

    // Hotels & Packages
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<HotelRoom> HotelRooms => Set<HotelRoom>();
    public DbSet<HotelBooking> HotelBookings => Set<HotelBooking>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<PackageBooking> PackageBookings => Set<PackageBooking>();

    // Multi-tenancy
    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Airport
        builder.Entity<Airport>(e =>
        {
            e.HasIndex(a => a.IATACode).IsUnique();
            e.Property(a => a.IATACode).HasMaxLength(3).IsRequired();
        });

        // Route - unique per origin/destination pair
        builder.Entity<Route>(e =>
        {
            e.HasIndex(r => new { r.OriginAirportId, r.DestinationAirportId }).IsUnique();
            e.HasOne(r => r.OriginAirport)
                .WithMany(a => a.OriginRoutes)
                .HasForeignKey(r => r.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(r => r.DestinationAirport)
                .WithMany(a => a.DestinationRoutes)
                .HasForeignKey(r => r.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Flight
        builder.Entity<Flight>(e =>
        {
            e.HasIndex(f => f.FlightNumber);
            e.HasIndex(f => f.DepartureTime);
        });

        // FlightSeatPrice - one per class per flight
        builder.Entity<FlightSeatPrice>(e =>
        {
            e.HasIndex(p => new { p.FlightId, p.Class }).IsUnique();
            e.Property(p => p.BasePrice).HasColumnType("decimal(10,2)");
            e.Property(p => p.EffectivePrice).HasColumnType("decimal(10,2)").HasComputedColumnSql(null);
        });

        // Booking
        builder.Entity<Booking>(e =>
        {
            e.HasIndex(b => b.BookingReference).IsUnique();
            e.Property(b => b.TotalPrice).HasColumnType("decimal(10,2)");
            e.Property(b => b.TaxAmount).HasColumnType("decimal(10,2)");
            e.Property(b => b.FeeAmount).HasColumnType("decimal(10,2)");
            e.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // BookingPassenger
        builder.Entity<BookingPassenger>(e =>
        {
            e.HasOne(bp => bp.Flight)
                .WithMany(f => f.Passengers)
                .HasForeignKey(bp => bp.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(bp => bp.AircraftSeat)
                .WithMany()
                .HasForeignKey(bp => bp.AircraftSeatId)
                .OnDelete(DeleteBehavior.SetNull);
            e.Property(bp => bp.ExtrasPrice).HasColumnType("decimal(10,2)");
        });

        // AircraftSeat
        builder.Entity<AircraftSeat>(e =>
        {
            e.HasIndex(s => new { s.AircraftId, s.SeatNumber }).IsUnique();
        });

        // Multi-tenancy
        builder.Entity<Tenant>(e =>
        {
            e.HasIndex(t => t.Slug).IsUnique();
        });

        builder.Entity<ApplicationUser>(e =>
        {
            e.HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Hotels
        builder.Entity<HotelBooking>(e =>
        {
            e.Property(b => b.PricePerNight).HasColumnType("decimal(10,2)");
            e.Property(b => b.TotalPrice).HasColumnType("decimal(10,2)");
            e.Property(b => b.TaxAmount).HasColumnType("decimal(10,2)");
            e.HasOne(b => b.User)
                .WithMany(u => u.HotelBookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<HotelRoom>(e =>
        {
            e.Property(r => r.PricePerNight).HasColumnType("decimal(10,2)");
        });

        builder.Entity<Package>(e =>
        {
            e.Property(p => p.BasePrice).HasColumnType("decimal(10,2)");
            e.Property(p => p.OriginalPrice).HasColumnType("decimal(10,2)");
            e.HasOne(p => p.OriginAirport)
                .WithMany()
                .HasForeignKey(p => p.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(p => p.DestinationAirport)
                .WithMany()
                .HasForeignKey(p => p.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PackageBooking>(e =>
        {
            e.Property(pb => pb.TotalPrice).HasColumnType("decimal(10,2)");
            e.HasOne(pb => pb.User)
                .WithMany()
                .HasForeignKey(pb => pb.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Rename Identity tables to avoid reserved words
        builder.Entity<ApplicationUser>().ToTable("Users");
    }
}
