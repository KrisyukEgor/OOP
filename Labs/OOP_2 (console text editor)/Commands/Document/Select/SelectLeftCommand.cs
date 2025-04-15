using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Select;

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