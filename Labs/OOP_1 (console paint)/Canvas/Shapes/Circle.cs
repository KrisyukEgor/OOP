using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Circle : IShape
    {
        Point _center;
        List<Point>? _allPoints = null;
        int _radius;

        public Circle(int xTop, int yTop, int radius)
        {

            _center = new Point(xTop, yTop);
            this._radius = radius;
            BackgroundSymbol = ' ';
        }

        public Point GetCenter()
        {
            return _center;
        }

        public List<Point> GetVertexPoints()
        {
            return GetAllSidesPoints();
        }

        public bool IsContainPoint(Point point)
        {
            int dx = point.x - _center.x;
            int dy = point.y - _center.y;
            return dx * dx + dy * dy <= _radius * _radius;
        }


        public List<Point> GetAllSidesPoints()
        {
            if (_allPoints != null)
            {
                return _allPoints;
            }

            _allPoints = new List<Point>();
            int d = 3 - 2 * _radius;
            int x = _radius, y = 0;

            while (x >= y)
            {

                _allPoints.Add(new Point(_center.x + x, _center.y + y));
                _allPoints.Add(new Point(_center.x - x, _center.y + y));
                _allPoints.Add(new Point(_center.x + x, _center.y - y));
                _allPoints.Add(new Point(_center.x - x, _center.y - y));

                _allPoints.Add(new Point(_center.x + y, _center.y - x));
                _allPoints.Add(new Point(_center.x - y, _center.y - x));

                _allPoints.Add(new Point(_center.x + y, _center.y + x));
                _allPoints.Add(new Point(_center.x - y, _center.y + x));

                y++;

                if (d < 0)
                {
                    d = d + 4 * y + 6;
                }
                else
                {
                    x--;
                    d = d + 4 * (y - x) + 10;
                }
            }
            return _allPoints;
        }

        public int[] GetParameters()
        {
            int[] result = new int[3];
            result[0] = _center.x;
            result[1] = _center.y;
            result[2] = _radius;

            return result;
        }

        public char BackgroundSymbol { get; set; }

        public List<Point> GetPointsInside()
        {

            List<Point> borderPoints = GetAllSidesPoints();

            int minX = borderPoints.Min(p => p.x);
            int maxX = borderPoints.Max(p => p.x);
            int minY = borderPoints.Min(p => p.y);
            int maxY = borderPoints.Max(p => p.y);

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

        public void UpdateParameters(int[] parameters)
        {
            _center.x = parameters[0];
            _center.y = parameters[1];
            _radius = parameters[2];
            _allPoints = null;
        }
    }
}
