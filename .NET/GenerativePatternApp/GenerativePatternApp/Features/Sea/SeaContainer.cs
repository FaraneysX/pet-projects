using GenerativePatternApp.Data;

namespace GenerativePatternApp.Features.Sea;

public class SeaContainer : IPackaging
{
    public required string Requirements { get; init; }
    public required string SafetyInstructions { get; init; }
}