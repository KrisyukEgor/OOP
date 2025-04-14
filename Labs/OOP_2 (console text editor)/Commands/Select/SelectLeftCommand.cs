using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.Select;

public class SelectLeftCommand : ICommand
{
    TextEditService _textEditService;
    public SelectLeftCommand(TextEditService documentController) 
    {
        _textEditService = documentController;
    }

    public void Execute()
    {
        _textEditService.SelectLeft();
    }

    public void UnExecute()
    {
        _textEditService.SelectRight();
    }
}