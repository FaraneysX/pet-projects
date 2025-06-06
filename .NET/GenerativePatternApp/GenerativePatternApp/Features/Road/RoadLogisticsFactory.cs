using GenerativePatternApp.Data;
using GenerativePatternApp.Domain;
using GenerativePatternApp.Features.Road;
using GenerativePatternApp.Features.Route;

namespace GenerativePatternApp.Features.Road;

public class RoadLogisticsFactory : ILogisticsFactory
{
    public ITransport CreateTransport() => new Truck
    {
        DeliveryDetails = "Наземная доставка",
        Cost = 500m,
        DeliveryTime = TimeSpan.FromDays(2)
    };

    public IPackaging CreatePackaging() => new WoodCrate
    {
        Requirements = "Деревянная тара, защита от влаги",
        SafetyInstructions = "Максимальный вес 1000 кг"
    };

    public RouteInfo Route() => new()
    {
        StartPoint = "Склад",
        Destination = "Автотрасса",
        Path = "Пункт назначения"
    };
}