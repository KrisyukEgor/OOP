
using OOP_1__console_paint_.TerminalDir;
using System.Runtime.CompilerServices;

namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandManager
    {
        private readonly CommandExecutor executor;
        private readonly Terminal terminal;

        public CommandManager()
        {
            terminal = Terminal.getInstance();
            executor = new CommandExecutor();
        }

        public void Start()
        {
            terminal.WriteLine("Введите операцию (все команды начинаются с /)");
            string? input;

            while (true)
            {
                input = terminal.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    terminal.WriteLine("Команда не может быть пустой.");
                    continue;
                }

                var parsedCommand = TerminalParser.ParseCommand(input);

                if (parsedCommand.Command != null)
                {
                    if (parsedCommand.IntArgs != null && parsedCommand.IntArgs.Contains(-1))
                    {
                        terminal.WriteLine("Введите натуральные значения.");
                    }
                    else
                    {
                        if(parsedCommand.StrArgs != null)
                        {
                            executor.ExecuteCommand(parsedCommand.Command, parsedCommand.StrArgs);
                        }
                        else
                        {
                            executor.ExecuteCommand(parsedCommand.Command, parsedCommand.IntArgs);
                        }
                        
                    }
                }
                else
                {
                    terminal.WriteLine("Неверная команда.");
                }
            }
        }
    }

}
