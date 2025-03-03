
using OOP_1__console_paint_.Canvas;
using System.Numerics;

namespace OOP_1__console_paint_.Comands
{
    public class CommandDictionary
    {
        private readonly Dictionary<string, Delegate> _dictionary;
        public CommandDictionary(CommandExecutor executor)
        {

            _dictionary = new Dictionary<string, Delegate> {
                { "/drawcircle", (Func<int, int, int, bool>)((x, y, r) => executor.DrawCircle(x, y ,r))},
                { "/drawsquare", (Func<int, int, int, bool>)((x, y, length) => executor.DrawRectangle(x, y, length, length))},
                { "/drawrect", (Func<int, int, int, int, bool>)((x, y, w, h) => executor.DrawRectangle(x, y, w, h))},
                { "/drawtriangle", (Func<int, int, int, int, int, bool>)((x, y, ls, bs, rs) => executor.DrawTriangle(x, y, ls, bs, rs)) },

                { "/erase", (Action)(() => executor.Erase())},
                { "/move", (Action)(() => executor.Move())},
                 { "/setbgcolor", (Action)(() => executor.SetBgColor())},
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
