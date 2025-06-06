using GenerativePatternApp.Data;
using GenerativePatternApp.Domain;
using GenerativePatternApp.Features.Route;

namespace GenerativePatternApp.Features.Sea;

public class SeaLogisticsFactory : ILogisticsFactory
{
    public ITransport CreateTransport() => new Ship
    {
        DeliveryDetails = "Морская доставка",
        Cost = 1500m,
        DeliveryTime = TimeSpan.FromDays(14)
    };

    public IPackaging CreatePackaging() => new SeaContainer
    {
        Requirements = "Морской контейнер, крепление груза",
        SafetyInstructions = "Защита от коррозии"
    };

    public RouteInfo Route() => new()
    {
        StartPoint = "Порт",
        Path = "Морской путь",
        Destination = "Терминал назначения"
    };
}