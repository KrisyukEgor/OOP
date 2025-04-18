using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;
using OOP_2__console_text_editor_.Services;
using OOP_2__console_text_editor_.Utils;

namespace OOP_2__console_text_editor_.Views
{
    public class ConsoleDocumentView : IDocumentViewer
    {
        private int _firstLineIndex = 0;
        
        private WindowService _windowService;
        private TextDecoratorService textDecoratorService;

        public ConsoleDocumentView(WindowService windowService)
        {
            _windowService = windowService;
            textDecoratorService = new TextDecoratorService();

        }
        
        public void Render(Document document, int firstLineIndex)
        {
            
            _firstLineIndex = firstLineIndex;
            ClearArea();

            int windowHeight = _windowService.Height;

            int startLine = Math.Max(0, _firstLineIndex);
            int endLine = Math.Min(document.Lines.Count, startLine + windowHeight);

            for (int i = startLine; i < endLine; i++)
            {
                PrintLine(document.Lines[i], i - startLine);
            }
            
        }
        
        public void ClearArea()
        {
            Console.Clear();   //возможно потом изменю
        }

        private void PrintLine(StyledString line, int row)
        {
            // int windowWidth = _windowSizeController.Width;
            
            // string visiblePart = plainLine.Length > windowWidth 
            //     ? plainLine.Substring(0, windowWidth) 
            //     : plainLine;
            //
            // visiblePart = visiblePart.PadRight(windowWidth);

            // Console.SetCursorPosition(0, row);
            
            bool isPrinted = false;
            
            for (int i = 0; i < line.Length; i++)
            {
                var styledSymbol = line.GetStyledSymbol(i);

                if (styledSymbol.IsSelected)
                {
                    HandleSelectedSymbol(styledSymbol);
                    isPrinted = true;
                }
                
                else if (styledSymbol.IsBold || styledSymbol.IsItalic || styledSymbol.IsUnderline) 
                {
                    HandleDecoratedSymbol(styledSymbol);
                    isPrinted = true;
                }
                
                if(!isPrinted)
                {
                    Console.Write(styledSymbol.Symbol);
                }

                isPrinted = false;

            }
            Console.WriteLine();
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

        private void PrintChar(char symbol)
        {
            Console.Write(symbol);
        }

        private void PrintString(string str)
        {
            Console.Write(str);
        }
    }
}
