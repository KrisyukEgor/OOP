using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Services.Document;
using OOP_2__console_text_editor_.Services.Window;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Views
{
    public class ConsoleDocumentView : IDocumentViewer
    {
        private int _firstLineIndex;
        
        private (int, int) startPosition = (0, 0);
        private int sectionWidth = 0;
        private int sectionHeight = 0;
        private (int, int) cursorPosition = (0, 0);
        
        int borderSize = 1;
        private int _scrollOffset = 0; 
        private int _absoluteCursorY = 0; 
        
        private WindowSizeService _windowSizeService;
        private TextDecoratorService textDecoratorService;

        public ConsoleDocumentView(WindowSizeService windowSizeService)
        {
            _windowSizeService = windowSizeService;
            textDecoratorService = new TextDecoratorService();
            
        }
        
        public void Render(Document document)
        {
            _firstLineIndex = _windowSizeService.GetMainStartLine();
            ClearView();
            
            CalculateSectionSize();
            
            List<StyledString> stringsToRender = GetListToRender(document);

            foreach (var line in stringsToRender)
            {
                PrintLine(line);
            }
        }
        
        public void SetCursorPosition(int cursorX, int cursorY)
        {
            cursorPosition = AdaptCursorPosition(cursorX, cursorY);
            
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
        }

        private (int, int) AdaptCursorPosition(int cursorX, int cursorY)
        {
            
            int safeCursorX = Math.Max(0, cursorX);
            int safeCursorY = Math.Max(0, cursorY);

            int newCursorX = startPosition.Item1 + safeCursorX;
            int newCursorY = startPosition.Item2 + safeCursorY;

            int overflowLines = sectionWidth != 0 ? newCursorX / sectionWidth : 0;
            
            newCursorX = sectionWidth != 0 ? newCursorX % sectionWidth: 0;
            newCursorY += overflowLines;
            
            return (newCursorX, newCursorY);
        }
        
        private void ClearView()
        {
            var (startX, startY) = startPosition;
            int endX = startX + sectionWidth;
            int endY = startY + sectionHeight;
            
            for (int y = startY; y < endY; y++)
            {
                Console.SetCursorPosition(startX, y);
                Console.Write(new string(' ', endX - startX));
            }
            
            SetCursorPosition(0, 0);
        }

        private void CalculateSectionSize()
        {
            int startY = _windowSizeService.GetMainStartLine();
            int endY = _windowSizeService.Height - borderSize;
            
            int startX = borderSize;
            int endX = _windowSizeService.Width - borderSize;
            
            startPosition = (startX, startY);
            
            sectionWidth = endX - startX;
            sectionHeight = endY - startY;
        }

        private List<StyledString> GetListToRender(Document document)
        {
            int maxLinesCount = sectionHeight;
            List<StyledString> styledStrings = new List<StyledString>();
            
            int linesCount = document.Lines.Count - _scrollOffset;

            for (int i = 0; i < Math.Min(linesCount, maxLinesCount); i++)
            {
                var documentLine = document.GetLine(i + _scrollOffset);
                
                for (int j = 0; j < documentLine.Length; j += sectionWidth)
                {
                    int length = Math.Min(sectionWidth, documentLine.Length - j);
                    var documentLinePart = documentLine.Substring(j, length);
                    styledStrings.Add(documentLinePart);
                }
            }
            
            return styledStrings;
        }

        private void PrintLine(StyledString line)
        {
            bool isPrinted;

            for (int j = 0; j < line.Length; j++)
            {
                isPrinted = false;
                var styledSymbol = line.GetStyledSymbol(j);

                if (styledSymbol.IsSelected)
                {
                    HandleSelectedSymbol(styledSymbol);
                    isPrinted = true;
                }

                if (!isPrinted && (styledSymbol.IsBold || styledSymbol.IsItalic || styledSymbol.IsUnderline))
                {
                    HandleDecoratedSymbol(styledSymbol);
                    isPrinted = true;
                }

                if (!isPrinted)
                {
                    Console.Write(styledSymbol.Symbol);
                }
            }

            Console.SetCursorPosition(startPosition.Item1, Console.CursorTop + 1);
            
        }

        private void HandleSelectedSymbol(StyledSymbol styledSymbol)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(styledSymbol.Symbol);
            Console.ResetColor(); 
            
        }

        private void HandleDecoratedSymbol(StyledSymbol styledSymbol)
        {
            char symbol = styledSymbol.Symbol;
            string decoratedString = symbol.ToString();

            if (styledSymbol.IsBold)
            {
                decoratedString = textDecoratorService.GetBoldText(decoratedString);
            }

            if (styledSymbol.IsItalic)
            {
                decoratedString = textDecoratorService.GetItalicText(decoratedString);
            }

            if (styledSymbol.IsUnderline)
            {
                decoratedString = textDecoratorService.GetUnderlineText(decoratedString);
            }
            
            PrintString(decoratedString);
        }

        
        private void PrintString(string str)
        {
            Console.Write(str);
        }
    }
}
