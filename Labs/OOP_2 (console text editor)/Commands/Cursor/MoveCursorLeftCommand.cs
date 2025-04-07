using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorLeftCommand : ICommand
{
    private DocumentController documentController;
    public MoveCursorLeftCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }

    public void Execute()
    {
        documentController.MoveCursorLeft();
    }

    public void UnExecute()
    {
        documentController.MoveCursorRight();
    }
}