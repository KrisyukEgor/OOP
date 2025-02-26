
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;
using System.Runtime.CompilerServices;


namespace OOP_1__console_paint_.Canvas
{
    public class CanvasManager
    {
        private int _width = 120;
        private int _height = 24;
        private double scale = 0.5;

        private List<IShape> _ShapesList = new List<IShape>();

        private static CanvasManager? instanse;
        public static CanvasManager getInstance()
        {
            if (instanse == null)
            {
                instanse = new CanvasManager();
            }
            return instanse;
        }
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }

        }

        public void DrawCanvas()
        {
            for (int i = 0; i < _width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("-");
            }

            for (int i = 0; i < _width; i++)
            {
                Console.SetCursorPosition(i, _height);
                Console.Write("-");
            }
            for (int i = 1; i <= _height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }

            for (int i = 1; i <= _height; i++)
            {
                Console.SetCursorPosition(_width - 1, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("#");

            Console.SetCursorPosition(_width - 1, 0);
            Console.Write("#");

            Console.SetCursorPosition(_width - 1, _height);
            Console.Write("#");

            Console.SetCursorPosition(0, _height);
            Console.Write("#");
        }
        private void DrawSymbol(int x, int y, char symbol)
        {
            y = (int)(y * scale);
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private void DrawVertexPoints(List<Point> pointList)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                DrawSymbol(pointList[i].x, pointList[i].y, '#');
            }
        }

        private void DrawLine(Point p1, Point p2, char symbol)
        {
            int x1 = p1.x, y1 = p1.y;
            int x2 = p2.x, y2 = p2.y;

            int dx = Math.Abs(x1 - x2);
            int dy = Math.Abs(y1 - y2);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy, e2;

            while (true)
            {
                DrawSymbol(x1, y1, symbol);

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
        }

        private bool CanDraw(List<Point> pointList)
        {
            foreach (Point point in pointList)
            {
                if ((point.x <= 0 || point.y <= 0) || (point.x >= _width || point.y >= _height / scale))
                {
                    return false;
                }
            }
            return true;
        }
        public bool DrawRectangle(int xTop, int yTop, int width, int height)
        {
            Rectangle rectangle = new Rectangle(xTop, yTop, width, height);
            _ShapesList.Add(rectangle);

            List<Point> pointList = rectangle.GetVertexPoints();
            if (!CanDraw(pointList))
            {
                return false;
            }
            
           
            Point topLeftPoint = pointList[0];
            Point topRightPoint = pointList[1];
            Point bottomRightPoint = pointList[2];
            Point bottomLeftPoint = pointList[3];

            DrawLine(topLeftPoint, topRightPoint, '-');
            DrawLine(bottomLeftPoint, bottomRightPoint, '-');
            DrawLine(topLeftPoint, bottomLeftPoint, '|');
            DrawLine(topRightPoint, bottomRightPoint, '|');
            DrawVertexPoints(pointList);

            return true;
        }
        public bool DrawSquare(int xTop, int yTop, int length)
        {
            DrawRectangle(xTop, yTop, length, length);
            return true;
        }

        public bool DrawTriangle(int xTop, int yTop, int leftSideLength, int baseLength, int rightSideLength)
        {
            if (!Triangle.IsExist(xTop, yTop, leftSideLength, baseLength, rightSideLength))
            {
                return false;
            }

            Triangle triangle = new Triangle(xTop, yTop, leftSideLength, baseLength, rightSideLength);
            _ShapesList.Add(triangle);

            List<Point> pointList = triangle.GetVertexPoints();
            if (!CanDraw(pointList))
            {
                return false;
            }
            Point top = pointList[0];
            Point rightBottom = pointList[1];
            Point leftBottom = pointList[2];

            DrawLine(leftBottom, rightBottom, '-');
            DrawLine(top, rightBottom, '\\');
            DrawLine(top, leftBottom, '/');
            DrawVertexPoints(pointList);
            return true;
        }

        public bool DrawCircle(int xCenter, int yCenter, int radius)
        {
            Circle circle = new Circle(xCenter, yCenter, radius);
            _ShapesList.Add(circle);

            List<Point> pointList = circle.GetVertexPoints();
            if (!CanDraw(pointList))
            {
                return false;
            }

            foreach (Point point in pointList)
            {
                DrawSymbol(point.x, point.y, '.');
            }
            return true;
        }
        public List<IShape>? GetShapesWhichContainPoint(Point erasePoint)
        {
            erasePoint.y = (int)(erasePoint.y / scale);
            List<IShape>? shapeList = _ShapesList.Where(shape => (shape.IsContainPoint(erasePoint))).ToList();
            return shapeList;
        }
        public void Erase(IShape? shape)
        {

            List<Point>? points = shape?.GetAllSidesPoints();

            foreach (Point point in points)
            {
                Console.SetCursorPosition(point.x, (int)(point.y * scale));
                Console.Write(' ');
            }

            if (shape != null)
            {
                _ShapesList.Remove(shape);
                RedrawShapesAfterDeleteShape(shape);
            }
        }

        private void RedrawShapesAfterDeleteShape(IShape shape) 
        {
            List<Point> shapesSidesPoints = shape.GetAllSidesPoints();
            HashSet<IShape> shapesToRedraw = new HashSet<IShape>();

            foreach (Point point in shapesSidesPoints)
            {
                List<IShape>? tempList = GetShapesWhichContainPoint(point);

                if (tempList != null)
                {
                    foreach (IShape tempShape in tempList)
                    {
                        if (!shapesToRedraw.Contains(tempShape)) 
                        {
                            shapesToRedraw.Add(tempShape);
                        }
                    }
                }
            }
            Terminal terminal = Terminal.getInstance();
            terminal.WriteLine($"count {shapesToRedraw.Count}");
            foreach(IShape shapeToRedraw in shapesToRedraw)
            {
                RedrawShape(shapeToRedraw);
            }
        }

        private void RedrawShape(IShape shape)
        {

            int[] parameters = shape.GetParameters();

            if (shape.GetName() == "Круг")
            {
                DrawCircle(parameters[0], parameters[1], parameters[2]);
            }
            if (shape.GetName() == "Прямоугольник")
            {

                DrawRectangle(parameters[0], parameters[1], parameters[2], parameters[3]);
            }
            if (shape.GetName() == "")
            {
                DrawTriangle(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
            }
        }
    }

    
}
