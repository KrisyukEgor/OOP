
using OOP_1__console_paint_.Canvas;
using System.Reflection.Metadata.Ecma335;

namespace OOP_1__console_paint_.Comands
{
    public class CommandManager
    {

        Point cursorPositon;
        bool _isRun;
        CommandDictionary commandDictionary;
        Canvas.CanvasManager canvas;
        CommandExecutor executor;
       
        public CommandManager()
        {
            canvas = Canvas.CanvasManager.getInstance();
            cursorPositon = new Point(0, canvas.Height + 1);

            executor = new CommandExecutor();
            commandDictionary = new CommandDictionary(executor);
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
                        ExecuteCommand(command, args);
                    }
                }
                else
                {
                    Console.WriteLine("Неверная команда");
                }
                Console.SetCursorPosition(0, cursorPos + 1);
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

        public void ExecuteCommand(string command, int[]? args = null)
        {
            var (action, error) = commandDictionary.GetFunction(command);

            try
            {
                switch (action)
                {
                    case Func<int, int, int, bool> threeParam when args?.Length == 3:
                        if (!threeParam(args[0], args[1], args[2]))
                            Console.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Func<int, int, int, int, bool> fourParam when args?.Length == 4:
                        if (!fourParam(args[0], args[1], args[2], args[3]))
                            Console.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Func<int, int, int, int, int, bool> fiveParam when args?.Length == 5:
                        if (!fiveParam(args[0], args[1], args[2], args[3], args[4]))
                            Console.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Action noParam when args == null:
                        noParam();
                        break;

                    default:
                        Console.WriteLine("Ошибка: неверное количество аргументов");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка выполнения команды: {ex.Message}");
            }
        }

    }
}
