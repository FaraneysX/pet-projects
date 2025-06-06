namespace Bad.Creatures;

/// <summary>
///     Дракон.
/// </summary>
internal class DragonCreature(string name) : Creature(name)
{
    public override void Attack()
    {
        Console.WriteLine($"Дракон {Name} атакует.");
    }
}