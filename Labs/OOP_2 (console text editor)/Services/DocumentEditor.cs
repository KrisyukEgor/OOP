using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services;

public class DocumentEditor
{
    public DocumentEditor() {}
    public void InsertLine(Document document, int index, string line)
    {
        List<string> linesList = document.Lines;
        
        ValidateLineIndex(linesList, index, maxInclude: true);
        linesList.Insert(index, line);
        
    }

    public void RemoveLine(Document document, int index)
    {
        var linesList = document.Lines;
        
        ValidateLineIndex(linesList, index);
        linesList.RemoveAt(index);
        
    }

    public void UpdateLine(Document document, int index, string line)
    {
        var linesList = document.Lines;
        
        ValidateLineIndex(linesList, index);
        linesList[index] = line;
        
        
    }

    public void InsertChar(Document document, int y, int x, char symbol)
    {
        var linesList = document.Lines;
        ValidateLineIndex(linesList, y);
        ValidateColumnIndex(linesList, y, x, allowEnd: true);
        
        linesList[y] = linesList[y].Insert(x, symbol.ToString());

    }

    public char RemoveChar(Document document, int y, int x)
    {
        var linesList = document.Lines;
        ValidateLineIndex(linesList, y);
        ValidateColumnIndex(linesList, y, x);

        char removedSymbol = linesList[y][x];

        linesList[y] = linesList[y].Remove(x, 1);

        return removedSymbol;
    }

    
    
    private void ValidateLineIndex(List<string> linesList, int index, bool maxInclude = false)
    {
        int max = maxInclude ? linesList.Count : linesList.Count - 1;
        if (index < 0 || index > max)
        {
            throw new ArgumentOutOfRangeException(
                nameof(index),
                $"Line index {index} is out of range [0-{max}]"
            );
        }
    }
    
    private void ValidateColumnIndex(List<string> linesList, int lineIndex, int columnIndex, bool allowEnd = false)
    {
        int max = allowEnd ? linesList[lineIndex].Length : linesList[lineIndex].Length - 1;
        if (columnIndex < 0 || columnIndex > max)
        {
            throw new ArgumentOutOfRangeException(
                nameof(columnIndex),
                $"Column index {columnIndex} is out of range [0-{max}] for line {lineIndex}"
            );
        }
    }
    
}