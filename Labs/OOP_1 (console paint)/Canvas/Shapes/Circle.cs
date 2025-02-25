
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

        }

        public Point GetCenter()
        {
            return _center;
        }

        public List<Point> GetVertexPoints()
        {
            return GetAllPoints();
        }

        public bool IsContainPoint(Point point)
        {
            Console.WriteLine($"pointx ({point.x}, {point.y} )");
            if((point.x >= _center.x - _radius && point.x <= _center.x + _radius) && (point.y >= _center.y - _radius && point.y <= _center.y + _radius))
            {
                Console.WriteLine("true");
                return true;
            }
            return false;
        }

        public List<Point> GetAllPoints()
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
    }
}
