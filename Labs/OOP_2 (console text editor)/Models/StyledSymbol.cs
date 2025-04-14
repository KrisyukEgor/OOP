namespace OOP_2__console_text_editor_.Models;

public class StyledSymbol
{
    public StyledSymbol()
    {
    }

    public StyledSymbol(StyledSymbol symbol)
    {
        Symbol = symbol.Symbol;
        IsBold = symbol.IsBold;
        IsItalic = symbol.IsItalic;
        IsUnderline = symbol.IsUnderline;
        IsSelected = symbol.IsSelected;
        TextColor = symbol.TextColor;
        BackgroundColor = symbol.BackgroundColor;
        
    }
    public char Symbol { get; set; }
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }
    
    public bool IsSelected { get; set; }
    public ConsoleColor TextColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }
}