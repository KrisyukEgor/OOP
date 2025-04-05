using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Commands;

public class PrintLetterCommand : ICommand
{
    Document document;
    IDocumentViewer view;
    char letter;
    
    public PrintLetterCommand(Document document, IDocumentViewer view, char letter)
    {
        this.document = document;
        this.view = view;
        this.letter = letter;
    }
    
    public void Execute()
    {
        int lineIndex = view.FirstLineIndex;
        
    }

    public void UnExecute()
    {
        
    }
}