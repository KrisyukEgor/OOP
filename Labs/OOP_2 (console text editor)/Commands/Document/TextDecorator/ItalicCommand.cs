using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.TextDecorator;

public class ItalicCommand : ICommand
{
    private TextEditService _textEditService;
    private StyledString selectedString;
    
    public ItalicCommand(TextEditService _textEditService)
    {
        this._textEditService = _textEditService;
    }
    public void Execute()
    {
        selectedString = _textEditService.SetItalicText();
    }

    public void UnExecute()
    {
        _textEditService.UnsetItalicText(selectedString);
    }
}