using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class SelectionService
{
    private StyledString selectedString = new();
    public bool IsSelectionActive { get; private set; }
    public (int X, int Y) Start { get; private set; }
    public (int X, int Y) End { get; private set; }

    public void StartSelection(int startX, int startY)
    {
        Start = (startX, startY);
        End = (startX, startY);
        IsSelectionActive = true;
    }

    public void UpdateSelection(int endX, int endY)
    {
        if (!IsSelectionActive) return;
        End = (endX, endY);
    }

    public List<(int, int)> GetSelection()
    {
        return new List<(int, int)> { (Start.X, Start.Y), (End.X, End.Y) };
    
    }

    public (int, int) GetSelectionStart()
    {
        return (Start.X, Start.Y);
    }
    
    public (int, int) GetSelectionEnd()
    {
        return (End.X, End.Y);
    }

    // public void Select(Document document)
    // {
    //     
    // }
    //
    // public void StartSelection(int x, int y)
    // {
    //     Start = (x, y);
    // }
    
    
    public StyledString GetSelectedString()
    {
        return selectedString;
    }

    public void Reset() => IsSelectionActive = false;
}
