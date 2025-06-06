namespace StructuralPatternApp.Level4;

public class MineralDeposit(string currentType, double volume) : IGeoComponent
{
    public double GetTotalResources(string type) => currentType == type ? volume : 0;

    public void DisplayDetails(int depth)
    {
        Console.WriteLine($"{new string(' ', depth)}Минерал: {currentType} ({volume})");
    }
}