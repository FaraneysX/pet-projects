using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Road;

public class Truck : ITransport
{
    public required string DeliveryDetails { get; init; }
    public required decimal Cost { get; init; }
    public required TimeSpan DeliveryTime { get; init; }
}