
using OOP_1__console_paint_.Canvas;

namespace OOP_1__console_paint_.Comands
{
    public class ComandManager
    {

        Point cursorPositon;
        bool _isRun;

        Dictionary<string, Delegate> commandsDictionary;
        CanvasManager canvas;
        public ComandManager()
        {
            canvas = CanvasManager.getInstance();
            commandsDictionary = new Dictionary<string, Delegate>
            {
                { "/drawcircle", (Func<int, int, int, bool>)((x, y, r) => canvas.DrawCircle(x, y, r))},
                { "/drawsquare", (Func<int, int, int, bool>)((x, y, length) => canvas.DrawSquare(x, y, length))},
                { "/drawrect", (Func<int, int, int, int, bool>)((x, y, w, h) => canvas.DrawRectangle(x, y, w, h))},
                { "/drawtriangle", (Func<int, int, int, int, int, bool>)((x, y, ls, bs, rs) => canvas.DrawTriangle(x, y, ls, bs, rs)) },
                { "/erase", (Func<bool>)(() => {ChooseAndEraseShape(); return true; }) },
                { "/help", (Func<bool>)(() => { WriteHelp(); return true; }) },
                { "/exit", (Func<bool>)(() => { Exit(); return true; }) }
            };
            cursorPositon = new Point(0, canvas.Height + 1);

            _isRun = true;
        }
        public void Start()
        {
            Console.SetCursorPosition(cursorPositon.x, cursorPositon.y);
            Console.WriteLine("Введите операцию (все команды /help)");
            string? input, command;
            int[]? args;    
            while(_isRun)
            {
                input = Console.ReadLine()?.Trim().ToLower();
                command = DetectComand(ref input);

                args = GetArgs(input);

                //if (args == null)
                //{
                //    Console.WriteLine("args is null");
                //}

                //for (int i = 0; i < args?.Length; i++)
                //{
                //    Console.Write($"args{i} {args[i]} \t");
                //}

                int cursorPos = Console.CursorTop;

                if (command != null)
                {
                    if (args!= null && args.Contains(-1))
                    {
                        Console.WriteLine("Введите натуральные значения");
                    }
                    else
                    {
                        DoComand(command, args);
                    }
                }
                else
                {
                    Console.WriteLine("Неверная команда");
                }
                Console.SetCursorPosition(0, cursorPos + 1);
            }
        }

        private void DoComand(string command, int[]? args)
        {
            if (commandsDictionary.TryGetValue(command, out var action))
            {
                try
                {
                    bool success = false; 

                    switch (action)
                    {
                        case Func<int, int, int, bool> threeParamAction when args?.Length == 3:
                            success = threeParamAction(args[0], args[1], args[2]);
                            break;

                        case Func<int, int, int, int, bool> fourParamAction when args?.Length == 4:
                            success = fourParamAction(args[0], args[1], args[2], args[3]);
                            break;

                        case Func<int, int, int, int, int, bool> fiveParamAction when args?.Length == 5:
                            success = fiveParamAction(args[0], args[1], args[2], args[3], args[4]);
                            break;

                        case Func<bool> noParamAction when args == null:
                            success = noParamAction();
                            break;

                        default:
                            Console.WriteLine("Неверное количество аргументов для команды");
                            return; 
                    }

                    if (!success)
                    {
                        Console.WriteLine("Фигура выходит за рамки холста");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Ошибка: неверная команда");
            }
        }


        private string? DetectComand(ref string input)
        {
            string? result = "";
            int slashIndex = input.IndexOf('/');

            if(slashIndex == -1) { return null; }

            int spaceIndex = input.IndexOf(" ");

            if(spaceIndex == -1)
            {
                result = input;
                input = "";
            }
            else
            {
                result = input.Substring(0, spaceIndex);
                input = input.Substring(spaceIndex + 1);
            }
            return result;
        }

        private int ParseStringToInt(string str)
        {
            int result = -1;

            if (!int.TryParse(str, out result)) { return -1; };
            
            return result;
        }
        private int[]? GetArgs(string input)
        {
            int[]? args = null;
            int semicolonIndex = input.IndexOf(";");

            if (semicolonIndex == -1) { return null; }

            string[] parts = input.Split(';');

            if (parts.Length == 2)
            {
                string[] values = parts[1].Trim().Split(",");
                args = new int[2 + values.Length];
                string[] coordinates = parts[0].Split(',');

                if (coordinates.Length == 2)
                {
                    args[0] = ParseStringToInt(coordinates[0]);
                    args[1] = ParseStringToInt(coordinates[1]);

                }
                else
                {
                    return null;
                }

                for (int i = 0; i < values.Length; ++i)
                {
                    args[2 + i] = ParseStringToInt(values[i]);
                }
                Console.WriteLine();
            }
            else
            {
                return null;
            }

            return args;
        }
        private void Exit()
        {
            _isRun = false;
        }

        private void WriteHelp()
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

        private void ChooseAndEraseShape()
        {
            ConsoleKey key;
            int x = (int)(canvas.Width / 2);
            int y = (int)(canvas.Height / 2);
            
            do
            {
                Console.SetCursorPosition(x, y);
                key = Console.ReadKey(true).Key;

                switch(key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 1) x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if(x < canvas.Width - 2) x++;
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
    }
}
