using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorRightCommand : ICommand
{
    private DocumentController documentController;
    public MoveCursorRightCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }

    public void Execute()
    {
        documentController.MoveCursorRight();
    }

    public void UnExecute()
    {
        documentController.MoveCursorLeft();
    }
}