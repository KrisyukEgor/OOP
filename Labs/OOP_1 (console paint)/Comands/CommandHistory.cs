using OOP_1__console_paint_.Interfaces;

namespace OOP_1__console_paint_.Comands
{
    public class CommandHistory
    {
        private Stack<ICommand> _undoStack;
        private Stack<ICommand> _redoStack;
        private CommandHistory()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
        }
        
        public void ExecuteCommand(ICommand cmd)
        {
            cmd.Execute();
            _undoStack.Push(cmd);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if(_undoStack.Count > 0 )
            {
                ICommand command = _undoStack.Pop();    
                command.UnExecute();
                _redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if(_redoStack.Count > 0 )
            {
                ICommand command = _redoStack.Pop(); 
                command.Execute();
                _undoStack.Push(command);
            }
        }
    }
}
