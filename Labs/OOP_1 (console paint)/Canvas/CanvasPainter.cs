
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Canvas
{
    public class CanvasPainter
    {
        CanvasTransformer transformer;
        public CanvasPainter() 
        {
            transformer = new CanvasTransformer();
        }

        public void DrawSymbol(int x, int y, char symbol)
        {
            (x, y) = transformer.GetScaledPoint(x, y);
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        
        public void DrawLine(Point p1, Point p2, char symbol)
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

        private void DrawVertexPoints(List<Point> pointList)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                DrawSymbol(pointList[i].x, pointList[i].y, '#');
            }
        }

        public bool DrawRectangle(Rectangle rectangle)
        {

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

            char symbol = rectangle.BackgroundSymbol;
            SetShapeBackground(rectangle, symbol);

            return true;
        }

        public bool DrawTriangle(Triangle triangle)
        {
            List<Point> pointList = triangle.GetVertexPoints();
            Point top = pointList[0];
            Point rightBottom = pointList[1];
            Point leftBottom = pointList[2];

            DrawLine(leftBottom, rightBottom, '-');
            DrawLine(top, rightBottom, '\\');
            DrawLine(top, leftBottom, '/');
            DrawVertexPoints(pointList);

            char symbol = triangle.BackgroundSymbol;
            SetShapeBackground(triangle, symbol);

            return true;
        }


        public bool DrawCircle(Circle circle)
        {
  
            List<Point> pointList = circle.GetVertexPoints();

            foreach (Point point in pointList)
            {
                DrawSymbol(point.x, point.y, '.');
            }

            char symbol = circle.BackgroundSymbol;
            SetShapeBackground(circle, symbol);

            return true;
        }

        public void SetShapeBackground(IShape shape, char symbol)
        {
            shape.BackgroundSymbol = symbol;

            HashSet<Point> drawingPoints = shape.GetPointsInside().ToHashSet();

            foreach (var point in drawingPoints)
            {
                DrawSymbol(point.x, point.y, symbol);
            }

        }

        public void ClearPoints(List<Point> pointList)
        {
            foreach (Point point in pointList)
            {
                var (consoleX, consoleY) = transformer.GetScaledPoint(point.x, point.y);
                Console.SetCursorPosition(consoleX, consoleY);
                Console.Write(' ');
            }
        }
    }
}
