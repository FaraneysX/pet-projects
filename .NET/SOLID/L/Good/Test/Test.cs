using Good.Creatures;
using Good.Creatures.AttackerCreatures;
using Good.Creatures.SupportCreatures;

namespace Good.Test;

internal static class Test
{
    public static void Action(Creature creature)
    {
        switch (creature)
        {
            case AttackerCreature attacker:
                attacker.Attack();
                break;
            case SupportCreature supporter:
                supporter.Support();
                break;
            default:
                Console.WriteLine($"{creature.GetType().Name} не выполняет действий.");
                break;
        }
    }
}