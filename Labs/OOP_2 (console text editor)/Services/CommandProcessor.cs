using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Services;

public class CommandProcessor
{
    private readonly List<ICommand> _undoList = new();
    private readonly List<ICommand> _redoList = new();
    private int maxCount = 100;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        AddToHistory(_undoList, command);
        _redoList.Clear();
    }

    public void Undo()
    {
        if (_undoList.Count == 0) return;
        
        var command = _undoList[^1];
        
        _undoList.RemoveAt(_undoList.Count - 1);
        AddToHistory(_redoList, command);
        
        command.UnExecute();
    }

    public void Redo()
    {
        if (_redoList.Count == 0) return;
        
        var command = _redoList[^1];
        
        _redoList.RemoveAt(_redoList.Count - 1);
        AddToHistory(_undoList, command);
        command.Execute();
    }

    private void AddToHistory(List<ICommand> list, ICommand command)
    {
        list.Add(command);

        if (list.Count > maxCount)
        {
            int overflow = list.Count - maxCount;
            list.RemoveRange(0, overflow);
        }
    }
}