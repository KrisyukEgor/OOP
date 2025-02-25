

using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Comands
{
    public class CommandExecutor
    {
        Canvas.CanvasManager canvas;
       
        Terminal terminal;
        
        public CommandExecutor()
        {
            canvas = Canvas.CanvasManager.getInstance();
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

        public void ChooseAndEraseShape()
        {
            ConsoleKey key;
            int x = (int)(canvas.Width / 2);
            int y = (int)(canvas.Height / 2);

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
                        if (x < canvas.Width - 2) x++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (y > 0) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y < canvas.Height - 1) y++;
                        break;
                    case ConsoleKey.Enter:
                        EraseItem(new Point(x, y));
                        break;

                }
            } while (key != ConsoleKey.Escape);

        }
        private void EraseItem(Point point)
        {

            List<IShape>? shapeList = canvas.GetShapesWhichContainPoint(point);
            if(shapeList == null || shapeList.Count == 0)
            {
                return;
            }
            if (shapeList?.Count == 1)
            {
                canvas.Erase(shapeList[0]);
                return;
            }

            terminal.WriteLine("Введите номер фигуры, который хотите удалить");
            for(int i = 0; i < shapeList?.Count; i++)
            {
                IShape shape = shapeList[i];
                terminal.Write($"{i + 1}. {shape.GetName()} с центром в точке ({shape.GetCenter().x}, {(int)(shape.GetCenter().y * 0.5)}) и сторонами (радиусом): ");

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
                        IShape? shapeToDelete = shapeList?.ElementAt(index - 1);
                        if (shapeToDelete != null) { canvas.Erase(shapeToDelete); }
                        break;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        terminal.WriteLine("Выберите число из диапазона");
                    }
                }
            }

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
