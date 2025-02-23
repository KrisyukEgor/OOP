
using OOP_1__console_paint_.Canvas;

namespace OOP_1__console_paint_.Comands
{
    public class ComandManager
    {
        int comandCursorHeight;
        int comandCursorWidth;
        bool _isRun;

        Dictionary<string, Delegate> commandsDictionary;
        CanvasManager canvas;
        public ComandManager()
        {
            canvas = CanvasManager.getInstance();
            commandsDictionary = new Dictionary<string, Delegate>
            {
                { "/drawcircle", (Action<int, int, int>)((x, y, r) => canvas.DrawCircle(x, y, r))},
                { "/drawsquare", (Action<int, int, int>)((x, y, length) => canvas.DrawSquare(x, y, length))},
                { "/drawrect", (Action<int, int, int, int>)((x, y, w, h) => canvas.DrawRectangle(x, y, w, h))},
                { "/drawtriangle", (Action<int, int, int, int ,int>)((x, y, ls, bs, rs) => canvas.DrawTriangle(x, y, ls, bs, rs)) },
                { "/help", (Action)WriteHelp },
                { "/exit", (Action)Exit }
            };

            comandCursorHeight = canvas.Height + 1;
            comandCursorWidth = 0;
            _isRun = true;
        }
        public void Start()
        {
            Console.SetCursorPosition(comandCursorWidth, comandCursorHeight);
            Console.WriteLine("Введите операцию (все команды /help)");
            string? input;
            string? command;
            int[]? args;    
            while(_isRun)
            {
                input = Console.ReadLine()?.Trim().ToLower();
                command = DetectComand(ref input);

                args = GetArgs(input);

                if (args == null)
                {
                    Console.WriteLine("args is null");
                }

                for (int i = 0; i < args?.Length; i++)
                {
                    Console.Write($"args{i} {args[i]} \t");
                }

                int cursorPos = Console.CursorTop;

                if (command != null)
                {
                    if (args.Contains(-1))
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
                    switch (action)
                    {

                        case Action<int, int, int> threeParamAction when args?.Length == 3:
                            threeParamAction(args[0], args[1], args[2]);
                            break;

                        case Action<int, int, int, int> fourParamAction when args?.Length == 4:
                            fourParamAction(args[0], args[1], args[2], args[3]);
                            break;

                        case Action<int, int, int, int, int> fiveParamAction when args?.Length == 5:
                            fiveParamAction(args[0], args[1], args[2], args[3], args[4]);
                            break;

                        case Action noParamAction when args == null || args.Length == 0:
                            noParamAction();
                            break;

                        default:
                            Console.WriteLine("Ошибка: неверное количество аргументов для команды.");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
    }
}
