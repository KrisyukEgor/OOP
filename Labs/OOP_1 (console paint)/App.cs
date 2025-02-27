
using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Comands;

namespace OOP_1__console_paint_
{
    public class App
    {
        private CommandManager comandManager;
        private Canvas.Canvas canvas;
        public App() 
        {
            comandManager = new CommandManager();
            canvas = Canvas.Canvas.getInstance();
        }
        public void Start()
        {
            canvas.DrawCanvas();
            canvas.DrawRectangle(26, 8, 15, 15);
            canvas.DrawRectangle(33, 3, 10, 10);
            canvas.DrawRectangle(30, 10, 6, 6);
            //canvas.DrawCircle(15, 10, 6);
            //canvas.DrawTriangle(26, 10, 6, 8, 10);
            comandManager.Start();
            
        }

    }
}
