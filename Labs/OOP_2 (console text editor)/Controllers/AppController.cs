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
    private IDictionary documentDictionary;
    private IDictionary pageDictionary;
    private IDocumentViewer documentViewer;
    private ICursorViewer cursorViewer;
    private DocumentController documentController ;
    
    private DocumentCreator documentCreator = new();
    private InputController inputController = new();
    private CommandProcessor commandProcessor = new();
    private WindowSizeService _windowSizeService = new();
    private CursorController cursorController;
    private PageService pageService;
    
    private TextEditService _textEditService;
    private IBuffer buffer;
    private PageController pageController;
    private IPageViewer pageViewer;

    public AppController ()
    {
        buffer = new DocumentBuffer();
        
        documentViewer = new ConsoleDocumentView(_windowSizeService);
        cursorViewer = new CursorViewer(_windowSizeService);
        pageViewer = new PageViewer(_windowSizeService);
        
        cursorController = new CursorController(cursorViewer);
        documentController = new DocumentController(documentViewer);
        pageController = new PageController(pageViewer, _windowSizeService);
        pageService = new PageService(pageController);
        
        _textEditService = new TextEditService(cursorController, buffer, documentController);
        documentDictionary = new DocumentCommandDictionary(_textEditService, commandProcessor);
        pageDictionary = new PageDictionary(pageService);
    }
    public void Start()
    {
        pageService.RenderDocumentStatePage(); 
        inputController.Initialize(pageDictionary ,commandProcessor);
        
        inputController.ListenPage();

    }

    private void NewDocument()
    {
        Document newDocument = documentCreator.CreateDocument();
        ModifyDocument(newDocument);
    }


    private void OldDocument()
    {
        Document oldDocument = documentCreator.OpenDocument();
        ModifyDocument(oldDocument);
        
    }

    private void ModifyDocument(Document document)
    {
        _textEditService.SetDocument(document);
        
        inputController.SetDictionary(documentDictionary);
        inputController.ListenDocument();
    }
}