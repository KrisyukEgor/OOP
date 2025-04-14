using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Cursor;

public class MoveCursorLeftCommand : ICommand
{
    private CursorController cursorController;
    public MoveCursorLeftCommand(CursorController cursorController)
    {
        this.cursorController = cursorController;
    }

    public void Execute()
    {
        cursorController.MoveCursorLeft();
    }

    public void UnExecute()
    {
        cursorController.MoveCursorRight();
    }
}