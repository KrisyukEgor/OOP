using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Controllers;

public class PageController
{
    private IPageViewer _pageViewer;
    private WindowSizeService _windowSizeService;

    private const int ButtonHeight = 5;
    private const int ButtonWidth = 21;
    private const int VerticalMargin = 2;
    
    
    public PageController(IPageViewer pageViewer, WindowSizeService windowSizeService)
    {
        this._pageViewer = pageViewer;
        this._windowSizeService = windowSizeService;
    }
    
    public void RenderPage(Page page)
    {
        _pageViewer.RenderPage(page);
    }

    public void CalculateButtonsParameters(List<Button> buttons)
    {
        CalculateButtonsDimensions(buttons);
        CalculateButtonsPosition(buttons);
    }

    private void CalculateButtonsDimensions(List<Button> buttons)
    {
        foreach (var button in buttons)
        {
            button.Width = ButtonWidth;
            button.Height = ButtonHeight;
        }
    }

    private void CalculateButtonsPosition(List<Button> buttons)
    {
        int windowWidth = _windowSizeService.Width;
        int windowHeight = _windowSizeService.Height;
        
        int headerHeight = _windowSizeService.HeaderHeight;
        
        int startY = (windowHeight + headerHeight - (buttons.Count * (ButtonHeight + VerticalMargin))) / 2;
        
        int currentY = startY;
        
        foreach (var button in buttons)
        {
            button.X = (windowWidth - button.Width) / 2;
            button.Y = currentY;
            
            currentY += ButtonHeight + VerticalMargin;
        }
    }

    
    
}