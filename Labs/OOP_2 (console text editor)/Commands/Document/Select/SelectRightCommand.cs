using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Select;

public class SelectRightCommand : ICommand
{
    TextEditService _textEditService;
    public SelectRightCommand(TextEditService documentController) 
    {
        _textEditService = documentController;
    }

    public void Execute()
    {
        _textEditService.SelectRight();
    }

    public void UnExecute()
    {
        _textEditService.SelectLeft();
    }
}