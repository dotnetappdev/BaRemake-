namespace BaRemake.Shared.Enums;

public enum SeatClass
{
    Economy = 1,
    EconomyPlus = 2,
    Business = 3,
    First = 4
}

public enum BookingStatus
{
    Pending = 1,
    Confirmed = 2,
    Cancelled = 3,
    CheckedIn = 4,
    Completed = 5
}

public enum FlightStatus
{
    Scheduled = 1,
    Boarding = 2,
    Departed = 3,
    Arrived = 4,
    Delayed = 5,
    Cancelled = 6
}

public enum SeatStatus
{
    Available = 1,
    Occupied = 2,
    Blocked = 3,
    ExtraLegroom = 4
}

public enum TripType
{
    OneWay = 1,
    Return = 2
}

public enum CabinBaggage
{
    None = 0,
    Small = 1,
    Large = 2
}

public enum DatabaseProvider
{
    SqlServer = 1,
    PostgreSQL = 2,
    MySQL = 3
}
