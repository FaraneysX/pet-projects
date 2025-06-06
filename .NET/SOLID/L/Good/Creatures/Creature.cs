namespace Good.Creatures;

/// <summary>
///     Базовый класс для всех существ.
/// </summary>
internal abstract class Creature(string name)
{
    /// <summary>
    ///     Имя.
    /// </summary>
    protected string Name { get; } = name;
}