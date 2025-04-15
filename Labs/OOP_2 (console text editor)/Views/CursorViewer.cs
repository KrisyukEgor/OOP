using OOP_2__console_text_editor_.Utils;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Views;

public class CursorViewer : ICursorViewer 
{
    private WindowSizeService _windowSizeService;
    
    public CursorViewer(WindowSizeService windowSizeService)
    {
        this._windowSizeService = windowSizeService;
    }
    public void SetCursorPosition(int cursorX, int cursorY)
    {
        Console.SetCursorPosition(cursorX, cursorY);
    }
    
}