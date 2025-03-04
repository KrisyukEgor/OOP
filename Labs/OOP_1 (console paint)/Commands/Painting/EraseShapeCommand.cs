using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Painting
{
    public class EraseShapeCommand : ICommand
    {
        private readonly CanvasManager canvas;
        private readonly IShape shapeToErase;
        private bool isExecuted;

        public EraseShapeCommand(IShape shape)
        {
            canvas = CanvasManager.getInstance();
            shapeToErase = shape;
            isExecuted = false;
        }

        public void Execute()
        {
            if (!isExecuted)
            {
                canvas.Erase(shapeToErase);
                isExecuted = true;
            }
        }

        public void UnExecute()
        {
            if (isExecuted)
            {
                canvas.DetectAndDrawShape(shapeToErase);
                isExecuted = false;
            }
        }
    }

}
