using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Comands
{
    public class CommandManager
    { 
        CommandDictionary commandDictionary;
        Canvas.CanvasManager canvas;
        CommandExecutor executor;
        
        Terminal terminal;
        public CommandManager()
        {
            canvas = Canvas.CanvasManager.getInstance();
            executor = new CommandExecutor();
            commandDictionary = new CommandDictionary(executor);
            terminal = Terminal.getInstance();
        }
        public void Start()
        {
            terminal.WriteLine("Введите операцию (все команды /help)");
            string? input;
             
            while(true)
            {
                input = terminal.ReadLine()?.Trim().ToLower();

                var (command, args) = TerminalParser.ParseCommand(input);

                if (command != null)
                {

                    if (args!= null && args.Contains(-1))
                    {
                        terminal.WriteLine("Введите натуральные значения");
                    }
                    else
                    {
                        ExecuteCommand(command, args);
                    }
                }
                else
                {
                    terminal.WriteLine("Неверная команда");
                }
            }
        }

        public void ExecuteCommand(string command, int[]? args = null)
        {
            var (action, error) = commandDictionary.GetFunction(command);
            if (error != null)
            {
                terminal.WriteLine($"Ошибка: {error}");
                return;
            }

            try
            {
                switch (action)
                {
                    case Func<int, int, int, bool> threeParam when args?.Length == 3:
                        if (!threeParam(args[0], args[1], args[2]))
                            terminal.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Func<int, int, int, int, bool> fourParam when args?.Length == 4:
                        if (!fourParam(args[0], args[1], args[2], args[3]))
                            terminal.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Func<int, int, int, int, int, bool> fiveParam when args?.Length == 5:
                        if (!fiveParam(args[0], args[1], args[2], args[3], args[4]))
                            terminal.WriteLine("Ошибка: фигура выходит за границы холста");
                        break;

                    case Action noParam when args == null:
                        noParam();
                        break;

                    default:
                        terminal.WriteLine("Ошибка: неверное количество аргументов");
                        break;
                }
            }
            catch (Exception ex)
            {
                terminal.WriteLine($"Ошибка выполнения команды: {ex.Message}");
            }
        }

    }
}
