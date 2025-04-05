
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Controllers;
public class InputController
{
    private InputHandler _inputHandler;

    public InputController()
    {
        _inputHandler = new InputHandler();
    }

    public void Start()
    {
        _inputHandler.KeyPressed += ControlInputKey;
        _inputHandler.StartListening();
    }

    private void ControlInputKey(object? sender, ConsoleKeyInfo keyInfo)
    {
        try
        { 
            if (char.IsLetter(keyInfo.KeyChar))
            {
                Console.Write(keyInfo.KeyChar);
               
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}