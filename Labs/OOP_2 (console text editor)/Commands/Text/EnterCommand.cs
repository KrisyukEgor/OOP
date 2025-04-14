using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Text;

public class EnterCommand : ICommand
{
    private TextEditService _textEditService;
    public EnterCommand(TextEditService textEditService)
    {
        _textEditService = textEditService;
    }
    public void Execute()
    {
        _textEditService.BreakLine();
    }

    public void UnExecute()
    {
        _textEditService.RemoveChar();
    }
}