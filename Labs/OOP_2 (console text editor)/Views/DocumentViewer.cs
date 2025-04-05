using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Views;

public class DocumentView : IDocumentViewer
{
    Document? document;
    int firstLineIndex = 0;
    int currentLine;
    
    public DocumentView()
    {
    }


    public void Renter(Document document)
    {
        this.document = document;
    }

    public void Renter(Document document, int lineIndex)
    {
        this.document = document;
        this.firstLineIndex = lineIndex;
    }


    public int FirstLineIndex
    {
        get { return firstLineIndex; }
        set { firstLineIndex = value; }
    }
    
}