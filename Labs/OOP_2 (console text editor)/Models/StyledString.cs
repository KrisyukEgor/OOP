using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Models;

public class StyledString
{
    private List<StyledSymbol> _styledString = new();
    public StyledString()
    {
    }
    
    public StyledString(StyledString styledString)
    {
        for (int i = 0; i < styledString.Length; i++)
        {
            _styledString.Add(new StyledSymbol(styledString.GetStyledSymbol(i)));
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

    public string GetString()
    {
        string result  = "";

        foreach (var symbol in _styledString)
        {
            result += symbol.Symbol;
        }

        return result;
    }
    
    public StyledString Substring(int startIndex, int length)
    {
        if (startIndex < 0 || startIndex > _styledString.Count)
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (length < 0 || startIndex + length > _styledString.Count)
            throw new ArgumentOutOfRangeException(nameof(length));

        var result = new StyledString();
        
        for (int i = 0; i < length; i++)
        {
            var sym = _styledString[startIndex + i];
            result += sym;
        }
        return result;
    }

    public StyledString Substring(int startIndex)
    {
        return Substring(startIndex, _styledString.Count - startIndex);
    }

    public static StyledString operator +(StyledString left, StyledString right)
    {
        var result = new StyledString();

        for (int i = 0; i < left.Length; i++)
        {
            result += left.GetStyledSymbol(i);
        }

        for (int i = 0; i < right.Length; i++)
        {
            result += right.GetStyledSymbol(i);
        }

        return result;
    }
    
    public static StyledString operator +(StyledString styledStr,StyledSymbol sym)
    {
        var result = new StyledString();
        
        
        for (int i = 0; i < styledStr.Length; i++)
        {
            result.AddSymbol(styledStr.GetStyledSymbol(i));
        }
        
        result.AddSymbol(sym);

        return result;
    }

    public bool Contains(StyledSymbol symbol)
    {
        return _styledString.Contains(symbol);
    }
    
}