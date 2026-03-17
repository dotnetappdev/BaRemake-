using BaRemake.Shared.Enums;

namespace BaRemake.Shared.DTOs;

public class SeatMapDto
{
    public int FlightId { get; set; }
    public string AircraftModel { get; set; } = string.Empty;
    public string SeatLayout { get; set; } = "3-3";
    public int TotalRows { get; set; }
    public List<SeatRowDto> Rows { get; set; } = new();
}

public class SeatRowDto
{
    public int RowNumber { get; set; }
    public bool IsExitRow { get; set; }
    public bool IsExtraLegroom { get; set; }
    public List<SeatDto> Seats { get; set; } = new();
}

public class SeatDto
{
    public int Id { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public int Row { get; set; }
    public string Column { get; set; } = string.Empty;
    public SeatClass Class { get; set; }
    public SeatStatus Status { get; set; }
    public bool IsExtraLegroom { get; set; }
    public bool IsExitRow { get; set; }
    public bool IsWindowSeat { get; set; }
    public bool IsAisleSeat { get; set; }
    public bool IsMiddleSeat { get; set; }
    public decimal SeatFee { get; set; }

    // UI state (not persisted)
    public bool IsSelected { get; set; }

    public string CssClass => (Status, IsExtraLegroom, Class) switch
    {
        (SeatStatus.Occupied, _, _) => "seat seat--occupied",
        (SeatStatus.Blocked, _, _) => "seat seat--blocked",
        (_, _, SeatClass.Business) when IsSelected => "seat seat--business seat--selected",
        (_, _, SeatClass.Business) => "seat seat--business",
        (_, _, SeatClass.First) when IsSelected => "seat seat--first seat--selected",
        (_, _, SeatClass.First) => "seat seat--first",
        (_, true, _) when IsSelected => "seat seat--extra-legroom seat--selected",
        (_, true, _) => "seat seat--extra-legroom",
        _ when IsSelected => "seat seat--selected",
        _ => "seat seat--available"
    };
}
