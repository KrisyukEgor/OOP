using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Drawing
{
    public class DrawRectangleCommand : ICommand
    {
        private readonly int x, y, width, height;
        private readonly CanvasManager canvas;
        private IShape? currentShape = null;

        public DrawRectangleCommand(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            canvas = CanvasManager.getInstance();
        }

        public void Execute()
        {

            currentShape = canvas.DrawRectangle(x, y, width, height);
            if (currentShape == null)
            {
                throw new InvalidOperationException("Не удалось нарисовать прямоугольник");
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
