
using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandDispatcher
    {
        private readonly Dictionary<string, Func<int[], ICommand>> commands = new();

        public void RegisterCommand(string name, Func<int[], ICommand> commandFactory)
        {
            commands[name] = commandFactory;
        }

        public (ICommand?, string?) GetCommand(string name, int[] args)
        {
            if (commands.TryGetValue(name, out var commandFactory))
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
            else
            {
                return (null, $"Команда {name} не найдена.");
            }

            return (null, null);
        }

    }

}
