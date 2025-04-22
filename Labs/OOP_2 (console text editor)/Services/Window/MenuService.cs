using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Utils;
using OOP_2__console_text_editor_.Utils.Dictionaries;
using OOP_2__console_text_editor_.Views;

namespace OOP_2__console_text_editor_.Services.Window;

public class MenuService
{
    private IDictionary pageDictionary;
    private IPageViewer pageViewer;
    
    private PageService pageService;
    private PageController pageController;
    private WindowSizeService _windowSizeService;
    private InputController inputController;
    
    public MenuService(WindowSizeService windowSizeService, InputController inputController, ButtonSetClickService buttonSetClickService)
    {
        this._windowSizeService = windowSizeService;

        pageViewer = new PageViewer(windowSizeService);
        pageController = new PageController(pageViewer, this._windowSizeService);
        pageService = new PageService(pageController, buttonSetClickService);
        
        pageDictionary = new PageDictionary(pageService);
        
        this.inputController = inputController;
        
    }

    public void Focus()
    {
        inputController.SetDictionary(pageDictionary);
        inputController.ListenPage();
    }

    public void RenderDocumentStatePage()
    {
        pageService.RenderDocumentStatePage();
    }

    public void RenderSavePage()
    {
        pageService.RenderSavePage();
    }
    
}