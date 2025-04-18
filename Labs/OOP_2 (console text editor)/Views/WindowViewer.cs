using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Views;

public class WindowViewer : IWindowViewer
{
    public WindowViewer()
    {
        
    }
    public void RenderBoard(int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            string line = "";
        
            if (y == 0 || y == height - 1)
            {
                line += "*";
                line += new string('-', width - 2);
                line += "*";
            }
            else
            {
                line += "|";
                line += new string(' ', width - 2);
                line += "|";
            }

            Console.SetCursorPosition(0, y);
            Console.Write(line);
        }
    }
}