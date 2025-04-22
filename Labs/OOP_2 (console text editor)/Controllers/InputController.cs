using OOP_2__console_text_editor_.Commands.Document.HotKeys;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Controllers;
public class InputController
{
    private InputHandler _inputHandler;
    private IDictionary _dictionary;
    private CommandProcessor _commandProcessor;
    
    private bool _shiftDown;
    
    public InputController()
    {
        _inputHandler = new InputHandler();
    }

    public void Initialize(CommandProcessor commandProcessor)
    {
        _commandProcessor = commandProcessor;
    }

    public void SetDictionary(IDictionary dictionary)
    {
        _dictionary = dictionary;
    }
    
    private void ControlDocumentInput(object? sender, ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            _inputHandler.StopListening();
            _inputHandler.KeyPressed -= ControlDocumentInput;
            
        }
        
        try
        { 
            var command = _dictionary.GetCommand(keyInfo);
            
            if (command is UndoCommand || command is RedoCommand)
            {
                command.Execute();
            }
            else if(command != null)
            {
                _commandProcessor.ExecuteCommand(command);
            }

            
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ControlPageInput(object? sender, ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            _inputHandler.StopListening();
            _inputHandler.KeyPressed -= ControlPageInput;
        }
        
        try
        { 
            var command = _dictionary.GetCommand(keyInfo);

            if(command != null)
            {
                _commandProcessor.ExecuteCommand(command);
            }
            
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    public void ListenDocument()
    {
        _inputHandler.KeyPressed += ControlDocumentInput;
        _inputHandler.KeyPressed -= ControlPageInput;
        
        _inputHandler.StartListening();
    }

    public void ListenPage()
    {
        _inputHandler.KeyPressed += ControlPageInput;
        _inputHandler.KeyPressed -= ControlDocumentInput;
        
        _inputHandler.StartListening();
    }
    
}