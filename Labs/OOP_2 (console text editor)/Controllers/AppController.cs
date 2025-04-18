using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Services.Page;
using OOP_2__console_text_editor_.Utils;
using OOP_2__console_text_editor_.Views;

namespace OOP_2__console_text_editor_.Controllers;

public class AppController
{
    private IBuffer buffer;
    
    private IDocumentViewer documentViewer;
    private ICursorViewer cursorViewer;
    private IPageViewer pageViewer;
    private IWindowViewer windowViewer;
    
    private DocumentController documentController ;
    private PageController pageController;
    private WindowController _windowController;
    private CursorController cursorController;
    private InputController inputController = new();
    
    private PageService pageService;
    private TextEditService _textEditService;
    private HeaderService headerService = new();
    private IDictionary documentDictionary;
    private IDictionary pageDictionary;
    
    private DocumentCreator documentCreator = new();
    private CommandProcessor commandProcessor = new();
    private WindowService _windowService = new();
    
    
    public AppController ()
    {
        buffer = new DocumentBuffer();
        
        documentViewer = new ConsoleDocumentView(_windowService);
        cursorViewer = new CursorViewer(_windowService);
        pageViewer = new PageViewer(_windowService);
        windowViewer = new WindowViewer();
        
        cursorController = new CursorController(cursorViewer);
        documentController = new DocumentController(documentViewer);
        pageController = new PageController(pageViewer, _windowService);
        _windowController = new WindowController(windowViewer, _windowService, headerService);
        
        pageService = new PageService(pageController);
        _textEditService = new TextEditService(cursorController, buffer, documentController);
        documentDictionary = new DocumentCommandDictionary(_textEditService, commandProcessor);
        pageDictionary = new PageDictionary(pageService);
        
    }
    public void Start()
    {
        _windowController.RenderWindow();
        pageService.RenderDocumentStatePage(); 
        inputController.Initialize(pageDictionary ,commandProcessor);
        
        inputController.ListenPage();

    }
    
    private void ModifyDocument(Document document)
    {
        _textEditService.SetDocument(document);
        
        inputController.SetDictionary(documentDictionary);
        inputController.ListenDocument();
    }
}