using BaRemake.Shared.DTOs;
using BaRemake.Shared.Models;

namespace BaRemake.Data.Repositories;

public interface IBookingRepository
{
    Task<Booking> CreateBookingAsync(BookingSessionDto session, string userId);
    Task<List<Booking>> GetUserBookingsAsync(string userId);
    Task<Booking?> GetBookingByReferenceAsync(string reference);
    Task<Booking?> GetBookingByIdAsync(int id);
    Task<bool> CancelBookingAsync(int bookingId, string userId);
    Task<List<RecentBookingDto>> GetRecentBookingsAsync(int count = 20);
    Task<AdminDashboardDto> GetDashboardStatsAsync();
}
