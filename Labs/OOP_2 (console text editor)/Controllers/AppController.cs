using System.ComponentModel;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Views;

namespace OOP_2__console_text_editor_.Controllers;

public class AppController
{
    private IDictionary dictionary;
    private IDocumentViewer documentViewer;
    
    private DocumentCreator documentCreator;
    private InputController inputController;
    private DocumentController documentController;
    private CommandProcessor commandProcessor;
    private WindowSizeController windowSizeController;
    private CursorController cursorController;

    public AppController ()
    {
        documentCreator = new DocumentCreator();
        windowSizeController = new WindowSizeController();
        
        inputController = new InputController();
        commandProcessor = new CommandProcessor();
        cursorController = new CursorController();

        documentViewer = new ConsoleDocumentView(windowSizeController);
        documentController = new DocumentController(documentViewer, cursorController);
        
        dictionary = new EditTextCommandDictionary(documentController, commandProcessor);
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
        documentController.SetDocument(document);
        
        inputController.Initialize(dictionary ,commandProcessor);
        inputController.Start(); 
    }
}