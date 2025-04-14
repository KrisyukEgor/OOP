using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorDownCommand : ICommand
{
    private CursorController _cursorController;
    public MoveCursorDownCommand(CursorController cursorController)
    {
        this._cursorController = cursorController;
    }

    public void Execute()
    {
        _cursorController.MoveCursorDown();
    }

    public void UnExecute()
    {
        _cursorController.MoveCursorUp();
    }
}