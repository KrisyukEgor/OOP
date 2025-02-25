
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Triangle : IShape
    {
        Point _center;
        Point _top;
        Point _bottomRight;
        Point _bottomLeft;
        List<Point>? _allPoints = null;

        int _rightSideLength;
        int _leftSideLength;
        int _baseLength;

        public Triangle(int xTop, int yTop, int leftSideLength, int baseLength, int rightSideLength)
        {
            _top = new Point(xTop, yTop);

            this._rightSideLength = rightSideLength;
            this._leftSideLength = leftSideLength;
            this._baseLength = baseLength;

            double tempX = ((_leftSideLength * _leftSideLength + _baseLength * _baseLength - _rightSideLength * _rightSideLength) / (2 * _baseLength));
            double tempY = Math.Sqrt(_leftSideLength * _leftSideLength - (tempX * tempX));

            this._bottomLeft = new Point((int)(_top.x - tempX), (int)(_top.y + tempY));
            this._bottomRight = new Point((int)(_top.x - tempX + _baseLength), (int)(_top.y + tempY));

            _center = CalculateCenter();
        }

        public static bool IsExist(int xTop, int yTop, int leftSideLength, int baseLength, int rightSideLength)
        {
            if (leftSideLength + baseLength <= rightSideLength ||
                leftSideLength + rightSideLength <= baseLength ||
                baseLength + rightSideLength <= leftSideLength)
            {
                return false;
            }

            return true;
        }

        public List<Point> GetVertexPoints()
        {
            List<Point> pointsList = new List<Point>();

            pointsList.Add(_top);
            pointsList.Add(_bottomRight);
            pointsList.Add(_bottomLeft);

            return pointsList;
        }

        public Point GetCenter()
        {
            return _center;
        }

        private Point CalculateCenter()
        {
            int xCenter = (int)((_top.x + _bottomLeft.x + _bottomRight.x) / 3);
            int yCenter = (int)((_top.y + _bottomLeft.y + _bottomRight.y) / 3);
            Point center = new Point(xCenter, yCenter);
            return center;
        }

        public bool IsContainPoint(Point P)
        {
            (int x, int y) A = (_top.x, _top.y);
            (int x, int y) B = (_bottomLeft.x, _bottomLeft.y);
            (int x, int y) C = (_bottomRight.x, _bottomRight.y);

            float denominator = (float)((B.y - C.y) * (A.x - C.x) + (C.x - B.x) * (A.y - C.y));
            float lambda1 = ((B.y - C.y) * (P.x - C.x) + (C.x - B.x) * (P.y - C.y)) / denominator;
            float lambda2 = ((C.y - A.y) * (P.x - C.x) + (A.x - C.x) * (P.y - C.y)) / denominator;
            float lambda3 = 1 - lambda1 - lambda2;


            return lambda1 >= 0 && lambda2 >= 0 && lambda3 >= 0;
        }

        public List<Point> GetAllSidesPoints()
        {
            if (_allPoints != null)
            {
                return _allPoints;
            }

            _allPoints = new List<Point>();

            foreach (Point point in GetPointsFromPoints(_top, _bottomLeft))
            {
                _allPoints.Add(point);
            }

            foreach (Point point in GetPointsFromPoints(_bottomLeft, _bottomRight))
            {
                _allPoints.Add(point);
            }

            foreach (Point point in GetPointsFromPoints(_top, _bottomRight))
            {
                _allPoints.Add(point);
            }

            return _allPoints;
        }


        private List<Point> GetPointsFromPoints(Point p1, Point p2)
        {
            List<Point> points = new List<Point>();

            int x1 = p1.x, y1 = p1.y;
            int x2 = p2.x, y2 = p2.y;

            int dx = Math.Abs(x1 - x2);
            int dy = Math.Abs(y1 - y2);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy, e2;

            while (true)
            {
                points.Add(new Point(x1, y1));

                if (x1 == x2 && y1 == y2) break;
                e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
            return points;
        }

        public int[] GetParameters()
        {
            int[] result = new int[5];
            result[0] = _center.x;
            result[1] = _center.y;
            result[2] = _leftSideLength;
            result[3] = _baseLength;
            result[4] = _rightSideLength;

            return result;
        }

        public string GetName()
        {
            return new string("Треугольник");
        }
    }
}
