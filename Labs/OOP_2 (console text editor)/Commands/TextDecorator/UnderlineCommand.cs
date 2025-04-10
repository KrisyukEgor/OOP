using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class UnderlineCommand : ICommand
{
    private DocumentController documentController;
    
    public UnderlineCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        documentController.SetUnderlineText();
    }

    public void UnExecute()
    {
        
    }
}