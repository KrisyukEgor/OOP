using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class BoldCommand : ICommand
{
    private DocumentController documentController;
    
    public BoldCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        documentController.SetBoldText();
    }

    public void UnExecute()
    {
        
    }
}