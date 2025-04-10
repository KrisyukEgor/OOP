using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Views
{
    public class ConsoleDocumentView : IDocumentViewer
    {
        private int _firstLineIndex = 0;
        private int cursorX = 0;
        private int cursorY = 0;
        
        private WindowSizeController _windowSizeController;

        public ConsoleDocumentView(WindowSizeController windowSizeController)
        {
            _windowSizeController = windowSizeController;

        }

        public void SetCursorPosition(int cursorX, int cursorY)
        {
            this.cursorX = cursorX;
            this.cursorY = cursorY;
            Console.SetCursorPosition(cursorX, cursorY);
        }

        private void SetCursorPosition()
        {
            Console.SetCursorPosition(cursorX, cursorY);
        }
        public void Render(Document document, int firstLineIndex)
        {
            
            _firstLineIndex = firstLineIndex;
            
            ClearArea();

            // int windowWidth = _windowSizeController.Width;
            // int windowHeight = _windowSizeController.Height;
            
            // int startLine = firstLineIndex;
            // int endLine = Math.Min(_document.Lines.Count, startLine + windowHeight);

            for (int i = 0; i < document.Lines.Count; i++)
            {
                var line = document.Lines[i];

                PrintLine(line);
                // for (int j = 0; j < line.Length; j += windowWidth)
                // {
                //     string chunk = line.Substring(j, Math.Min(windowWidth, line.Length - j));
                //     PrintLine(chunk);
                // }

            }
            SetCursorPosition();
        }

       
        public void ClearArea()
        {
            Console.Clear();   //возможно потом изменю
        }

        private void PrintLine(StyledString line)
        {

            Console.WriteLine(line.GetString()); //возможно потом изменю
            
        }
        
    }
}
