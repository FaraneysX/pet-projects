namespace Good.Creatures.SupportCreatures;

/// <summary>
///     Существа поддержки.
/// </summary>
/// <param name="name">Имя.</param>
internal abstract class SupportCreature(string name) : Creature(name)
{
    /// <summary>
    ///     Поддержка.
    /// </summary>
    public abstract void Support();
}