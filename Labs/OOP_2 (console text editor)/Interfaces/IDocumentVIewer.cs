
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Interfaces;

public interface IDocumentViewer
{
    void Renter(Document document);
    void Renter(Document document, int lineIndex);
    
    int FirstLineIndex { get; set; }
}