using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Commands.Document.Text;

public class PrintCharCommand : ICommand
{
    TextEditService _textEditService;
    private StyledSymbol? symbol;
    
    public PrintCharCommand(TextEditService textEditService, char symbol)
    {
        StyledSymbol styledSymbol = new StyledSymbol();
        styledSymbol.Symbol = symbol;
        
        this.symbol = styledSymbol;
        _textEditService = textEditService; 
    }
    
    public void Execute()
    {
        if (symbol != null)
        {
            _textEditService.InsertChar(symbol);
        }
    }

    public void UnExecute()
    {
        symbol = _textEditService.RemoveChar();
    }
}