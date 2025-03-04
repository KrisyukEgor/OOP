using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;
using System.Diagnostics;

namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandManager
    {
        CommandExecutor executor;

        Terminal terminal;
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

                var (command, args) = TerminalParser.ParseCommand(input);

                if (command != null)
                {
                    if (args != null && args.Contains(-1))
                    {
                        terminal.WriteLine("Введите натуральные значения");
                    }
                    else
                    {
                        executor.ExecuteCommand(command, args);
                    }
                }
                else
                {
                    terminal.WriteLine("Неверная команда");
                }
            }
        }

    }
}
