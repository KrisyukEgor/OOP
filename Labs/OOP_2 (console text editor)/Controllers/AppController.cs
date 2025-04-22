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
    
    private IWindowViewer windowViewer;
    private WindowController _windowController;
    private InputController inputController = new();
    
    private IDictionary documentDictionary;
    
    private WindowSizeService _windowSizeService = new();
    
    
    public AppController ()
    {
        windowViewer = new WindowViewer();
        _windowController = new WindowController(windowViewer, _windowSizeService);

    }
    public void Start()
    {
        _windowController.RenderWindow();
    }
    
    // private void ModifyDocument(Document document)
    // {
    //     _textEditService.SetDocument(document);
    //     
    //     inputController.SetDictionary(documentDictionary);
    //     inputController.ListenDocument();
    // }
}