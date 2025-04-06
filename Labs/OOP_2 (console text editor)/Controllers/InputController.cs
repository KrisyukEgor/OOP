
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Controllers;
public class InputController
{
    private InputHandler _inputHandler;
    private IDictionary _dictionary;
    private CommandProcessor _commandProcessor;
    
    public InputController()
    {
        _inputHandler = new InputHandler();
    }

    public void Initialize(IDictionary dictionary, CommandProcessor commandProcessor)
    {
        _dictionary = dictionary;
        _commandProcessor = commandProcessor;
    }
    
    public void Start()
    {
        _inputHandler.KeyPressed += ControlInputKey;
        _inputHandler.StartListening();
    }

    private void ControlInputKey(object? sender, ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            _inputHandler.StopListening();
            _inputHandler.KeyPressed -= ControlInputKey;
        }
        try
        { 
            ICommand? command = _dictionary.GetCommand(keyInfo);
            if (command != null)
            {
                _commandProcessor.ExecuteCommand(command);
            }
            
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}