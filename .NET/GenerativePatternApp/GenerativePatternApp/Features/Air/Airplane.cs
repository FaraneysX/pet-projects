using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Air;

public class Airplane : ITransport
{
    public required string DeliveryDetails { get; init; }
    public required decimal Cost { get; init; }
    public required TimeSpan DeliveryTime { get; init; }
}