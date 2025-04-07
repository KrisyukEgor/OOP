using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands.Text;

public class PrintCharCommand : ICommand
{
    Document document;
    DocumentController controller;
    private char symbol;
    
    public PrintCharCommand(DocumentController controller, char symbol)
    {
        this.symbol = symbol;
        this.controller = controller; 
    }
    
    public void Execute()
    {
        controller.InsertChar(symbol);
    }

    public void UnExecute()
    {
        controller.RemoveChar();
    }
}