using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Views;

public class PageViewer : IPageViewer
{
    private WindowSizeService _windowSizeService;
    public PageViewer(WindowSizeService windowSizeService)
    {
        _windowSizeService = windowSizeService;
    }
    public void RenderPage(Page page)
    {
        
        foreach (var button in page.GetButtons())
        {
            RenderButton(button);
        }
    }
    
    private void RenderButton(Button button)
    {
        Console.ResetColor();
        
        if (button.IsSelected)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
        }
        
        int textX = button.X + (button.Width - button.Text.Length) / 2;
        int textY = button.Y + button.Height / 2;

        for (int y = 0; y < button.Height; y++)
        {
            Console.SetCursorPosition(button.X, button.Y + y);
        
            if (y == 0)
            {
                Console.Write("*" + new string('-', button.Width - 2) + "*");
            }
            else if (y == button.Height - 1) 
            {
                Console.Write("*" + new string('-', button.Width - 2) + "*");
            }
            else 
            {
                Console.Write("|" + new string(' ', button.Width - 2) + "|");
            }
        }
        
        Console.SetCursorPosition(Math.Max(textX, button.X + 1), Math.Min(textY, button.Y + button.Height - 2));
    
        string visibleText = button.Text.Length > button.Width - 2 ? button.Text.Substring(0, button.Width - 2) : button.Text;

        Console.Write(visibleText);
    
        Console.ResetColor();
    }
}