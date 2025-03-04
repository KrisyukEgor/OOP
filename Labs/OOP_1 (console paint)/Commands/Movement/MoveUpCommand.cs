using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Movement
{
    public class MoveUpCommand : ICommand
    {
        IShape currentShape;
        CanvasManager canvasManager;
        public MoveUpCommand(IShape shape)
        {
            currentShape = shape;
            canvasManager = CanvasManager.getInstance();
        }

        public void Execute()
        {
            canvasManager.MoveUp(currentShape);
        }
        public void UnExecute()
        {
            canvasManager.MoveDown(currentShape);
        }
    }
}
