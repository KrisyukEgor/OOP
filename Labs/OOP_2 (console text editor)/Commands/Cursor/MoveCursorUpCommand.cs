using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorUpCommand : ICommand
{
    private CursorController _cursorController;
    public MoveCursorUpCommand(CursorController cursorController)
    {
        this._cursorController = cursorController;
    }

    public void Execute()
    {
        _cursorController.MoveCursorUp();
    }

    public void UnExecute()
    {
        _cursorController.MoveCursorDown();
    }
}