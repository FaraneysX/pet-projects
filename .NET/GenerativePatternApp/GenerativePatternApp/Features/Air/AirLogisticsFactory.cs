using GenerativePatternApp.Data;
using GenerativePatternApp.Domain;
using GenerativePatternApp.Features.Route;

namespace GenerativePatternApp.Features.Air;

public class AirLogisticsFactory : ILogisticsFactory
{
    public ITransport CreateTransport() => new Airplane
    {
        DeliveryDetails = "Морская доставка",
        Cost = 1500m,
        DeliveryTime = TimeSpan.FromDays(14)
    };

    public IPackaging CreatePackaging() => new AirCargo
    {
        Requirements = "Авиационная упаковка",
        SafetyInstructions = "Сертификат безопасности обязателен"
    };

    public RouteInfo Route() => new()
    {
        StartPoint = "Аэропорт",
        Destination = "Воздушный коридор",
        Path = "Пункт выгрузки"
    };
}