namespace StructuralPatternApp;

/// <summary>
/// Интерфейс, представляющий географический компонент.
/// </summary>
public interface IGeoComponent
{
    /// <summary>
    /// Получает общее количество ресурсов указанного типа в компоненте.
    /// </summary>
    /// <param name="type">Тип ресурса (например, "уголь", "вода").</param>
    /// <returns>Общее количество ресурсов указанного типа.</returns>
    double GetTotalResources(string type);
    
    /// <summary>
    /// Отображает детали компонента с учетом уровня вложенности.
    /// </summary>
    /// <param name="depth">Уровень вложенности (для отступов при выводе).</param>
    void DisplayDetails(int depth);
}