namespace Good.Creatures.AttackerCreatures;

/// <summary>
///     Атакующие существа.
/// </summary>
/// <param name="name">Имя.</param>
internal abstract class AttackerCreature(string name) : Creature(name)
{
    /// <summary>
    ///     Атака.
    /// </summary>
    public abstract void Attack();
}