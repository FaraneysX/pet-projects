using BehavioralPatternApp.Commands;

namespace BehavioralPatternApp;

public class StarshipControl
{
    private readonly Stack<ICommand> _commandHistory = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count == 0)
        {
            return;
        }
        
        var lastCommand = _commandHistory.Pop();
        lastCommand.Undo();
    }
}