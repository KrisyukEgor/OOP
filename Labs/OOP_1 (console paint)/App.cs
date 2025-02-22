
using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Comands;

namespace OOP_1__console_paint_
{
    public class App
    {
        private ComandManager comandManager;
        private CanvasManager canvas;
        public App() 
        {
            comandManager = new ComandManager();
            canvas = CanvasManager.getInstance();
        }
        public async void Start()
        {
            canvas.DrawCanvas();
            canvas.DrawSquare(100, 12, 21);
            comandManager.Start();
            
        }

    }
}
