
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
            canvas.DrawRectangle(46, 8, 15, 15);
            canvas.DrawRectangle(53, 3, 10, 10);
            canvas.DrawRectangle(50, 10, 6, 6);
            //canvas.DrawCircle(35, 20, 16)
            //canvas.DrawTriangle(56, 10, 6, 8, 10);
            comandManager.Start();
            
        }

    }
}
