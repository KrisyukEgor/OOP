using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Page;
using OOP_2__console_text_editor_.Views;

namespace OOP_2__console_text_editor_.Controllers;

public class WindowController
{
    private IWindowViewer _windowViewer;
    private WindowService _windowService;
    private HeaderService _headerService;
    
    private const int headerHeight = 5;
    public WindowController(IWindowViewer windowViewer, WindowService windowService, HeaderService headerService)
    {
        _windowViewer = windowViewer;
        _windowService = windowService;
        _headerService = headerService;
    }

    private void RenderBorder()
    {
        int width = _windowService.Width;
        int height = _windowService.Height;

        _windowViewer.RenderBoard(width, height);
    }

    public void RenderWindow()
    {
        RenderBorder();
        RenderHeader();
        RenderMain();
    }

    private void RenderHeader()
    {
        int width = _windowService.Width;
        int height = _windowService.HeaderHeight;
        
        _headerService.Render(width, height);
    }

    private void RenderMain()
    {
        
    }


}