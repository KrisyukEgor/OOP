
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Rectangle : IShape
    {
        Point _center;
        Point _topLeft;
        List<Point>? _allPoints = null;
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

        private Point CalculateCenter()
        {
            int xCenter = _topLeft.x + (int)(_width / 2);
            int yCenter = _topLeft.y + (int)(_height / 2);
            Point center = new Point(xCenter, yCenter);
            return center;
        }

        public Point GetCenter()
        {
            return _center;
        }

        public bool IsContainPoint(Point point)
        {
            if ((point.x >= _topLeft.x && point.x <= _topLeft.x + _width ) && (point.y >= _topLeft.y && point.y <= _topLeft.y + _height))
            {
                return true;
            }

            return false;
        }

        public List<Point> GetAllPoints()
        {
            if(_allPoints != null)
            {
                return _allPoints;

            }

            _allPoints = new List<Point> ();
            Point tempPoint;
            
            for(int i = 1; i < _width; ++i)
            {
                tempPoint = new Point(_topLeft.x + i, _topLeft.y);
                _allPoints.Add(tempPoint);
            }
            for (int i = 1; i < _width; ++i)
            {
                tempPoint = new Point(_topLeft.x + i, _topLeft.y + _height);
                _allPoints.Add(tempPoint);
            }

            for(int i = 1; i < _height; ++i)
            {
                tempPoint = new Point(_topLeft.x, _topLeft.y + i);
                _allPoints.Add(tempPoint);
            }

            for (int i = 1; i < _height; ++i)
            {
                tempPoint = new Point(_topLeft.x + _width, _topLeft.y + i);
                _allPoints.Add(tempPoint);
            }

            foreach(Point point in GetVertexPoints())
            {
                _allPoints.Add(point);
            }

            return _allPoints;
        }
    }
}
