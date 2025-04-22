using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services.Window;

public class HeaderService
{
    private Button helpButton;
    private string roleText;
    
    public HeaderService()
    {
        helpButton = new Button("help");
    }

    
    public void Render(int width, int height)
    {
        RenderBoard(width, height);
        CalculateElementsPosition();
    }
    
    private void CalculateElementsPosition() {}

    private void RenderBoard(int width, int height)
    {
        for (int x = 1; x < width - 1; x++)
        {
            Console.SetCursorPosition(x, height);
            Console.Write('-');
        }
    }
    
}