using GenerativePatternApp.Data;
using GenerativePatternApp.Features.Route;

namespace GenerativePatternApp.Domain;

/// <summary>
/// Интерфейс фабрики логистики, которая создает транспорт, упаковку и предоставляет информацию о маршруте.
/// </summary>
public interface ILogisticsFactory
{
    /// <summary>
    /// Создает транспортное средство для доставки.
    /// </summary>
    /// <returns>Объект, реализующий интерфейс <see cref="ITransport"/>.</returns>
    ITransport CreateTransport();
    
    /// <summary>
    /// Создает упаковку для груза.
    /// </summary>
    /// <returns>Объект, реализующий интерфейс <see cref="IPackaging"/>.</returns>
    IPackaging CreatePackaging();
    
    /// <summary>
    /// Предоставляет информацию о маршруте доставки.
    /// </summary>
    /// <returns>Объект, содержащий информацию о маршруте.</returns>
    RouteInfo Route();
}