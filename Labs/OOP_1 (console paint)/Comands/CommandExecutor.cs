
using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;
using System.Runtime.CompilerServices;



namespace OOP_1__console_paint_.Comands
{
    public class CommandExecutor
    {
        Canvas.Canvas canvas;
       
        Terminal terminal;
        
        public CommandExecutor()
        {
            canvas = Canvas.Canvas.getInstance();
            terminal = Terminal.getInstance();
        }
        public bool DrawCircle(int x, int y, int r)
        {
            if(canvas.DrawCircle(x, y, r))
            {
                return true;
            }
            return false;
        }
        public bool DrawSquare(int x, int y, int length)
        {
            if (canvas.DrawSquare(x, y, length))
            {
                return true;
            }
            return false;
        }

        public bool DrawRectangle(int x, int y, int width, int height)
        {
            if (canvas.DrawRectangle(x, y, width, height))
            {
                return true;
            }
            return false;
        }

        public bool DrawTriangle(int x, int y, int leftSide, int baseSide, int rightSide)
        {
            if (canvas.DrawTriangle(x, y, leftSide, baseSide, rightSide))
            {
                return true;
            }
            return false;
        }

        public void Erase()
        {
            Point point = new Point(10, 10);

            while (point.x != -1 && point.y != -1)
            {
                point = canvas.ChoosePoint();
                IShape? shape = ChooseShape(point);
                if (shape != null)
                {
                    canvas.Erase(shape);
                }
            }
        }

        public void Move()
        {
            IShape? shape = null;
            while(shape == null)
            {
                Point point = canvas.ChoosePoint();

                if (point.x == -1 && point.y == -1)
                {
                    return;
                }
                shape = ChooseShape(point);
            }
            
            StartMove(shape);
        }
        private void StartMove(IShape shape)
        {
            ConsoleKey key;
            IShape? movingShape = shape;
            
            do
            {
                var (cursorX, cursorY) = canvas.GetScaledPoint(movingShape.GetCenter().x, movingShape.GetCenter().y);
                Console.SetCursorPosition(cursorX, cursorY);
                key = Console.ReadKey(true).Key;
                
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        movingShape = canvas.MoveLeft(movingShape);
                        break;
                    case ConsoleKey.RightArrow:
                        movingShape = canvas.MoveRight(movingShape);
                        break;
                    case ConsoleKey.UpArrow:
                        movingShape = canvas.MoveUp(movingShape);
                        break;
                    case ConsoleKey.DownArrow:
                        movingShape = canvas.MoveDown(movingShape);
                        break;

                }
                
            } while (key != ConsoleKey.Escape);

        }

        private IShape? ChooseShape(Point point)
        {
            var (consoleX, consoleY) = canvas.GetUnscaledPoint(point.x, point.y);
            List<IShape> shapeList = canvas.GetShapesWhichContainPoint(new Point(consoleX, consoleY));

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
                terminal.Write($"{i + 1}. {shape.GetName()} с центром в точке ({shape.GetCenter().x}, {shape.GetCenter().y}) и сторонами (радиусом): ");

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

        public void WriteHelp()
        {
            terminal.WriteLine("\n===================================\n Команды \n");
            terminal.WriteLine("/drawSquare");
            terminal.WriteLine("/drawTriangle");
            terminal.WriteLine("/drawRectangle");
            terminal.WriteLine("/drawCircle");

            terminal.WriteLine("\n/cls: Очищает командную строку");
            terminal.WriteLine("/exit: Выход");

            terminal.WriteLine("\n===================================");
        }
        public void Exit()
        {
            terminal.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }

        public void Undo()
        {
           
        }
    }
}
