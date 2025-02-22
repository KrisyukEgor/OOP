
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Rectangle : IShape
    {
        Point _center;
        Point _topLeft;
        int _width;
        int _height;

        public Rectangle(int xTop, int yTop, int width, int height)
        {

            _topLeft = new Point(xTop, yTop);
            this._width = width;
            this._height = height;
            _center = CalculateCenter();
        }

        public List<Point> GetVertexPoints()
        {
            List<Point> pointsList = new List<Point>();

            pointsList.Add(new Point(_topLeft.x, _topLeft.y)); //TopLeft
            pointsList.Add(new Point(_topLeft.x + _width, _topLeft.y)); //TopRight
            pointsList.Add(new Point(_topLeft.x + _width, _topLeft.y + _height)); //BottomRight
            pointsList.Add(new Point(_topLeft.x, _topLeft.y + _height)); //BottomLeft
            return pointsList;
        }
        public Point GetCenter()
        {
            return _center;
        }

        private Point CalculateCenter()
        {
            int xCenter = _topLeft.x + (int)(_width / 2);
            int yCenter = _topLeft.y + (int)(_height / 2);
            Point center = new Point(xCenter, yCenter);
            return center;
        }
    }
}
