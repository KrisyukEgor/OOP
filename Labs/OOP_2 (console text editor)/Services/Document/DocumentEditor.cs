using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Services.Document
{
    public class DocumentEditor
    {
        public DocumentEditor() { }

        public void InsertLine(Models.Document document, int index, StyledString line)
        {
            var lines = document.Lines; 
            ValidateLineIndex(lines, index, allowEnd: true);
            lines.Insert(index, line);
        }

        public void RemoveLine(Models.Document document, int index)
        {
            var lines = document.Lines;
            ValidateLineIndex(lines, index);
            lines.RemoveAt(index);
        }

        public void UpdateLine(Models.Document document, int index, StyledString line)
        {
            var lines = document.Lines;
            ValidateLineIndex(lines, index);
            lines[index] = line;
        }

        public void InsertSymbol(Models.Document document, int y, int x, StyledSymbol symbol)
        {
            var lines = document.Lines;
           
            ValidateLineIndex(lines, y);
            
            var styledLine = lines[y];
            ValidateColumnIndex(styledLine, x, allowEnd: true);
            
            styledLine.InsertSymbol(x, symbol);
        }

        public StyledSymbol RemoveSymbol(Models.Document document, int y, int x)
        {
            var lines = document.Lines;
            ValidateLineIndex(lines, y);
            var styledLine = lines[y];
            ValidateColumnIndex(styledLine, x);
            
            var removed = styledLine.GetStyledSymbol(x);
            styledLine.RemoveSymbol(x);
            return removed;
        }
        
        private void ValidateLineIndex(List<StyledString> lines, int index, bool allowEnd = false)
        {

            int max = allowEnd ? lines.Count : lines.Count - 1;
            
            if (index < 0 || index > max)
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"Line index {index} is out of range [0–{max}]"
                );
        }

        private void ValidateColumnIndex(StyledString line, int columnIndex, bool allowEnd = false)
        {
            int max = allowEnd ? line.Length : line.Length - 1;
            if (columnIndex < 0 || columnIndex > max)
                throw new ArgumentOutOfRangeException(
                    nameof(columnIndex),
                    $"Column index {columnIndex} is out of range [0–{max}]"
                );
        }
    }
}
