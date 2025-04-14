using OOP_2__console_text_editor_.Commands.Cursor;
using OOP_2__console_text_editor_.Commands.HotKeys;
using OOP_2__console_text_editor_.Commands.Select;
using OOP_2__console_text_editor_.Commands.Text;
using OOP_2__console_text_editor_.Commands.TextDecorator;
using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class DocumentCommandDictionary : IDictionary
{
    private readonly Dictionary<(ConsoleKey Key, ConsoleModifiers Mods), Func<ICommand>> _commands = new();
    private TextEditService _textEditService;
    private CommandProcessor _commandProcessor;
    private CursorController _cursorController;

    public DocumentCommandDictionary(TextEditService _textEditService, CommandProcessor commandProcessor, CursorController cursorController)
    {
        this._textEditService = _textEditService;
        _commandProcessor = commandProcessor;
        _cursorController = cursorController;
        

        InizializeCommands();
    }
    
    public ICommand? GetCommand(ConsoleKeyInfo key)
    {
        var turpleKey = (key.Key, key.Modifiers);
        
        if (_commands.TryGetValue(turpleKey, out var commandFunc))
        {
            return commandFunc();
        }
        
        if (!char.IsControl(key.KeyChar))
        {
            return new PrintCharCommand(_textEditService, key.KeyChar);
            
        }
        return null;

    }


    private void InizializeCommands()
    {
        _commands.Add((ConsoleKey.LeftArrow, 0), () => new MoveCursorLeftCommand(_cursorController)); 
        _commands.Add((ConsoleKey.RightArrow, 0 ), () => new MoveCursorRightCommand(_cursorController)); 
        _commands.Add((ConsoleKey.UpArrow, 0 ), () => new MoveCursorUpCommand(_cursorController)); 
        _commands.Add((ConsoleKey.DownArrow, 0 ), () => new MoveCursorDownCommand(_cursorController)); 
        
        _commands.Add((ConsoleKey.Backspace, 0), () => new BackspaceCommand(_textEditService)); 
        _commands.Add((ConsoleKey.Enter, 0), () => new EnterCommand(_textEditService)); 
        
        _commands.Add((ConsoleKey.Z, ConsoleModifiers.Control), () => new UndoCommand(_commandProcessor));
        _commands.Add((ConsoleKey.Y, ConsoleModifiers.Control), () => new RedoCommand(_commandProcessor));
        
        _commands.Add((ConsoleKey.RightArrow, ConsoleModifiers.Shift), () => new SelectRightCommand(_textEditService));
        _commands.Add((ConsoleKey.LeftArrow, ConsoleModifiers.Shift), () => new SelectLeftCommand(_textEditService));
        
        _commands.Add((ConsoleKey.B, ConsoleModifiers.Control), () => new BoldCommand(_textEditService));
        _commands.Add((ConsoleKey.I, ConsoleModifiers.Control), () => new ItalicCommand(_textEditService));
        _commands.Add((ConsoleKey.U, ConsoleModifiers.Control), () => new UnderlineCommand(_textEditService));
        
        _commands.Add((ConsoleKey.C, ConsoleModifiers.Control), () => new CopyCommand(_textEditService));
        _commands.Add((ConsoleKey.V, ConsoleModifiers.Control), () => new PasteCommand(_textEditService));
        

    }
    
}