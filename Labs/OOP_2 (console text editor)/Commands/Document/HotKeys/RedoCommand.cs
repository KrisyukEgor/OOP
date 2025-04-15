using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.HotKeys;

public class RedoCommand : ICommand
{
    private CommandProcessor _commandProcessor;
    public RedoCommand(CommandProcessor commandProcessor)
    {
        _commandProcessor = commandProcessor;
    }
    public void Execute()
    {   
        _commandProcessor.Redo();
    }

    public void UnExecute()
    {
        
    }
}