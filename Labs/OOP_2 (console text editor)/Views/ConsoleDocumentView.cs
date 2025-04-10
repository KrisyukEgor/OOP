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
                string line = document.Lines[i];

                PrintLine(line);
                // for (int j = 0; j < line.Length; j += windowWidth)
                // {
                //     string chunk = line.Substring(j, Math.Min(windowWidth, line.Length - j));
                //     PrintLine(chunk);
                // }

            }
            SetCursorPosition();
        }

        public void RenderWithSelection(Document document, int firstLineIndex, List<(int, int )> selection)
        {
            
            _firstLineIndex = firstLineIndex;
            ClearArea();

            int w = _windowSizeController.Width;
            int h = _windowSizeController.Height;
            int drawn = 0;

            var selSet = new HashSet<(int X, int Y)>(selection);

            for (int docY = firstLineIndex; docY < document.Lines.Count && drawn < h; docY++)
            {
                string line = document.Lines[docY];
                int chunks = Math.Max(1, (line.Length + w - 1) / w);

                for (int chunkIdx = 0; chunkIdx < chunks && drawn < h; chunkIdx++, drawn++)
                {
                    int offsetX = chunkIdx * w;
                    int len = Math.Min(w, line.Length - offsetX);
                    string chunk = len > 0 ? line.Substring(offsetX, len) : "";

                    PrintChunkWithSelection(chunk, docY, offsetX, selSet);
                }
            }
            SetCursorPosition();
        }
        public void ClearArea()
        {
            Console.Clear();   //возможно потом изменю
        }

        private void PrintLine(string line)
        {
            Console.WriteLine(line); //возможно потом изменю
        }
        
        private void PrintChunkWithSelection(string chunk, int docY, int offsetX, HashSet<(int X, int Y)> selSet)
        {
            for (int i = 0; i < chunk.Length; i++)
            {
                var pos = (X: offsetX + i, Y: docY);
                if (selSet.Contains(pos))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                }

                Console.Write(chunk[i]);
                Console.ResetColor();
            }
            
        }
    }
}
