using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Services.Window;

namespace OOP_2__console_text_editor_.Controllers;

public class WindowController
{
    private IWindowViewer _windowViewer;
    private WindowSizeService _windowSizeService;
    private HeaderService _headerService;

    private MenuService _menuService;
    private DocumentService _documentService;
    private InputController _inputController = new();

    private CommandProcessor commandProcessor = new();

    private ButtonSetClickService buttonSetClickService;

    public WindowController(IWindowViewer windowViewer, WindowSizeService windowSizeService)
    {
        _windowViewer = windowViewer;
        _windowSizeService = windowSizeService;
        _headerService = new HeaderService();

        _documentService = new DocumentService(windowSizeService, commandProcessor, _inputController);
        buttonSetClickService = new ButtonSetClickService(_documentService);

        _menuService = new MenuService(windowSizeService, _inputController, buttonSetClickService);

        _inputController.Initialize(commandProcessor);
    }

    private void RenderBorder()
    {
        int width = _windowSizeService.Width;
        int height = _windowSizeService.Height;

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
        int width = _windowSizeService.Width;
        int height = _windowSizeService.HeaderHeight;

        _headerService.Render(width, height);
    }

    private void RenderMain()
    {
        _menuService.RenderDocumentStatePage();
        _menuService.Focus();
    }
}