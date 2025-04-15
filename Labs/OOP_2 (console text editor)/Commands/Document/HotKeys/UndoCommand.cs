using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.HotKeys;

public class UndoCommand : ICommand
{
    private CommandProcessor _commandProcessor;
    public UndoCommand(CommandProcessor commandProcessor)
    {
        _commandProcessor = commandProcessor;
    }
    public void Execute()
    {   
        _commandProcessor.Undo();
    }

    public void UnExecute()
    {
        
    }
}