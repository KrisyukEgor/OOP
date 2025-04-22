
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Interfaces;

public interface IDocumentViewer
{
    void Render(Document document);
    void SetCursorPosition(int cursorX, int cursorY);
}