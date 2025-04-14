using System.ComponentModel;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Views;
using Buffer = OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class AppController
{
    private IDictionary dictionary;
    private IDocumentViewer documentViewer;
    private ICursorViewer cursorViewer;
    private DocumentController documentController ;
    
    private DocumentCreator documentCreator = new();
    private InputController inputController = new();
    private CommandProcessor commandProcessor = new();
    private WindowSizeController windowSizeController = new();
    private CursorController cursorController;

    private TextEditService _textEditService;
    private IBuffer buffer;

    public AppController ()
    {
        buffer = new DocumentBuffer();
        
        documentViewer = new ConsoleDocumentView(windowSizeController);
        cursorViewer = new CursorViewer(windowSizeController);
        
        cursorController = new CursorController(cursorViewer);
        
        documentController = new DocumentController(documentViewer);
        
        _textEditService = new TextEditService(cursorController, buffer, documentController);
        dictionary = new DocumentCommandDictionary(_textEditService, commandProcessor, cursorController);
    }
    public void Start()
    {
        //menu
        int state = 1;
        
        if (state == 1)
        {
            NewDocument();
        }
        else if (state == 2)
        {
            OldDocument();
        }
        
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
        
        inputController.Initialize(dictionary ,commandProcessor);
        inputController.Start(); 
    }
}