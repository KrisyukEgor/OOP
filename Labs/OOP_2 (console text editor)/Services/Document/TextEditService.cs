using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services.Document;

public class TextEditService
{
    private readonly CursorController _cursorController;
    
    private readonly DocumentEditor _documentEditor = new();
    private readonly TextDecoratorService _textDecoratorService = new();
    private readonly SelectionService selectionService = new();

    private readonly DocumentController _documentController;

    private readonly IBuffer buffer;
    
    private Models.Document? document = null;
    
    public TextEditService(CursorController cursorController, IBuffer buffer, DocumentController documentController)
    {
       
        _cursorController = cursorController;
        _documentController = documentController;

        this.buffer = buffer;

    }
    public void SetDocument(Models.Document document)
    {
        this.document = document;
        _documentController.SetDocument(document);
        _cursorController.SetDocument(document);
        selectionService.Document = document;

        UpdateView();
    }
    
    public void InsertLine(StyledString line)
    {
        if (document == null) return;
        
        int cursorY = _cursorController.GetY();
        
        _documentEditor.InsertLine(document, cursorY, line);
        MoveCursorDown();
        
        UpdateView();
    }

    public void RemoveLine()
    {
        if (document == null || document.Lines.Count == 0) return;
        
        int cursorY = _cursorController.GetY();
        
        _documentEditor.RemoveLine(document, cursorY);
        
        if (cursorY >= document.Lines.Count)
        {
            MoveCursorUp();
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
            MoveCursorLeft();
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
        
        StyledSymbol styledSymbol = new StyledSymbol();
        styledSymbol.Symbol = '\0';
        
        firstPart.AddSymbol(styledSymbol);
        
        var secondPart = currentLineText.Substring(cursorX );
        
        _documentEditor.UpdateLine(document, cursorY, firstPart);
        _documentEditor.InsertLine(document, cursorY + 1, secondPart);
        
        _cursorController.MoveToLineStart();
        MoveCursorDown();
        
        UpdateView();
    }
    
    public void SelectLeft()
    {
        SelectAndMoveCursor(_cursorController.MoveCursorLeft);
    }

    public void SelectRight()
    {
        SelectAndMoveCursor(_cursorController.MoveCursorRight);
    }

    private void SelectAndMoveCursor(Action cursorMove)
    {
        if (document == null) return;
        
        if (!selectionService.IsSelectionActive)
        {
            var (startX, startY) = _cursorController.GetPosition();
            selectionService.StartSelection(startX, startY);
        }

        cursorMove();
        
        var (endX, endY) = _cursorController.GetPosition();
        selectionService.UpdateSelection(endX, endY);
        
        UpdateView();
    }
    
    
    public StyledString SetBoldText()
    {
        var selectedString = selectionService.GetSelectedString();
        return SetTextAction(selectedString, sym => sym.IsBold = true);
    }
    
    public StyledString SetItalicText()
    {
        var selectedString = selectionService.GetSelectedString();
        return SetTextAction(selectedString, sym => sym.IsItalic = true);
    }

    public StyledString SetUnderlineText()
    {
        var selectedString = selectionService.GetSelectedString();
        return SetTextAction(selectedString, sym => sym.IsUnderline = true);
    }
    
    public void UnsetBoldText(StyledString selectedString)
    {
        SetTextAction(selectedString, sym => sym.IsBold = false);
    }
    
    public void UnsetItalicText(StyledString selectedString)
    {
        SetTextAction(selectedString, sym => sym.IsItalic = false);
    }
    
    public void UnsetUnderlineText(StyledString selectedString)
    {
        SetTextAction(selectedString, sym => sym.IsUnderline = false);
    }
    
    private StyledString SetTextAction(StyledString selectedString, Action<StyledSymbol> styleAction)
    {

        for (int i = 0; i < selectedString.Length; i++)
        {
            styleAction(selectedString.GetStyledSymbol(i));
        }
        
        UpdateView();
        return selectedString;
    }


    public void Copy()
    {
        StyledString selection = selectionService.GetSelectedString();

        ClearSelection();
        
        buffer.SetBuffer(selection);

    }

    public void Paste()
    {
        StyledString bufferString = buffer.GetBuffer();
    
        List<StyledString> lines = new List<StyledString>();
    
        StyledString currentLine = new StyledString();
    
        for (int i = 0; i < bufferString.Length; i++)
        {
            var symbol = bufferString.GetStyledSymbol(i);
        
            if (symbol.Symbol == '\0')
            {
                lines.Add(currentLine);
                currentLine = new StyledString();
            }
            else
            {
                currentLine.AddSymbol(symbol);
            }
        }
    
        lines.Add(currentLine);
        
        AddLines(lines);

        ClearSelection();
        
        UpdateView();
    }

    private void ClearSelection()
    {
        if (selectionService.IsSelectionActive)
        {
            selectionService.Reset();
            UpdateView();

        }
    }


    private void AddLines(List<StyledString> lines)
    {
        if (document == null  || lines.Count == 0)
            return;

        var (cursorX, cursorY) = _cursorController.GetPosition();
        
        var currentLine = document.GetLine(cursorY);
        
        _documentEditor.UpdateLine(document, cursorY, currentLine + lines[0]);
        
        for (int i = 1; i < lines.Count; i++)
        {
            _documentEditor.InsertLine(document, cursorY + i, lines[i]);
        }
        
        _cursorController.MoveToLineEnd();

        for (int i = 1; i < lines.Count; i++)
        {
            MoveCursorDown();
        }
    }

    private void UpdateView()
    {
        _documentController.UpdateView();
        _cursorController.UpdatePosition();
    }

    public void MoveCursorLeft()
    {
        _cursorController.MoveCursorLeft();
        UpdateCursorPosition();
    }
    
    public void MoveCursorRight()
    {
        _cursorController.MoveCursorRight();
        UpdateCursorPosition();
    }

    public void MoveCursorUp()
    {
        _cursorController.MoveCursorUp();
        UpdateCursorPosition();
    }

    public void MoveCursorDown()
    {
        _cursorController.MoveCursorDown();
        UpdateCursorPosition();
    }

    private void UpdateCursorPosition()
    {
        ClearSelection();
        _cursorController.UpdatePosition();
    }
}