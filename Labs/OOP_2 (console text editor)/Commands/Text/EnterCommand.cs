using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Text;

public class EnterCommand : ICommand
{
    private DocumentController _documentController;
    public EnterCommand(DocumentController documentController)
    {
        _documentController = documentController;
    }
    public void Execute()
    {
        _documentController.BreakLine();
    }

    public void UnExecute()
    {
        _documentController.RemoveChar();
    }
}