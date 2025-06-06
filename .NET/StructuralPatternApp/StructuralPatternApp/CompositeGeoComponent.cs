namespace StructuralPatternApp;

/// <summary>
/// Абстрактный класс, представляющий составной географический компонент.
/// </summary>
/// <param name="name">Название компонента.</param>
public abstract class CompositeGeoComponent(string name) : IGeoComponent
{
    // Список дочерних компонентов.
    private readonly List<IGeoComponent> _components = [];

    /// <summary>
    /// Добавляет дочерний компонент.
    /// </summary>
    /// <param name="component">Компонент для добавления.</param>
    public void Add(IGeoComponent component) => _components.Add(component);

    /// <summary>
    /// Возвращает общее количество ресурсов указанного типа в компоненте и всех его дочерних компонентах.
    /// </summary>
    /// <param name="type">Тип ресурса (например, "уголь", "вода").</param>
    /// <returns>Общее количество ресурсов указанного типа.</returns>
    public virtual double GetTotalResources(string type)
    {
        return _components.Sum(c => c.GetTotalResources(type));
    }

    /// <summary>
    /// Отображает детали компонента и всех его дочерних компонентов с учетом уровня вложенности.
    /// </summary>
    /// <param name="depth">Уровень вложенности (для отступов при выводе).</param>
    public virtual void DisplayDetails(int depth)
    {
        // Вывод названия текущего компонента.
        Console.WriteLine($"{new string(' ', depth)}Название: " + name);
        
        // Рекурсивный вывод деталей дочерних компонентов.
        foreach (var c in _components)
        {
            c.DisplayDetails(depth + 4);
        }
    }
    
    /// <summary>
    /// Возвращает имя компонента.
    /// </summary>
    /// <returns>Имя компонента.</returns>
    public string GetName() => name;
}