using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorUpCommand : ICommand
{
    private DocumentController documentController;
    public MoveCursorUpCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }

    public void Execute()
    {
        documentController.MoveCursorUp();
    }

    public void UnExecute()
    {
        documentController.MoveCursorDown();
    }
}