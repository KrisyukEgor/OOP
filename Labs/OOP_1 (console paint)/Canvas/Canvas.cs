
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;
using System.Runtime.CompilerServices;


namespace OOP_1__console_paint_.Canvas
{
    public class Canvas
    {
        private int _width = 120;
        private int _height = 24;

        private List<IShape> _ShapesList = new List<IShape>();

        private static Canvas? instanse;
        public static Canvas getInstance()
        {
            if (instanse == null)
            {
                instanse = new Canvas();
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
        public (int, int) GetScaledPoint(int x, int y)
        {
            return (x * 2, y);
        }
        public (int, int) GetUnscaledPoint(int x, int y)
        {
            return (x / 2, y);
        }
        private void DrawSymbol(int x, int y, char symbol)
        {
            (x, y) = GetScaledPoint(x, y);
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

        public bool CanDraw(List<Point> pointList)
        {
            foreach (Point point in pointList)
            {

                var (consoleX, consoleY) = GetScaledPoint(point.x, point.y);
                
                if ((consoleX <= 0 || consoleY <= 0) || (consoleX >= _width - 1 || consoleY >= _height))
                {
                    return false;
                }
            }
            return true;
        }
        public bool DrawRectangle(int xTop, int yTop, int width, int height)
        {
            Rectangle rectangle = new Rectangle(xTop, yTop, width, height);

            List<Point> pointList = rectangle.GetVertexPoints();
            if (!CanDraw(pointList))
            {
                return false;
            }

            _ShapesList.Add(rectangle);

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
            List<Point> pointList = triangle.GetVertexPoints();
            if (!CanDraw(pointList))
            {
                return false;
            }

            _ShapesList.Add(triangle);
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
            List<Point> pointList = circle.GetVertexPoints();

            if (!CanDraw(pointList))
            {
                return false;
            }
            _ShapesList.Add(circle);
            foreach (Point point in pointList)
            {
                DrawSymbol(point.x, point.y, '.');
            }
            return true;
        }

        public List<IShape> GetShapesWhichContainPoint(Point erasePoint)
        {
 
            List<IShape> shapeList = _ShapesList.Where(shape => (shape.IsContainPoint(erasePoint))).ToList();
            return shapeList;
        }

        public void Erase(IShape? shape)
        {

            List<Point>? points = shape?.GetAllSidesPoints();

            foreach (Point point in points)
            {
                var (consoleX, consoleY) = GetScaledPoint(point.x, point.y);
                Console.SetCursorPosition(consoleX, consoleY);
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

            foreach (IShape shapeToRedraw in shapesToRedraw)
            {
                RedrawShape(shapeToRedraw.GetName(), shapeToRedraw.GetParameters());
            }
        }

        private void RedrawShape(string shapeName, int[] parameters)
        {
            RedrawWithoutDelete(shapeName, parameters);
            _ShapesList.RemoveAt(_ShapesList.Count - 1);
        }

        private void RedrawWithoutDelete(string shapeName, int[] parameters)
        {
            if (shapeName == "Круг")
            {
                DrawCircle(parameters[0], parameters[1], parameters[2]);
            }
            else if (shapeName == "Прямоугольник")
            {

                DrawRectangle(parameters[0], parameters[1], parameters[2], parameters[3]);
            }
            else if (shapeName == "Треугольник")
            {
                DrawTriangle(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
            }
        }

        public Point ChoosePoint()
        {
            ConsoleKey key;
            int x = (int)(Width / 2);
            int y = (int)(Height / 2);

            do
            {
                Console.SetCursorPosition(x, y);
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 1) x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x < Width - 2) x++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 0) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < Height - 1) y++;
                        break;
                    case ConsoleKey.Enter:
                        return new Point(x, y);

                }
            } while (key != ConsoleKey.Escape);
            return new Point(-1, -1);
        }

        public IShape MoveRight(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            string shapeName = shape.GetName();
            List<Point> pointList = new List<Point>(shape.GetVertexPoints());

            List<Point> newPointList = new List<Point>();
            foreach (Point point in pointList)
            {
                newPointList.Add(new Point(point.x + 1, point.y));
            }

            if (CanDraw(newPointList))
            {
                parameters[0]++;
                Erase(shape); 
                RedrawWithoutDelete(shapeName, parameters); 
            }
            else { return shape; }
            return _ShapesList.ElementAt(_ShapesList.Count - 1);
        }


        public IShape MoveLeft(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            string shapeName = shape.GetName();
            List<Point> pointList = new List<Point>(shape.GetVertexPoints());

            List<Point> newPointList = new List<Point>();
            foreach (Point point in pointList)
            {
                newPointList.Add(new Point(point.x - 1, point.y));
            }

            if (CanDraw(newPointList))
            {
                parameters[0]--;
                Erase(shape);
                RedrawWithoutDelete(shapeName, parameters);
            }
            else { return shape; }
            return _ShapesList.ElementAt(_ShapesList.Count - 1);
        }

        public IShape MoveUp(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            string shapeName = shape.GetName();
            List<Point> pointList = new List<Point>(shape.GetVertexPoints());

            List<Point> newPointList = new List<Point>();
            foreach (Point point in pointList)
            {
                newPointList.Add(new Point(point.x, point.y - 1));
            }

            if (CanDraw(newPointList))
            {
                parameters[1]--;
                Erase(shape);
                RedrawWithoutDelete(shapeName, parameters);
            }
            else { return shape; }
            return _ShapesList.ElementAt(_ShapesList.Count - 1);
        }

        public IShape MoveDown(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            string shapeName = shape.GetName();
            List<Point> pointList = new List<Point>(shape.GetVertexPoints());

            List<Point> newPointList = new List<Point>();
            foreach (Point point in pointList)
            {
                newPointList.Add(new Point(point.x, point.y + 1));
            }

            if (CanDraw(newPointList))
            {
                parameters[1]++;
                Erase(shape);
                RedrawWithoutDelete(shapeName, parameters);
            }
            else { return shape; }
            return _ShapesList.ElementAt(_ShapesList.Count - 1);
        }

    }
}
