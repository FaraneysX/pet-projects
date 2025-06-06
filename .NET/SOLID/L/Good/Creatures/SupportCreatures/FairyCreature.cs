namespace Good.Creatures.SupportCreatures;

/// <summary>
///     Фея.
/// </summary>
/// <param name="name">Имя.</param>
internal class FairyCreature(string name) : SupportCreature(name)
{
    public override void Support()
    {
        Console.WriteLine($"Фея {Name} исцеляет союзников.");
    }
}