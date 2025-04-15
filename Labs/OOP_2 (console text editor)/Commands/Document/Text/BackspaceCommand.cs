using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Text;

public class BackspaceCommand : ICommand
{
    private TextEditService textEditService;
    private StyledSymbol? removedSymbol;
    
    public BackspaceCommand(TextEditService textEditService)
    {
        this.textEditService = textEditService;
    }
    public void Execute()
    {
        removedSymbol = textEditService.RemoveChar();
    }

    public void UnExecute()
    {
        if (removedSymbol != null)
        {
            textEditService.InsertChar(removedSymbol);
        }
    }
}