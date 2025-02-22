
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Triangle : IShape
    {
        Point _center;
        Point _top;
        Point _bottomRight;
        Point _bottomLeft;
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
            int xCenter = (int)((_top.x + _bottomLeft.x + _bottomRight.x)/ 3);
            int yCenter = (int)((_top.y + _bottomLeft.y + _bottomRight.y) / 3);
            Point center = new Point(xCenter, yCenter);
            return center;
        }

        
    }
}
