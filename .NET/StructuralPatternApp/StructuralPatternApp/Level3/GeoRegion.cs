namespace StructuralPatternApp.Level3;

public class GeoRegion(string name, double temperature) : CompositeGeoComponent(name)
{
    private double AverageTemperature { get; } = temperature;

    public override void DisplayDetails(int depth)
    {
        Console.WriteLine($"{new string(' ', depth)}Температура: {AverageTemperature}\n" +
                          $"{new string(' ', depth)}Климат: {GetClimateType()}");
        
        base.DisplayDetails(depth);
    }

    private string GetClimateType() => AverageTemperature switch
    {
        > 20 => "Тропики",
        > 10 => "Умеренный",
        _ => "Полярный",
    };
}