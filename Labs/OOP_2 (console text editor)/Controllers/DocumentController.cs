using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;

namespace OOP_2__console_text_editor_.Controllers;

public class DocumentController
{ 
    private Document? document = null;
    private readonly IDocumentViewer _documentViewer;
    private readonly CursorController _cursorController;
    private readonly DocumentEditor _documentEditor;
    private int _scrollOffset = 0;

    public DocumentController(IDocumentViewer documentViewer, CursorController cursorController)
    {
       
        _documentViewer = documentViewer;
        _cursorController = cursorController;
        _documentEditor = new DocumentEditor();
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        _cursorController.SetDocument(document);
        _documentViewer.Document = document;
        
        _scrollOffset = 0;
        UpdateView();

    }

    public (int, int) GetCursorPosition()
    {
        return _cursorController.GetPosition();
    }

    public void MoveCursorLeft()
    {
        _cursorController.MoveLeft();
        _documentViewer.SetCursorPosition(_cursorController.GetX(), _cursorController.GetY());
    }

    public void MoveCursorRight()
    {
        _cursorController.MoveRight();
        _documentViewer.SetCursorPosition(_cursorController.GetX(), _cursorController.GetY());
    }

    public void MoveCursorUp()
    {
        _cursorController.MoveUp();
        _documentViewer.SetCursorPosition(_cursorController.GetX(), _cursorController.GetY());
    }

    public void MoveCursorDown()
    {
        _cursorController.MoveDown();
        _documentViewer.SetCursorPosition(_cursorController.GetX(), _cursorController.GetY());
    }
    
    public void InsertLine(string line)
    {
        if (document == null) return;
        
        int cursorY = _cursorController.GetY();
        
        _documentEditor.InsertLine(document, cursorY, line);
        _cursorController.MoveDown();
        
        UpdateView();
    }

    public void RemoveLine()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        int cursorY = _cursorController.GetY();
        
        _documentEditor.RemoveLine(document, cursorY);
        
        if (cursorY >= document.Lines.Count)
        {
            _cursorController.MoveUp();
        }
        
        UpdateView();
    }
    
    public void InsertChar(char symbol)
    {
        if (document == null) return;
        
        var (cursorX, cursorY) = _cursorController.GetPosition();
        
        _documentEditor.InsertChar(document, cursorY, cursorX, symbol);
        _cursorController.MoveRight();
        
        UpdateView();
    }

    public char RemoveChar()
    {
        if (document == null) 
            return '\0';

        char? removedSymbol = null;
        var (cursorX, cursorY) = _cursorController.GetPosition();

        if (cursorX > 0)
        {
            removedSymbol = _documentEditor.RemoveChar(document, cursorY, cursorX - 1);
            _cursorController.MoveLeft();
        }
        else if (cursorY > 0)
        {
            string currentLineText = document.Lines[cursorY];

            _documentEditor.RemoveLine(document, cursorY);

            _cursorController.MoveUp();
            _cursorController.MoveToEndLine();

            _documentEditor.UpdateLine( document, _cursorController.GetY(), document.Lines[_cursorController.GetY()] + currentLineText);

            removedSymbol = '\n';
        }

        UpdateView();

        return removedSymbol ?? '\0';
    }


    private void UpdateView()
    {
        if (document == null) return;
        
        var (x, y) = _cursorController.GetPosition();
         
        _documentViewer.SetCursorPosition(x, y - _scrollOffset);
        
        _documentViewer.Render(_scrollOffset);
        
    }
    
}