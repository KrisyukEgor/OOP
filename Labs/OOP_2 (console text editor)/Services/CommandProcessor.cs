using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Services;

public class CommandProcessor
{
    private readonly Stack<ICommand> _undoStack = new();
    private readonly Stack<ICommand> _redoStack = new();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
        _redoStack.Clear();
    }

    public void Undo()
    {
        if (_undoStack.Count == 0) return;
        
        var command = _undoStack.Pop();
        _redoStack.Push(command);
        command.UnExecute();
    }

    public void Redo()
    {
        if (_redoStack.Count == 0) return;
        
        var command = _redoStack.Pop();
        _undoStack.Push(command);
        command.Execute();
    }
    
}