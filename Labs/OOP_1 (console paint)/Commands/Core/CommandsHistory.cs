
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandHistory
    {
        private readonly Stack<ICommand> undoStack;
        private readonly Stack<ICommand> redoStack;
        private readonly Terminal terminal;

        public CommandHistory()
        {
            terminal = Terminal.getInstance();
            undoStack = new Stack<ICommand>();
            redoStack = new Stack<ICommand>();
        }

        public void AddToHistory(ICommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                var lastAction = undoStack.Pop();
                redoStack.Push(lastAction);
                lastAction.UnExecute();
            }
            else
            {
                terminal.WriteLine("Нет действий для отмены.");
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                var lastUndoneAction = redoStack.Pop();
                undoStack.Push(lastUndoneAction);
                lastUndoneAction.Execute();
            }
            else
            {
                terminal.WriteLine("Нет действий для повтора.");
            }
        }
    }

}
