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
    private readonly TextDecoratorService _textDecoratorService;
    private readonly SelectionService selectionService;
    
    private int _scrollOffset = 0;

    public DocumentController(IDocumentViewer documentViewer, CursorController cursorController)
    {
       
        _documentViewer = documentViewer;
        _cursorController = cursorController;
        _documentEditor = new DocumentEditor();
        _textDecoratorService = new TextDecoratorService();
        selectionService = new SelectionService();
    }

    public void SetDocument(Document document)
    {
        this.document = document;
        _cursorController.SetDocument(document);
        _scrollOffset = 0;

        UpdateView();
    }
    
    public void SetCursorPosition(int x, int y)
    {
        _cursorController.SetPosition(x, y);
    }
    public (int, int) GetCursorPosition()
    {
        return _cursorController.GetPosition();
    }

    
    public void InsertLine(StyledString line)
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
    
    public void InsertChar(StyledSymbol symbol)
    {
        if (document == null) return;
        
        var (cursorX, cursorY) = _cursorController.GetPosition();
        
        _documentEditor.InsertSymbol(document, cursorY, cursorX, symbol);
        MoveCursorRight();
        
        UpdateView();
    }

    public StyledSymbol? RemoveChar()
    {
        if (document == null) 
            return null;

        StyledSymbol? removedSymbol = null;
        var (cursorX, cursorY) = _cursorController.GetPosition();

        if (cursorX > 0)
        {
            removedSymbol = _documentEditor.RemoveSymbol(document, cursorY, cursorX - 1);
            _cursorController.MoveLeft();
        }
        else if (cursorY > 0)
        {
            var currentLineText = document.Lines[cursorY];

            _documentEditor.RemoveLine(document, cursorY);

            MoveCursorUp();
            _cursorController.MoveToLineEnd();

            _documentEditor.UpdateLine( document, _cursorController.GetY(), document.Lines[_cursorController.GetY()] + currentLineText);

        }

        UpdateView();

        return removedSymbol;
    }

    public void BreakLine()
    {
        if (document == null) return;
        
        var (cursorX, cursorY) = _cursorController.GetPosition();
        
        var currentLineText = document.Lines[cursorY];
        
        var firstPart = currentLineText.Substring(0, cursorX);
        var secondPart = currentLineText.Substring(cursorX );
        
        _documentEditor.UpdateLine(document, cursorY, firstPart);
        _documentEditor.InsertLine(document, cursorY + 1, secondPart);
        
        _cursorController.MoveToLineStart();
        MoveCursorDown();
        
        UpdateView();
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

    public void MoveCursorToEndLine()
    {
        _cursorController.MoveToLineEnd();
        _documentViewer.SetCursorPosition(_cursorController.GetX(), _cursorController.GetY());
    }

    private void UpdateView()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        var (x, y) = _cursorController.GetPosition();
         
        _documentViewer.SetCursorPosition(x, y - _scrollOffset);
        
        _documentViewer.Render(document, _scrollOffset);
        
    }

    public void SelectLeft()
    {
        if (document == null) return;

        if (!selectionService.IsSelectionActive)
        {
            var (startX, startY) = _cursorController.GetPosition();
            selectionService.StartSelection(startX, startY);
        }
        
        _cursorController.MoveLeft();
        
        var (endX, endY) = _cursorController.GetPosition();
        selectionService.UpdateSelection(endX, endY);
        
        
        StyledString selectedString = GetSelectedText();
        
        for (int i = 0; i < selectedString.Length; i++)
        {
            selectedString.GetStyledSymbol(i).IsSelected = true;
        }
        
        UpdateView();
    }

    public void SelectRight()
    {
        if (document == null) return;
        
        if (!selectionService.IsSelectionActive)
        {
            var (startX, startY) = _cursorController.GetPosition();
            selectionService.StartSelection(startX, startY);
        }
        
        _cursorController.MoveRight();
        
        var (endX, endY) = _cursorController.GetPosition();
        selectionService.UpdateSelection(endX, endY);
        
        StyledString selectedString = GetSelectedText();
        
        for (int i = 0; i < selectedString.Length; i++)
        {
            selectedString.GetStyledSymbol(i).IsSelected = true;
        }
        UpdateView();
    }
    
    
    private StyledString GetSelectedText()
    {
        if(document == null) return new StyledString();
        
        if (!selectionService.IsSelectionActive)
            return new StyledString();
        
        var (startX, startY) = selectionService.GetSelectionStart();
        var (endX, endY) = selectionService.GetSelectionEnd();
        
        if (startY > endY || (startY == endY && startX > endX))
        {
            (startX, endX) = (endX, startX);
            (startY, endY) = (endY, startY);
        }
        
        var result = new StyledString();

        if (startY == endY)
        {
            var line = document.GetLine(startY);
            int length = endX - startX ;
            var part = line.Substring(startX, length);

            
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
            return result;
        }
        
        {
            var first = document.GetLine(startY);
            var part = first.Substring(startX);   
            
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
            
        }
        
        for (int y = startY + 1; y < endY; y++)
        {
            var mid = document.GetLine(y);

            for (int i = 0; i < mid.Length; i++)
                result.AddSymbol(mid.GetStyledSymbol(i));
            
        }
        
        {
            var last = document.GetLine(endY);
            int length = endX + 1;                  
            var part = last.Substring(0, length);
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
        }
        
        return result;
    }
    
    public void SetBoldText()
    {
        var selectedString = GetSelectedText();
        
        // Console.WriteLine(selectedString.ToString());
        for (int i = 0; i < selectedString.Length; ++i)
        {
            selectedString.GetStyledSymbol(i).IsBold = true;
            selectedString.GetStyledSymbol(i).IsSelected = false;
            selectionService.Reset();
        }
        
        UpdateView();

    }

    public void SetItalicText()
    {
        
    }

    public void SetUnderlineText()
    {
        
    }

}