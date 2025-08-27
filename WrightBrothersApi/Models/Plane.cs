namespace WrightBrothersApi.Models
{
    public class Plane
    {
    public int Id { get; set; }

    public required string Name { get; set; }

    public int Year { get; set; }

    public required string Description { get; set; }

    public int RangeInKm { get; set; }

    }
}
