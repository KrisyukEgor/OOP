using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.HotKeys;
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