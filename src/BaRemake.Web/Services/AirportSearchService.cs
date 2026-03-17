using BaRemake.Data.Repositories;
using BaRemake.Shared.Models;

namespace BaRemake.Web.Services;

public class AirportSearchService
{
    private readonly IFlightRepository _flights;
    private List<Airport>? _cache;

    public AirportSearchService(IFlightRepository flights) => _flights = flights;

    public async Task<List<Airport>> SearchAsync(string query)
    {
        _cache ??= await _flights.GetAirportsAsync();
        if (string.IsNullOrWhiteSpace(query)) return _cache.Take(8).ToList();

        query = query.ToLower();
        return _cache.Where(a =>
            a.IATACode.ToLower().Contains(query) ||
            a.City.ToLower().Contains(query) ||
            a.Country.ToLower().Contains(query) ||
            a.Name.ToLower().Contains(query))
        .Take(8).ToList();
    }

    public async Task<List<Airport>> GetAllAsync()
    {
        _cache ??= await _flights.GetAirportsAsync();
        return _cache;
    }
}
