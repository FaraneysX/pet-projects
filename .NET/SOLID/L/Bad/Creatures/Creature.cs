namespace Bad.Creatures;

/// <summary>
///     Базовый класс для всех существ.
/// </summary>
internal abstract class Creature(string name)
{
    /// <summary>
    ///     Имя.
    /// </summary>
    protected string Name { get; } = name;

    /// <summary>
    ///     Атака существа.
    /// </summary>
    public virtual void Attack()
    {
        Console.WriteLine($"Существо {Name} атакует.");
    }
}