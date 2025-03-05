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
            CalculateCenter();
            BackgroundSymbol = ' ';
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

        private void CalculateCenter()
        {
            int xCenter = _topLeft.x + (int)(_width / 2);
            int yCenter = _topLeft.y + (int)(_height / 2);
            _center = new Point(xCenter, yCenter);
       
        }

        public Point GetCenter()
        {
            return _center;
        }

        public bool IsContainPoint(Point point)
        {
            return (point.x >= _topLeft.x && point.x <= _topLeft.x + _width) &&
                   (point.y >= _topLeft.y && point.y <= _topLeft.y + _height);
        }


        public List<Point> GetAllSidesPoints()
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

        public int[] GetParameters()
        {
            int[] result = new int[4];
            result[0] = _topLeft.x;
            result[1] = _topLeft.y;
            result[2] = _width;
            result[3] = _height;

            return result;
        }

        public char BackgroundSymbol { get; set; }

        public List<Point> GetPointsInside()
        {

            int minX = _topLeft.x;
            int maxX = _topLeft.x + _width - 1;
            int minY = _topLeft.y;
            int maxY = _topLeft.y + _height - 1;

            List<Point> borderPoints = GetAllSidesPoints();
            HashSet<Point> borderSet = new HashSet<Point>(borderPoints);

            List<Point> result = new List<Point>();
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Point p = new Point(x, y);

                    if (!borderSet.Contains(p) && IsContainPoint(p))
                    {
                        result.Add(p);
                    }
                }
            }

            return result;
        }

        public void UpdateParameters(int[] paramaters)
        {
            _topLeft.x = paramaters[0];
            _topLeft.y = paramaters[1];
            _width = paramaters[2];
            _height = paramaters[3];
            CalculateCenter();
            _allPoints = null;
        }

        public override string ToString()
        {
            return $"Rectangle: {_topLeft.x}, {_topLeft.y}, {_width}, {_height}, bgColor:{BackgroundSymbol};";
        }

    }
}
