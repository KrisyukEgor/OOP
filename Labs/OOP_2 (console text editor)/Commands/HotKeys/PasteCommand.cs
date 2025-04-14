using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.HotKeys;

public class PasteCommand : ICommand
{
    DocumentController _documentController;
    public PasteCommand(DocumentController documentController) 
    {
        _documentController = documentController;
    }

    public void Execute()
    {
        _documentController.Paste();
    }

    public void UnExecute()
    {
        
    }
}