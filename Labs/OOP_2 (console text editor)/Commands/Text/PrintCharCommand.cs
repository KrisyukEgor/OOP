using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands.Text;

public class PrintCharCommand : ICommand
{
    DocumentController controller;
    private StyledSymbol? symbol;
    
    public PrintCharCommand(DocumentController controller, char symbol)
    {
        StyledSymbol styledSymbol = new StyledSymbol();
        styledSymbol.Symbol = symbol;
        
        this.symbol = styledSymbol;
        this.controller = controller; 
    }
    
    public void Execute()
    {
        if (symbol != null)
        {
            controller.InsertChar(symbol);
        }
    }

    public void UnExecute()
    {
        symbol = controller.RemoveChar();
    }
}