using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorDownCommand : ICommand
{
    private DocumentController documentController;
    public MoveCursorDownCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }

    public void Execute()
    {
        documentController.MoveCursorDown();
    }

    public void UnExecute()
    {
        documentController.MoveCursorUp();
    }
}