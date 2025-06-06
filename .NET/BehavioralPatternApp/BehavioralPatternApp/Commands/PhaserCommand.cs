using BehavioralPatternApp.Receivers;

namespace BehavioralPatternApp.Commands;

public class PhaserCommand(WeaponSystem weapon, bool newState) : ICommand
{
    public void Execute() => weapon.TogglePhaser(newState);

    public void Undo() => weapon.TogglePhaser(!newState);
}