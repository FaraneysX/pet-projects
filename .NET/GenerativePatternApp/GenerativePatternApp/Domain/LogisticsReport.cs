namespace GenerativePatternApp.Domain;

/// <summary>
/// Класс, представляющий отчет о логистике.
/// </summary>
public class LogisticsReport
{
    /// <summary>
    /// Создает новый отчет о логистике на основе фабрики.
    /// </summary>
    /// <param name="factory">Фабрика логистики, предоставляющая транспорт, упаковку и маршрут.</param>
    public LogisticsReport(ILogisticsFactory factory)
    {
        var transport = factory.CreateTransport();
        var packaging = factory.CreatePackaging();

        Route = factory.Route().ToString();
        TransportDetails = transport.DeliveryDetails;
        Cost = transport.Cost;
        DeliveryTime = $"{transport.DeliveryTime.TotalHours} часов";
        PackagingRequirements = packaging.Requirements;
        SafetyInfo = packaging.SafetyInstructions;
    }

    // Маршрут доставки
    private string Route { get; }
    
    // Детали транспорта
    private string TransportDetails { get; }
    
    // Стоимость доставки
    private decimal Cost { get; }
    
    // Время доставки
    private string DeliveryTime { get; }
    
    // Требования к упаковке
    private string PackagingRequirements { get; }
    
    // Инструкции по безопасности
    private string SafetyInfo { get; }

    /// <summary>
    /// Возвращает строковое представление отчета о логистике.
    /// </summary>
    /// <returns>Строка, содержащая детали маршрута, транспорта, стоимости, времени доставки, требований к упаковке и инструкций по безопасности.</returns>
    public override string ToString() => $"Маршрут : {Route}\n" +
                                         $"Транспорт: {TransportDetails}\n" +
                                         $"Стоимость: ${Cost}\n" +
                                         $"Время доставки: {DeliveryTime}\n" +
                                         $"Требования к упаковке: {PackagingRequirements}\n" +
                                         $"Безопасность: {SafetyInfo}\n";
}