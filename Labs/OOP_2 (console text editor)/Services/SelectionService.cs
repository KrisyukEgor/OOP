using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class SelectionService
{
    private StyledString selectedString;
    public bool IsSelectionActive { get; private set; }
    public (int X, int Y) Start { get; private set; }
    public (int X, int Y) End { get; private set; }

    public Document Document { get; set; }

    public void StartSelection(int startX, int startY)
    {
        Start = (startX, startY);
        End = (startX, startY);
        IsSelectionActive = true;
        selectedString = new StyledString();
    }

    public void UpdateSelection(int endX, int endY)
    {
        if (!IsSelectionActive) return;
        End = (endX, endY);

        var newSelectedString = GetSelectedText();

        for (int i = 0; i < selectedString.Length; i++)
        {
            var symbol = selectedString.GetStyledSymbol(i);
            
            if (!newSelectedString.Contains(symbol))
                symbol.IsSelected = false;
        }

        for (int i = 0; i < newSelectedString.Length; i++)
        {
            newSelectedString.GetStyledSymbol(i).IsSelected = true;
        }

        selectedString = newSelectedString;
    }
    public (int, int) GetSelectionStart()
    {
        return (Start.X, Start.Y);
    }
    
    public (int, int) GetSelectionEnd()
    {
        return (End.X, End.Y);
    }

    public StyledString GetSelectedString()
    {
        return selectedString;
        
    }
    private StyledString GetSelectedText()
    {

        if (Document == null)
        {
            throw new NullReferenceException("Selection service: Document is null");
        }
        if (!IsSelectionActive)
            return new StyledString();
        
        var (startX, startY) = GetSelectionStart();
        var (endX, endY) = GetSelectionEnd();
        
        if (startY > endY || (startY == endY && startX > endX))
        {
            (startX, endX) = (endX, startX);
            (startY, endY) = (endY, startY);
        }
        
        var result = new StyledString();

        if (startY == endY)
        {
            var line = Document.GetLine(startY);
            int length = endX - startX ;
            var part = line.Substring(startX, length);

            
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
            return result;
        }
        
        {
            var first = Document.GetLine(startY);
            var part = first.Substring(startX);   
            
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
            
        }
        
        for (int y = startY + 1; y < endY; y++)
        {
            var mid = Document.GetLine(y);

            for (int i = 0; i < mid.Length; i++)
                result.AddSymbol(mid.GetStyledSymbol(i));
            
        }
        
        {
            var last = Document.GetLine(endY);
            int length = endX ;   
            
            var part = last.Substring(0, length);
            
            for (int i = 0; i < part.Length; i++)
                result.AddSymbol(part.GetStyledSymbol(i));
        }
        
        return result;
    }

    public void Reset()
    {
        IsSelectionActive = false;

        for (int i = 0; i < selectedString.Length; ++i)
        {
            selectedString.GetStyledSymbol(i).IsSelected = false;
        }
        
    }
}
