namespace Good.Creatures.AttackerCreatures;

/// <summary>
///     Дракон.
/// </summary>
/// <param name="name">Имя.</param>
internal class DragonCreature(string name) : AttackerCreature(name)
{
    public override void Attack()
    {
        Console.WriteLine($"Дракон {Name} атакует.");
    }
}