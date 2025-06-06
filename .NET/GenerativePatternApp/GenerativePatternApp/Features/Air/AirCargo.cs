using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Air;

public class AirCargo : IPackaging
{
    public required string Requirements { get; init; }
    public required string SafetyInstructions { get; init; }
}