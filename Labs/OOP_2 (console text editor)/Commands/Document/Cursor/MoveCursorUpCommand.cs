using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Cursor;

public class MoveCursorUpCommand : ICommand
{
    private TextEditService _textEditService;
    public MoveCursorUpCommand(TextEditService cursorController)
    {
        this._textEditService = _textEditService;
    }

    public void Execute()
    {
        _textEditService.MoveCursorUp();
    }

    public void UnExecute()
    {
        _textEditService.MoveCursorDown();
    }
}