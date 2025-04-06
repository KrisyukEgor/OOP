using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Controllers;

public class DocumentController
{ 
    private Document? document = null;
    private readonly IDocumentViewer _documentViewer;
    private readonly CursorController _cursorController;
    private int _scrollOffset = 0;

    public DocumentController(IDocumentViewer documentViewer, CursorController cursorController)
    {
       
        _documentViewer = documentViewer;
        _cursorController = cursorController;
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        _cursorController.SetDocument(document);
        _documentViewer.Document = document;
        
        _scrollOffset = 0;
        UpdateView();

    }
    
    public void InsertLine(string line)
    {
        if (document == null) return;
        
        int cursorY = _cursorController.GetY();
        
        document.InsertLine(cursorY, line);
        _cursorController.MoveDown();
        
        UpdateView();
    }

    public void RemoveLine()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        int cursorY = _cursorController.GetY();
        
        document.RemoveLine(cursorY);
        
        if (cursorY >= document.Lines.Count)
        {
            _cursorController.MoveUp();
        }
        
        UpdateView();
    }
    
    public void InsertChar(char symbol)
    {
        if (document == null) return;
        
        var (currentColumn, currentLine) = _cursorController.GetPosition();
        
        document.InsertChar(currentLine, currentColumn, symbol);
        _cursorController.MoveRight();
        
        UpdateView();
    }

    public void RemoveChar()
    {
        if (document == null) return;
        
        var (cursorX, cursorY) = _cursorController.GetPosition();
        
        if (cursorX > 0)
        {
            document.RemoveChar(cursorY, cursorX - 1);
            _cursorController.MoveLeft();
        }
        else if (cursorY > 0)
        {
            string currentLineText = document.Lines[cursorY];
            
            document.RemoveLine(cursorY);
            
            _cursorController.MoveUp();
            
            document.UpdateLine(_cursorController.GetY(), document.Lines[_cursorController.GetY()] + currentLineText);
        }
        UpdateView();
    }

    private void UpdateView()
    {
        if (document == null) return;
        _documentViewer.Render(_scrollOffset);
        
        var (x, y) = _cursorController.GetPosition();
        
       // _documentViewer.SetCursorPosition(x, y - _scrollOffset);
    }
    
}