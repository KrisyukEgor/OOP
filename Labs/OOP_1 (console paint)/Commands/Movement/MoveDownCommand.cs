using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Movement
{
    public class MoveDownCommand : ICommand
    {
        IShape currentShape;
        CanvasManager canvasManager;
        public MoveDownCommand(IShape shape)
        {
            currentShape = shape;
            canvasManager = CanvasManager.getInstance();
        }

        public void Execute()
        {
            canvasManager.MoveDown(currentShape);
        }
        public void UnExecute()
        {
            canvasManager.MoveUp(currentShape);
        }
    }
}
