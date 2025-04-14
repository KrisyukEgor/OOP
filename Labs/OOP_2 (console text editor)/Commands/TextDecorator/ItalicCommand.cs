using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class ItalicCommand : ICommand
{
    private DocumentController documentController;
    private StyledString selectedString;
    
    public ItalicCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        selectedString = documentController.SetItalicText();
    }

    public void UnExecute()
    {
        documentController.UnsetItalicText(selectedString);
    }
}