using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class ItalicCommand : ICommand
{
    private DocumentController documentController;
    
    public ItalicCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        documentController.SetItalicText();
    }

    public void UnExecute()
    {
        
    }
}