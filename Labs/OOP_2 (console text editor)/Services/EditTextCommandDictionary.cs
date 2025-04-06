using OOP_2__console_text_editor_.Commands;
using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class EditTextCommandDictionary : IDictionary
{
    private readonly Dictionary<ConsoleKey, ICommand> _commands = new();
    private DocumentController _documentController;

    public EditTextCommandDictionary(DocumentController documentController)
    {
        _commands.Add(ConsoleKey.LeftArrow, new MoveCursorLeftCommand()); 
        _documentController = documentController;
    }
    
    public ICommand? GetCommand(ConsoleKeyInfo key)
    {
        if (_commands.TryGetValue(key.Key, out var command))
        {
            return command;
        }
        
        if (char.IsLetter(key.KeyChar))
        {
            return new PrintCharCommand(_documentController, key.KeyChar);
            
        }
        return null;

    }
    
}