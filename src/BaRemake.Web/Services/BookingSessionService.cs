using BaRemake.Shared.DTOs;
using BaRemake.Shared.Enums;

namespace BaRemake.Web.Services;

/// <summary>
/// Scoped service that holds the user's in-progress booking session across Blazor pages.
/// </summary>
public class BookingSessionService
{
    public BookingSessionDto Session { get; private set; } = new();

    public void StartNewSearch(FlightSearchRequest request)
    {
        Session = new BookingSessionDto { SearchRequest = request, CurrentStep = 1 };
    }

    public void SelectOutboundFlight(FlightDto flight)
    {
        Session.SelectedOutboundFlight = flight;
        Session.CurrentStep = Session.SearchRequest.TripType == TripType.Return ? 2 : 3;
        RecalculatePrice();
    }

    public void SelectReturnFlight(FlightDto flight)
    {
        Session.SelectedReturnFlight = flight;
        Session.CurrentStep = 3;
        RecalculatePrice();
    }

    public void SetOutboundSeats(List<int> seatIds, List<string> seatNumbers)
    {
        Session.SelectedOutboundSeatIds = seatIds;
        for (int i = 0; i < Session.Passengers.Count && i < seatNumbers.Count; i++)
            Session.Passengers[i].OutboundSeatNumber = seatNumbers[i];
    }

    public void SetReturnSeats(List<int> seatIds, List<string> seatNumbers)
    {
        Session.SelectedReturnSeatIds = seatIds;
        for (int i = 0; i < Session.Passengers.Count && i < seatNumbers.Count; i++)
            Session.Passengers[i].ReturnSeatNumber = seatNumbers[i];
    }

    public void InitialisePassengers()
    {
        int total = Session.SearchRequest.TotalPassengers;
        Session.Passengers = Enumerable.Range(0, total)
            .Select((_, i) => new PassengerInputDto { IsLeadPassenger = i == 0 })
            .ToList();
    }

    public void RecalculatePrice()
    {
        decimal total = 0;
        var cls = Session.SearchRequest.CabinClass;

        if (Session.SelectedOutboundFlight != null)
            total += (Session.SelectedOutboundFlight.PriceForClass(cls) ?? 0)
                     * Session.SearchRequest.TotalPassengers;

        if (Session.SelectedReturnFlight != null)
            total += (Session.SelectedReturnFlight.PriceForClass(cls) ?? 0)
                     * Session.SearchRequest.TotalPassengers;

        // Extras
        foreach (var p in Session.Passengers)
        {
            if (p.HasCheckedBaggage) total += 35;
            total += p.CabinBaggage switch
            {
                CabinBaggage.Small => 12,
                CabinBaggage.Large => 28,
                _ => 0
            };
        }

        Session.TotalPrice = total;
    }

    public bool CanProceedToStep(int step) => step switch
    {
        2 => Session.SelectedOutboundFlight != null,
        3 => Session.SelectedOutboundFlight != null &&
             (Session.SearchRequest.TripType == TripType.OneWay || Session.SelectedReturnFlight != null),
        4 => Session.SelectedOutboundFlight != null && Session.Passengers.Any(),
        5 => Session.Passengers.All(p => !string.IsNullOrEmpty(p.FirstName) && !string.IsNullOrEmpty(p.LastName)),
        _ => true
    };

    public void Reset() => Session = new BookingSessionDto();
}
