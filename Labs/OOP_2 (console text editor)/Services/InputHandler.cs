namespace OOP_2__console_text_editor_.Services;

public class InputHandler
{
    private bool _isRunning = true;
    public event EventHandler<ConsoleKeyInfo>? KeyPressed;

    
    private bool _shiftState;

    public void StartListening()
    {
        while (_isRunning)
        {
            var keyInfo = Console.ReadKey(intercept: true);
            
            OnKeyPressed(keyInfo);
            Thread.Sleep(10);
        }
    }

    
    protected virtual void OnKeyPressed(ConsoleKeyInfo keyInfo)
    {
        KeyPressed?.Invoke(this, keyInfo);
    }

    public void StopListening()
    {
        _isRunning = false;
    }
}