using OOP_2__console_text_editor_.Controllers;
using OOP_2__console_text_editor_.Interfaces;
using OOP_2__console_text_editor_.Models;

namespace OOP_2__console_text_editor_.Views
{
    public class ConsoleDocumentView : IDocumentViewer
    {
        private Document? _document;
        private int _firstLineIndex = 0;
        private WindowSizeController _windowSizeController;

        public ConsoleDocumentView(WindowSizeController windowSizeController)
        {
            _windowSizeController = windowSizeController;
        }

        public Document Document
        {
            get => _document;
            set => _document = value;
        }
        public void Render(int firstLineIndex)
        {
            if (_document == null)
            {
                return;
            }
            
            this._firstLineIndex = firstLineIndex;
            
            ClearArea();

            int windowWidth = _windowSizeController.Width;
            int windowHeight = _windowSizeController.Height;
            
            int startLine = firstLineIndex;
            int endLine = Math.Min(_document.Lines.Count, startLine + windowHeight);

            for (int i = startLine; i < endLine; i++)
            {
                string line = _document.Lines[i];

                for (int j = 0; j < line.Length; j += windowWidth)
                {
                    string chunk = line.Substring(j, Math.Min(windowWidth, line.Length - j));
                    PrintLine(chunk);
                }
                
            }
        }

        public void ClearArea()
        {
            Console.Clear();   //возможно потом изменю
        }

        private void PrintLine(string line)
        {
            Console.WriteLine(line); //возможно потом изменю
        }
    }
}
