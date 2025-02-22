
using OOP_1__console_paint_.Canvas.Shapes;
using System.Xml.Serialization;

namespace OOP_1__console_paint_.Canvas
{
    public class CanvasManager
    {
        private int _width = 120;
        private int _height = 24;
        private double scale = 0.5;

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
            set
            {
                if (value > 0)
                {
                    _width = value;
                }
                else
                {

                }
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value > 0)
                {
                    _height = value;
                }
                else
                {

                }
            }
        }


        public void DrawShape()
        {

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
                Console.SetCursorPosition(i, _height - 1);
                Console.Write("-");
            }
            for (int i = 1; i < _height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
            }

            for (int i = 1; i < _height; i++)
            {
                Console.SetCursorPosition(_width - 1, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("#");

            Console.SetCursorPosition(_width - 1, 0);
            Console.Write("#");

            Console.SetCursorPosition(_width - 1, _height - 1);
            Console.Write("#");

            Console.SetCursorPosition(0, _height - 1);
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
        public void DrawRectangle(int xTop, int yTop, int width, int height)
        {
            Rectangle rectangle = new Rectangle(xTop, yTop, width, height);
            List<Point> pointList = rectangle.GetVertexPoints();

            Point topLeftPoint = pointList[0];
            Point topRightPoint = pointList[1];
            Point bottomRightPoint = pointList[2];
            Point bottomLeftPoint = pointList[3];

            DrawLine(topLeftPoint, topRightPoint, '-');
            DrawLine(bottomLeftPoint, bottomRightPoint, '-');
            DrawLine(topLeftPoint, bottomLeftPoint, '|');
            DrawLine(topRightPoint, bottomRightPoint, '|');
            DrawVertexPoints(pointList);
        }
        public void DrawSquare(int xTop, int yTop, int length)
        {
            DrawRectangle(xTop, yTop, length, length);
        }

        public void DrawTriangle(int xTop, int yTop, int leftSideLength, int baseLength, int rightSideLength)
        {
            Triangle triangle = new Triangle(xTop, yTop, leftSideLength, baseLength, rightSideLength);
            List<Point> pointList = triangle.GetVertexPoints();

            Point top = pointList[0];
            Point rightBottom = pointList[1];
            Point leftBottom = pointList[2];

            DrawLine(leftBottom, rightBottom, '-');
            DrawLine(top, rightBottom, '\\');
            DrawLine(top, leftBottom, '/');
            DrawVertexPoints(pointList);

        }

        public void DrawCircle(int xCenter, int yCenter, int radius)
        {
            Circle circle = new Circle(xCenter, yCenter, radius);
            DrawSymbol(circle.GetCenter().x, circle.GetCenter().y, '#');

            int d = 3 - 2 * radius;
            int x = radius, y = 0;

            while (x >= y)
            {
                
                DrawSymbol(xCenter + x, yCenter + y, '.');
                DrawSymbol(xCenter - x, yCenter + y, '.');
                DrawSymbol(xCenter + x, yCenter - y, '.');
                DrawSymbol(xCenter - x, yCenter - y, '.');

                DrawSymbol(xCenter + y, yCenter + x, '.');
                DrawSymbol(xCenter - y, yCenter + x, '.');

                DrawSymbol(xCenter + y, yCenter - x, '.');
                DrawSymbol(xCenter - y, yCenter - x, '.');

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
        }
    }
}
