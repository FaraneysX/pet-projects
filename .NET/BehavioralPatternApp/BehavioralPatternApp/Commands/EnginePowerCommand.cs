using BehavioralPatternApp.Receivers;

namespace BehavioralPatternApp.Commands;

public class EnginePowerCommand(EngineSystem engine, int newPower) : ICommand
{
    private int _previousPower;

    public void Execute()
    {
        _previousPower = engine.GetCurrentPower();
        engine.SetPower(newPower);
    }

    public void Undo() => engine.SetPower(_previousPower);
}