using OOP_1__console_paint_.Canvas;
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.Comands
{
    public class CommandExecutor
    {
        CanvasManager canvas;
        CanvasTransformer canvasTransformer;
        Terminal terminal;
        UserInputHandler userInputHandler;
        
        public CommandExecutor()
        {
            canvas = CanvasManager.getInstance();
            terminal = Terminal.getInstance();
            canvasTransformer = new CanvasTransformer();
            userInputHandler = new UserInputHandler();
        }
        public bool DrawCircle(int x, int y, int r)
        {
            if(canvas.DrawCircle(x, y, r))
            {
                return true;
            }
            return false;
        }

        public bool DrawRectangle(int x, int y, int width, int height)
        {
            if (canvas.DrawRectangle(x, y, width, height))
            {
                return true;
            }
            return false;
        }

        public bool DrawTriangle(int x, int y, int leftSide, int baseSide, int rightSide)
        {
            if (canvas.DrawTriangle(x, y, leftSide, baseSide, rightSide))
            {
                return true;
            }
            return false;
        }

        public void Erase()
        {
            Point point = new Point(10, 10);

            while (point.x != -1 && point.y != -1)
            {
                point = userInputHandler.ChoosePoint();
                IShape? shape = userInputHandler.ChooseShape(point);

                if (shape != null)
                { 
                    canvas.Erase(shape);
                }
            }
        }

        public void Move()
        {
            IShape? shape = null;
            while(shape == null)
            {
                Point point = userInputHandler.ChoosePoint();

                if (point.x == -1 && point.y == -1)
                {
                    return;
                }
                shape = userInputHandler.ChooseShape(point);
            }
            
            StartMove(shape);
        }

        public void SetBgColor()
        {
            IShape? shape = null;
            while (shape == null)
            {
                Point point = userInputHandler.ChoosePoint();

                if (point.x == -1 && point.y == -1)
                {
                    return;
                }
                shape = userInputHandler.ChooseShape(point);
            }

            terminal.WriteLine("Введите символ");
            char symbol = terminal.ReadLine()[0];

            canvas.SetShapeBackground(shape, symbol);

        }

        private void StartMove(IShape shape)
        {
            ConsoleKey key;
            IShape? movingShape = shape;
            
            do
            {
                var (cursorX, cursorY) = canvasTransformer.GetScaledPoint(movingShape.GetCenter().x, movingShape.GetCenter().y);
                Console.SetCursorPosition(cursorX, cursorY);
                key = Console.ReadKey(true).Key;
                
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        movingShape = canvas.MoveLeft(movingShape);
                        break;
                    case ConsoleKey.RightArrow:
                        movingShape = canvas.MoveRight(movingShape);
                        break;
                    case ConsoleKey.UpArrow:
                        movingShape = canvas.MoveUp(movingShape);
                        break;
                    case ConsoleKey.DownArrow:
                        movingShape = canvas.MoveDown(movingShape);
                        break;

                }

                
            } while (key != ConsoleKey.Escape);

        }

        public void WriteHelp()
        {
            terminal.WriteLine("\n===================================\n Команды \n");
            terminal.WriteLine("/drawSquare");
            terminal.WriteLine("/drawTriangle");
            terminal.WriteLine("/drawRect");
            terminal.WriteLine("/drawCircle");

            terminal.WriteLine("\n/cls: Очищает командную строку");
            terminal.WriteLine("/exit: Выход");

            terminal.WriteLine("\n===================================");
        }
        public void Exit()
        {
            terminal.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }
        public void Undo()
        {

        }

        public void Redo()
        {

        }
    }
}
