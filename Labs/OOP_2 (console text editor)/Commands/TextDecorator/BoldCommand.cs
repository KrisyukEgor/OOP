using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.TextDecorator;

public class BoldCommand : ICommand
{
    private TextEditService _textEditService;
    private StyledString selectedString;
    
    public BoldCommand(TextEditService textEditService)
    {
        this._textEditService = textEditService;
    }
    public void Execute()
    {
        selectedString = _textEditService.SetBoldText();
    }

    public void UnExecute()
    {
        _textEditService.UnsetBoldText(selectedString);
    }
}