namespace GenerativePatternApp.Features.Route;

/// <summary>
/// Класс, представляющий информацию о маршруте.
/// </summary>
public class RouteInfo
{
    /// <summary>
    /// Начальная точка маршрута.
    /// </summary>
    public required string StartPoint { get; init; }
    
    /// <summary>
    /// Путь или промежуточные точки маршрута.
    /// </summary>
    public required string Path { get; init; }
    
    /// <summary>
    /// Конечный пункт назначения.
    /// </summary>
    public required string Destination { get; init; }

    /// <summary>
    /// Возвращает строковое представление маршрута.
    /// </summary>
    /// <returns>Строка в формате "StartPoint -> Path -> Destination".</returns>
    public override string ToString()
    {
        return $"{StartPoint} -> {Path} -> {Destination}";
    }
}