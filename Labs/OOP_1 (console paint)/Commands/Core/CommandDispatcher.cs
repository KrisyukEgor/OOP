
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandDispatcher
    {
        private readonly Dictionary<string, Func<int[], ICommand>> intCommands = new();
        private readonly Dictionary<string, Func<string[], ICommand>> stringCommands = new();

        public void RegisterCommand(string name, Func<int[], ICommand> func)
        {
            intCommands[name] = func;
        }

        public void RegisterStringCommand(string name, Func<string[], ICommand> func)
        {
            stringCommands[name] = func;
        }

        public (ICommand?, string?) GetCommand(string name, int[] args)
        {
            if (intCommands.TryGetValue(name, out var commandFactory))
            {
                try
                {
                    return (commandFactory(args), null);
                }
                catch (Exception ex)
                {
                    return (null, $"Ошибка при создании команды {name}: {ex.Message}");
                }
            }

            return (null, $"Команда {name} не найдена.");
        }

        public (ICommand?, string?) GetCommand(string name, string[] args)
        {
            if (stringCommands.TryGetValue(name, out var commandFactory))
            {
                try
                {
                    return (commandFactory(args), null);
                }
                catch (Exception ex)
                {
                    return (null, $"Ошибка при создании команды {name}: {ex.Message}");
                }
            }

            return (null, $"Команда {name} не найдена.");
        }
    }


}
