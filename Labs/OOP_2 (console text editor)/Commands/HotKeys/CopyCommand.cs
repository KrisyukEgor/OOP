using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.HotKeys;
public class CopyCommand : ICommand
{
    DocumentController _documentController;
    public CopyCommand(DocumentController documentController) 
    {
        _documentController = documentController;
    }

    public void Execute()
    {
        _documentController.Copy();
    }

    public void UnExecute()
    {
        
    }
}