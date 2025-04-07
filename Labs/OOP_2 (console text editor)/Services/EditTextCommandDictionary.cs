using OOP_2__console_text_editor_.Commands.Cursor;
using OOP_2__console_text_editor_.Commands.HotKeys;
using OOP_2__console_text_editor_.Commands.Text;
using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class EditTextCommandDictionary : IDictionary
{
    private readonly Dictionary<(ConsoleKey Key, ConsoleModifiers Mods), ICommand> _commands = new();
    private DocumentController _documentController;
    private CommandProcessor _commandProcessor;

    public EditTextCommandDictionary(DocumentController documentController, CommandProcessor commandProcessor)
    {
        _documentController = documentController;
        _commandProcessor = commandProcessor;

        InizializeCommands();
    }
    
    public ICommand? GetCommand(ConsoleKeyInfo key)
    {
        var turpleKey = (key.Key, key.Modifiers);
        
        if (_commands.TryGetValue(turpleKey, out var command))
        {
            return command;
        }
        
        if (!char.IsControl(key.KeyChar))
        {
            return new PrintCharCommand(_documentController, key.KeyChar);
            
        }
        return null;

    }


    private void InizializeCommands()
    {
        _commands.Add((ConsoleKey.LeftArrow, 0), new MoveCursorLeftCommand(_documentController)); 
        _commands.Add((ConsoleKey.RightArrow, 0 ), new MoveCursorRightCommand(_documentController)); 
        
        _commands.Add((ConsoleKey.Backspace, 0), new BackspaceCommand(_documentController)); 
        _commands.Add((ConsoleKey.Enter, 0), new EnterCommand()); 
        
        _commands.Add((ConsoleKey.Z, ConsoleModifiers.Control), new UndoCommand(_commandProcessor));
        _commands.Add((ConsoleKey.Y, ConsoleModifiers.Control), new RedoCommand(_commandProcessor));
    }
    
}