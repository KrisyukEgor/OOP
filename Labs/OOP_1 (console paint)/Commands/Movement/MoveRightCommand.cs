using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Movement
{
    public class MoveRightCommand : ICommand
    {
        IShape currentShape;
        CanvasManager canvasManager;
        public MoveRightCommand(IShape shape)
        {
            currentShape = shape;
            canvasManager = CanvasManager.getInstance();
        }

        public void Execute()
        {
            canvasManager.MoveRight(currentShape);
        }
        public void UnExecute() 
        {
            canvasManager.MoveLeft(currentShape);
        }
    }
}
