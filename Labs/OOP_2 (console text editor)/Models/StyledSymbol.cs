namespace OOP_2__console_text_editor_.Models;

public class StyledSymbol
{
    public char Symbol { get; set; }
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public bool IsUnderline { get; set; }
    public ConsoleColor TextColor { get; set; }
    public ConsoleColor BackgroundColor { get; set; }
}