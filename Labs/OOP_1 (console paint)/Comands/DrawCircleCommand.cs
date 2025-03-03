

using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Comands
{
    class DrawCircleCommand : ICommand
    {
        private int x, y, radius;
        private Canvas.CanvasManager canvas;
        IShape? currentShape = null;

        public DrawCircleCommand(int x, int y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.canvas = Canvas.CanvasManager.getInstance();
        }

        public bool Execute() 
        {
            int[] parameters = new int[3] {x,y,radius };

            

            return currentShape != null;
        }

        public void UnExecute()
        {
            canvas.Erase(currentShape);

        }
    }
}
