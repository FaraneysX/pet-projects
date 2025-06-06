namespace GenerativePatternApp.Data;

/// <summary>
/// Интерфейс, представляющий транспортное средство для доставки.
/// </summary>
public interface ITransport
{
    /// <summary>
    /// Получает детали доставки, включая описание транспорта и способа доставки.
    /// </summary>
    string DeliveryDetails { get; }
    
    /// <summary>
    /// Получает стоимость доставки в денежных единицах.
    /// </summary>
    decimal Cost { get; }
    
    /// <summary>
    /// Получает время, необходимое для доставки.
    /// </summary>
    TimeSpan DeliveryTime { get; }
}