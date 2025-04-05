using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class AppController
{
    private DocumentController documentController;
    private InputController inputController;

    public AppController ()
    {
        documentController = new DocumentController();
        inputController = new InputController();
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
        Document newDocument = documentController.CreateDocument();
        ModifyDocument(newDocument);
    }


    private void OldDocument()
    {
        Document oldDocument = documentController.OpenDocument();
        ModifyDocument(oldDocument);
        
    }

    private void ModifyDocument(Document document)
    {
        inputController.Start();
    }
}