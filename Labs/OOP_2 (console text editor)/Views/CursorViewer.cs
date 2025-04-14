using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Views;

public class CursorViewer : ICursorViewer 
{
    private WindowSizeController windowSizeController;
    
    public CursorViewer(WindowSizeController windowSizeController)
    {
        this.windowSizeController = windowSizeController;
    }
    public void SetCursorPosition(int cursorX, int cursorY)
    {
        Console.SetCursorPosition(cursorX, cursorY);
    }
    
}