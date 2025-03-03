
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Comands;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Canvas
{
    public class CanvasManager
    {
        static int _width = 120;
        static int _height = 24;
        Terminal terminal;

        static CanvasManager? instanse;
        CanvasPainter painter;
        ShapeManager shapeManager;
        CanvasValidator validator;

        public static CanvasManager getInstance()
        {
            if (instanse == null)
            {
                instanse = new CanvasManager();
            }
            return instanse;
        }

        private CanvasManager()
        {
             DrawCanvas();
            shapeManager = ShapeManager.getInstance();
            painter = new CanvasPainter();
            validator = new CanvasValidator();
            terminal = Terminal.getInstance();
        }
        public static int Width
        {
            get { return _width; }
        }

        public static int Height
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

        public bool DrawCircle(int xTop, int yTop, int radius)
        {
            Circle circle = shapeManager.CreateCircleShape(xTop, yTop, radius);
            if(!validator.CanDraw(circle))
            {
                return false;
            }

            painter.DrawCircle(circle);

            return true;
        }

        public bool DrawRectangle(int xTop, int yTop, int width, int height)
        {
            Rectangle rectangle = shapeManager.CreateRectangle(xTop, yTop, width, height);

            if (!validator.CanDraw(rectangle))
            {
                
                return false;
            }

            painter.DrawRectangle(rectangle);

            return true;
        }

        public bool DrawTriangle(int xTop, int yTop, int leftSide, int bottomSide, int rightSide)
        {
            if (!Triangle.IsExist(xTop, yTop, leftSide, bottomSide, rightSide)) { return false; }

            Triangle triangle = shapeManager.CreateTriangeShape(xTop, yTop, leftSide, bottomSide, rightSide);

            if (!validator.CanDraw(triangle))
            {
                return false;
            }

            painter.DrawTriangle(triangle);

            return true;
        }

        public void Erase(IShape shape)
        {
            shapeManager.Erase(shape);
        }

        public IShape MoveRight(IShape shape)
        {
            IShape newShape = shapeManager.MoveRight(shape);
            return newShape;
        }

        public IShape MoveLeft(IShape shape)
        {
            IShape newShape = shapeManager.MoveLeft(shape);
            return newShape;
        }

        public IShape MoveUp(IShape shape)
        {
            IShape newShape = shapeManager.MoveUp(shape);
            return newShape;
        }
        public IShape MoveDown(IShape shape)
        {
            IShape newShape = shapeManager.MoveDown(shape);
            return newShape;
        }

        public void SetShapeBackground(IShape shape, char symbol)
        {
            painter.SetShapeBackground(shape, symbol);
        }

    }
}


