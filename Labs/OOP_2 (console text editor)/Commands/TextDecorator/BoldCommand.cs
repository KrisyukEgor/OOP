using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class BoldCommand : ICommand
{
    private DocumentController documentController;
    private StyledString selectedString;
    
    public BoldCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        selectedString = documentController.SetBoldText();
    }

    public void UnExecute()
    {
        documentController.UnsetBoldText(selectedString);
    }
}