using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Drawing
{
    public class DrawTriangleCommand : ICommand
    {
        private readonly int x, y, leftSide, baseSide, rightSide;
        private readonly CanvasManager canvas;
        private IShape? currentShape = null;

        public DrawTriangleCommand(int x, int y, int leftSide, int baseSide, int rightSide)
        {
            this.x = x;
            this.y = y;
            this.leftSide = leftSide;
            this.baseSide = baseSide;
            this.rightSide = rightSide;
            canvas = CanvasManager.getInstance();
        }

        public void Execute()
        {
            currentShape = canvas.DrawTriangle(x, y, leftSide, baseSide, rightSide);
            if (currentShape == null)
            {
                throw new InvalidOperationException("Не удалось нарисовать треугольник");
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
