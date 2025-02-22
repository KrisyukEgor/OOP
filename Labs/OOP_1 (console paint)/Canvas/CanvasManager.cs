
using OOP_1__console_paint_.Canvas.Shapes;

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
            get{ return _width; }
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
                Console.Write("_");
            }

            for (int i = 0; i < _width; i++)
            {
                Console.SetCursorPosition(i, _height - 1);
                Console.Write("_");
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

        }

        private void ScalePoint(Point point)
        {

            point.y = (int)(point.y * scale);
        }
        public void DrawSquare(int xCenter, int yCenter, int length)
        {
            Square square = new Square(xCenter, yCenter, length);
            List<Point>squarePoint = square.GetVertexPoints();


            for(int i = 0; i < squarePoint.Count;i++)
            {
                ScalePoint(squarePoint[i]);
                Console.SetCursorPosition(squarePoint[i].x, squarePoint[i].y);
                Console.Write("*");
            }
        }
    }
}
