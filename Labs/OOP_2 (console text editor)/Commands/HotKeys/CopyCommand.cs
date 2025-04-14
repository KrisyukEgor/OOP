using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Commands.HotKeys;
public class CopyCommand : ICommand
{
    TextEditService _textEditService;
    public CopyCommand(TextEditService textEditService) 
    {
        _textEditService = textEditService;
    }

    public void Execute()
    {
        _textEditService.Copy();
    }

    public void UnExecute()
    {
        
    }
}