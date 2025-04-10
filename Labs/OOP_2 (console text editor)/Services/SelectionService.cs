using OOP_2__console_text_editor_.Controllers;

namespace OOP_2__console_text_editor_.Services;

public class SelectionService
{
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

    public void Reset() => IsSelectionActive = false;
}
