

using OOP_1__console_paint_.Canvas;
using System.Reflection.Metadata.Ecma335;

namespace OOP_1__console_paint_.Comands
{
    public class CommandExecutor
    {
        Canvas.CanvasManager canvas;
        //public static string? ExecuteCommand(string command, int[]? args)
        //{
        //    var (action, error) = CommandDictionary.getInstanse().GetFunction(command);

        //    if(error != null)
        //    {
        //        return error;
        //    }

        //    try
        //    {
        //        bool success = action switch
        //        {
        //            Func<int, int, int, bool> threeParam when args?.Length == 3 => threeParam(args[0], args[1], args[2]),
        //            Func<int, int, int, int, bool> fourParam when args?.Length == 4 => fourParam(args[0], args[1], args[2], args[3]),
        //            Func<int, int, int, int, int, bool> fiveParam when args?.Length == 5 => fiveParam(args[0], args[1], args[2], args[3], args[4]),
        //            Func<bool> noParam when args == null => noParam(),
        //            _ => throw new ArgumentException("Неверное количество аргументов")
        //        };

        //        if (!success)
        //        {
        //            return new string("Ошибка: фигура выходит за границы холста");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return new string($"Ошибка при выполнении команды: {e.Message}");
        //    }

        //    return null;
        //}

        public CommandExecutor()
        {
            canvas = Canvas.CanvasManager.getInstance();
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
                        canvas.Erase(new Point(x, y));
                        break;

                }
            } while (key != ConsoleKey.Escape);
            
        }

        public void WriteHelp()
        {
            Console.WriteLine("\n===================================\n Команды \n");
            Console.WriteLine("/drawSquare");
            Console.WriteLine("/drawTriangle");
            Console.WriteLine("/drawRectangle");
            Console.WriteLine("/drawCircle");

            Console.WriteLine("\n/cls: Очищает командную строку");
            Console.WriteLine("/exit: Выход");

            Console.WriteLine("\n===================================\n ");
        }
        public void Exit()
        {
            Console.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }

        public void Undo()
        {
           
        }
    }
}
