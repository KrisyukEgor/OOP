
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
        public void Start()
        {
            canvas.DrawCanvas();
            canvas.DrawRectangle(48, 8, 10, 10);
            canvas.DrawRectangle(50, 10, 6, 6);
            //canvas.DrawCircle(35, 20, 16)
            //canvas.DrawTriangle(56, 10, 6, 8, 10);
            comandManager.Start();
            
        }

    }
}
