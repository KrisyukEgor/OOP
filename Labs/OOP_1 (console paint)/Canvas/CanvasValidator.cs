
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Canvas
{
    public class CanvasValidator
    {

        CanvasTransformer transformer;
        public CanvasValidator() 
        {
            transformer = new CanvasTransformer();
        }

        public bool CanDraw(IShape shape)
        {
            int width = CanvasManager.Width;
            int height = CanvasManager.Height;

            List<Point> pointList = shape.GetVertexPoints();

            foreach (Point point in pointList)
            {

                var (consoleX, consoleY) = transformer.GetScaledPoint(point.x, point.y);

                if ((consoleX <= 0 || consoleY <= 0) || (consoleX >= width - 1 || consoleY >= height))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
