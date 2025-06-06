using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Sea;

public class Ship : ITransport
{
    public required string DeliveryDetails { get; init; }
    public required decimal Cost { get; init; }
    public required TimeSpan DeliveryTime { get; init; }
}