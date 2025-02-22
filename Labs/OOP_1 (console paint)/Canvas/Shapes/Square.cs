
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Square
    {
        Point _center;
        int _length;

        public Square(int xCenter, int yCenter, int length)
        {
            _center = new Point(xCenter, yCenter);
            this._length = length;
        }

        public List<Point> GetVertexPoints()
        {
            List<Point> pointsList = new List<Point> ();

            int topLeftX = _center.x - (int)(_length / 2);
            int topLeftY = _center.y - (int)(_length / 2);

            pointsList.Add(new Point(topLeftX, topLeftY)); //TopLeft
            pointsList.Add(new Point(topLeftX + _length, topLeftY)); //TopRight
            pointsList.Add(new Point(topLeftX + _length, topLeftY + _length)); //BottomRight
            pointsList.Add(new Point(topLeftX, topLeftY + _length)); //BottomLeft
            return pointsList;
        }

        public Point GetCenter()
        {
            return _center;
        }
    }
}
