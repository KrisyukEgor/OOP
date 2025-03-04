using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Canvas.Managers
{
    public class UserInputHandler
    {
        private readonly int _width;
        private readonly int _height;
        Terminal terminal;
        CanvasTransformer transformer;
        ShapeManager shapeManager;

        public UserInputHandler()
        {
            _width = CanvasManager.Width;
            _height = CanvasManager.Height;

            terminal = Terminal.getInstance();
            transformer = new CanvasTransformer();
            shapeManager = ShapeManager.getInstance();
        }

        public Point ChoosePoint()
        {
            ConsoleKey key;
            int x = _width / 2;
            int y = _height / 2;

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
                        if (x < _width - 2) x++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 0) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < _height - 1) y++;
                        break;
                    case ConsoleKey.Enter:
                        return new Point(x, y);
                }
            } while (key != ConsoleKey.Escape);

            return new Point(-1, -1);
        }

        public IShape? ChooseShape(Point point)
        {
            var (consoleX, consoleY) = transformer.GetUnscaledPoint(point.x, point.y);
            List<IShape> shapeList = shapeManager.GetShapesWhichContainPoint(new Point(consoleX, consoleY));

            if (shapeList.Count == 0)
            {
                return null;
            }
            if (shapeList?.Count == 1)
            {
                return shapeList[0];
            }

            terminal.WriteLine("Введите номер фигуры");
            for (int i = 0; i < shapeList?.Count; i++)
            {
                IShape shape = shapeList[i];
                terminal.Write($"{i + 1}. Фигура с центром в точке ({shape.GetCenter().x}, {shape.GetCenter().y}) и сторонами (радиусом): ");

                int[] parameters = shape.GetParameters();
                for (int j = 2; j < parameters.Length; j++)
                {
                    terminal.Write($"{parameters[j]}");
                    if (j != parameters.Length - 1)
                    {
                        terminal.Write(", ");
                    }
                }
                terminal.WriteLine();
            }
            string? inputNumber;
            int index;
            IShape? choosedShape;
            while (true)
            {
                inputNumber = terminal.ReadLine();
                index = TerminalParser.ParseStringToInt(inputNumber);

                if (index == -1)
                {
                    terminal.WriteLine("Некорректное число, введите заново");
                }
                else
                {
                    try
                    {
                        choosedShape = shapeList?.ElementAt(index - 1);
                        if (choosedShape != null)
                        {
                            break;
                        }

                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        terminal.WriteLine("Выберите число из диапазона");
                    }
                }
            }

            return choosedShape;
        }

    }

}
