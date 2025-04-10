using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Utils;

public class StyledString
{
    private List<StyledSymbol> _styledString = new();
    public StyledString()
    {
        
    }
    
    public StyledString(string text, 
        bool isBold = false, 
        bool isItalic = false, 
        bool isUnderline = false, 
        ConsoleColor textColor = ConsoleColor.White, 
        ConsoleColor backgroundColor = ConsoleColor.Black)
    {
        foreach (char c in text)
        {
            _styledString.Add(new StyledSymbol
            {
                Symbol = c,
                IsBold = isBold,
                IsItalic = isItalic,
                IsUnderline = isUnderline,
                TextColor = textColor,
                BackgroundColor = backgroundColor
            });
        }
    }

    public StyledSymbol GetStyledSymbol(int position)
    {
        return _styledString[position];
    }
    
    public void UpdateSymbol(int position, StyledSymbol symbol)
    {
        _styledString[position] = symbol;
    }
    
    public int Length => _styledString.Count;

    public void AddSymbol(StyledSymbol symbol) => _styledString.Add(symbol);

    public void InsertSymbol(int position, StyledSymbol symbol)
    {
        _styledString.Insert(position, symbol);
    }
    
    public void RemoveSymbol(int position)
    {
        _styledString.RemoveAt(position);
    }
}