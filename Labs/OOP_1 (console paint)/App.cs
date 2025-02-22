
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
            canvas.DrawRectangle(50, 10, 21, 12);
            canvas.DrawCircle(30, 16, 13);
            canvas.DrawTriangle(73, 10, 6, 8 , 10);
            comandManager.Start();
            
        }

    }
}
