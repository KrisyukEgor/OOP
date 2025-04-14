using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Select;

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