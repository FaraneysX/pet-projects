namespace Bad.Creatures;

/// <summary>
///     Фея.
/// </summary>
internal class FairyCreature(string name) : Creature(name)
{
    public override void Attack()
    {
        throw new NotImplementedException($"Фея {Name} не может атаковать.");
    }
}