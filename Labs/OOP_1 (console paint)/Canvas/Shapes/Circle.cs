
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Circle
    {
        Point _center;
        int _radius;

        public Circle(int xTop, int yTop, int radius)
        {

            _center = new Point(xTop, yTop);
            this._radius = radius;

        }

        public Point GetCenter()
        {
            return _center;
        }

    }
}
