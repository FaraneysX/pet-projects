using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Road;

public class WoodCrate : IPackaging
{
    public required string Requirements { get; init; }
    public required string SafetyInstructions { get; init; }
}