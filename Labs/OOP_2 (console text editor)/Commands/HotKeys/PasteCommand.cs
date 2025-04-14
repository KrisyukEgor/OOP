using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.HotKeys;

public class PasteCommand : ICommand
{
    TextEditService _textEditService;
    public PasteCommand(TextEditService documentController) 
    {
        _textEditService = documentController;
    }

    public void Execute()
    {
        _textEditService.Paste();
    }

    public void UnExecute()
    {
        
    }
}