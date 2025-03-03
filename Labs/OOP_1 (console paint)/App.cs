
using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Comands;

namespace OOP_1__console_paint_
{
    public class App
    {
        private readonly CommandManager comandManager;
        private readonly Canvas.CanvasManager canvas;
        public App() 
        {
            comandManager = new CommandManager();
            canvas = CanvasManager.getInstance();
        }
        public void Start()
        {
            canvas.DrawRectangle(26, 8, 15, 15);
            canvas.DrawRectangle(33, 3, 10, 10);
            canvas.DrawRectangle(30, 10, 6, 6);
            comandManager.Start();

        }

    }
}
