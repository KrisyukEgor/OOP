using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class UnderlineCommand : ICommand
{
    private DocumentController documentController;
    private StyledString selectedString;
    
    public UnderlineCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        selectedString= documentController.SetUnderlineText();
    }

    public void UnExecute()
    {
        documentController.UnsetUnderlineText(selectedString);
    }
}