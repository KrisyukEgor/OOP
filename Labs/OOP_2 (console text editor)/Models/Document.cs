namespace OOP_2__console_text_editor_.Models;

public class Document
{
    
    private List<string> _linesList;

    public Document()
    {
        _linesList = new List<string> { "" };
    }

    public List<string> Lines
    {
        get { return _linesList; }
    }


    public string GetLine(int index)
    {
        ValidateLineIndex(index);
        return _linesList[index];
    }

    public void InsertLine(int index, string line = "")
    {
        ValidateLineIndex(index, maxInclude: true);
        _linesList.Insert(index, line);
    }
    
    public void RemoveLine(int index)
    {
        ValidateLineIndex(index);
        _linesList.RemoveAt(index);
    }

    public void UpdateLine(int index, string text)
    {
        ValidateLineIndex(index);
        _linesList[index] = text;
    }

    public void InsertChar(int lineIndex, int columnIndex, char symbol)
    {
        ValidateLineIndex(lineIndex);
        ValidateColumnIndex(lineIndex, columnIndex, allowEnd: true);

        _linesList[lineIndex] = _linesList[lineIndex].Insert(columnIndex, symbol.ToString());
    }
    
    public void RemoveChar(int lineIndex, int columnIndex)
    {
        ValidateLineIndex(lineIndex);
        ValidateColumnIndex(lineIndex, columnIndex);

        _linesList[lineIndex] = _linesList[lineIndex].Remove(columnIndex, 1);
    }

    private void ValidateLineIndex(int index, bool maxInclude = false)
    {
        int max = maxInclude ? _linesList.Count : _linesList.Count - 1;
        if (index < 0 || index > max)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index),
                $"Line index {index} is out of range [0-{max}]"
            );
        }
    }

    private void ValidateColumnIndex(int lineIndex, int columnIndex, bool allowEnd = false)
    {
        int max = allowEnd ? _linesList[lineIndex].Length : _linesList[lineIndex].Length - 1;
        if (columnIndex < 0 || columnIndex > max)
        {
            throw new ArgumentOutOfRangeException(
                nameof(columnIndex),
                $"Column index {columnIndex} is out of range [0-{max}] for line {lineIndex}"
            );
        }
    }
}
