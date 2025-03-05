
using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Painting
{
    public class SetBgColorCommand : ICommand
    {
        readonly CanvasManager canvasManager;
        readonly char newColor;
        readonly char previousColor;
        readonly IShape shape;

        public SetBgColorCommand(IShape shape, char newColor)
        {
            canvasManager = CanvasManager.getInstance();

            this.newColor = newColor;
            this.previousColor = shape.BackgroundSymbol; 
            this.shape = shape;
        }

        public void Execute()
        {
            canvasManager.SetShapeBackground(shape, newColor);
        }

        public void UnExecute()
        {
            canvasManager.SetShapeBackground(shape, previousColor);
        }
    }

}
