using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services.Window;
using OOP_2__console_text_editor_.Utils;
using OOP_2__console_text_editor_.Utils.Dictionaries;
using OOP_2__console_text_editor_.Views;

namespace OOP_2__console_text_editor_.Services.Document;

public class DocumentService
{
    private TextEditService _textEditService;
    private IDocumentViewer documentViewer;
    private IBuffer buffer;
    private IDictionary documentDictionary;
    
    private DocumentController documentController ;
    private CursorController cursorController;
    private InputController inputController;
    
    private Models.Document? document = null;

    public DocumentService(WindowSizeService windowSizeService, CommandProcessor commandProcessor, InputController inputController)
    {
        buffer = new DocumentBuffer();
        documentViewer = new ConsoleDocumentView(windowSizeService);
        
        cursorController = new CursorController(documentViewer);
        documentController = new DocumentController(documentViewer);
        
        _textEditService = new TextEditService(cursorController, buffer, documentController);
        
        documentDictionary = new DocumentCommandDictionary(_textEditService, commandProcessor);
        this.inputController = inputController;
    }

    public void Focus()
    {
        if (document != null)
        {
            Focus(document);
        }
    }

    public void Focus(Models.Document doc)
    {
        document = doc;
        _textEditService.SetDocument(document);
        inputController.SetDictionary(documentDictionary);
        inputController.ListenDocument();
    }
}