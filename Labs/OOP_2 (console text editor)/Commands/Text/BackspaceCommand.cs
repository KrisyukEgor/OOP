using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;

namespace OOP_2__console_text_editor_.Commands.Text;

public class BackspaceCommand : ICommand
{
    private DocumentController documentController;
    private char removedSymbol;
    public BackspaceCommand(DocumentController documentController)
    {
        this.documentController = documentController;
    }
    public void Execute()
    {
        removedSymbol = documentController.RemoveChar();
    }

    public void UnExecute()
    {
        documentController.InsertChar(removedSymbol);
    }
}