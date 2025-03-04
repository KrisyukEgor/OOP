using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Movement
{
    public class MoveLeftCommand : ICommand
    {
        IShape currentShape;
        CanvasManager canvasManager;
        public MoveLeftCommand(IShape shape)
        {
            currentShape = shape;
            canvasManager = CanvasManager.getInstance();
        }

        public void Execute()
        {
            canvasManager.MoveLeft(currentShape);
        }
        public void UnExecute()
        {
            canvasManager.MoveRight(currentShape);
        }
    }
}
