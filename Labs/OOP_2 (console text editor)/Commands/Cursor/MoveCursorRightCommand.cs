using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorRightCommand : ICommand
{
    private CursorController cursorController;
    public MoveCursorRightCommand(CursorController cursorController)
    {
        this.cursorController = cursorController;
    }

    public void Execute()
    {
        cursorController.MoveCursorRight();
    }

    public void UnExecute()
    {
        cursorController.MoveCursorLeft();
    }
}