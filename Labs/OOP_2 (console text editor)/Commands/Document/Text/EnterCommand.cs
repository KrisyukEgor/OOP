using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Text;

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