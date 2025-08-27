using WrightBrothersApi.Models;

public class Flight
{
    public int Id { get; set; }
    public required string FlightNumber { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public FlightStatus Status { get; set; }
    public int FuelRange { get; set; }

    public bool FuelTankLeak { get; set; }

    public required string FlightLogSignature { get; set; }

    public required string AerobaticSequenceSignature { get; set; }
}

public enum FlightStatus
{
    Scheduled,
    Boarding,
    Departed,
    InAir,
    Landed,
    Cancelled,
    Delayed
}
