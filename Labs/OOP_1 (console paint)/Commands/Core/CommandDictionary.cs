using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Interfaces;
using System.Numerics;


namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandDictionary
    {
        private readonly Dictionary<string, Delegate> _dictionary;
        CommandExecutor executor;
        public CommandDictionary(CommandExecutor e)
        {
            executor = e;
            _dictionary = new Dictionary<string, Delegate> {
                { "/drawcircle", (Func<int, int, int, ICommand>) ((x, y, r) => executor.DrawCircle(x, y, r)) },
                { "/drawsquare", (Func<int, int, int, ICommand>)((x, y, length) => executor.DrawRectangle(x, y, length, length))},
                { "/drawrect", (Func<int, int, int, int, ICommand>)((x, y, w, h) => executor.DrawRectangle(x, y, w, h))},
                { "/drawtriangle", (Func<int, int, int, int, int, ICommand>)((x, y, ls, bs, rs) => executor.DrawTriangle(x, y, ls, bs, rs)) },

                { "/erase", () => executor.Erase()},
                { "/move", () => executor.Move()},
                // { "/setbgcolor", (Action)(() => executor.SetBgColor())},
                { "/help", (Action)(() => executor.WriteHelp()) },
                { "/exit", (Action)(() => executor.Exit()) },
                { "/undo", (Action)(() => executor.Undo()) },
                { "/redo", (Action)(() => executor.Redo()) }
            };
        }

        public (Delegate?, string?) GetFunction(string command)
        {
            if (!_dictionary.TryGetValue(command, out var action))
            {
                return (null, "Ошибка: неверная команда");
            }
            return (action, null);

        }
    }


}
