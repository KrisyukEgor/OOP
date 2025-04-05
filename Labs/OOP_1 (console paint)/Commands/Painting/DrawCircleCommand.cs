using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Drawing
{
    class DrawCircleCommand : ICommand
    {
        private int x, y, radius;
        private CanvasManager canvas;
        IShape? currentShape = null;

        public DrawCircleCommand(int x, int y, int radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            canvas = CanvasManager.getInstance();
        }

        public void Execute()
        {
            currentShape = canvas.DrawCircle(x, y, radius);
            if (currentShape == null)
            {
                throw new InvalidOperationException("Не удалось нарисовать круг");
            }
        }

        public void UnExecute()
        {
            if (currentShape != null)
            {
                canvas.Erase(currentShape);
                currentShape = null;
            }
        }
    }
}
