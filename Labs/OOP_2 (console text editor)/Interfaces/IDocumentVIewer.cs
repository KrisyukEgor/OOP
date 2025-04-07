
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Interfaces;

public interface IDocumentViewer
{
    void Render(int firstLineIndex);
    void SetCursorPosition(int x, int y);
    Document Document { get; set; }
}