using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Canvas.Shapes;
using OOP_1__console_paint_.Commands.Drawing;
using OOP_1__console_paint_.Commands.Movement;
using OOP_1__console_paint_.Commands.Painting;
using OOP_1__console_paint_.File;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;


namespace OOP_1__console_paint_.Commands.Core
{
    public class CommandExecutor
    {

        CanvasTransformer canvasTransformer;
        Terminal terminal;
        UserInputHandler userInputHandler;
        CommandHistory history;
        CommandDispatcher dispatcher;
        CanvasManager canvasManager;
        FileService fileService;
        public CommandExecutor()
        {

            terminal = Terminal.getInstance();
            canvasTransformer = new CanvasTransformer();
            userInputHandler = new UserInputHandler();
            history = new CommandHistory();
            dispatcher = new CommandDispatcher();
            canvasManager = CanvasManager.getInstance();
            fileService = new FileService();
            RegistrateCommands();
        }

        private void RegistrateCommands()
        {
            dispatcher.RegisterCommand("/drawcircle", args => DrawCircle(args[0], args[1], args[2]));
            dispatcher.RegisterCommand("/drawsquare", args => DrawRectangle(args[0], args[1], args[2], args[2]));
            dispatcher.RegisterCommand("/drawrect", args => DrawRectangle(args[0], args[1], args[2], args[3]));
            dispatcher.RegisterCommand("/drawtriangle", args => DrawTriangle(args[0], args[1], args[2], args[3], args[4]));

            dispatcher.RegisterCommand("/erase", args => Erase());
            dispatcher.RegisterCommand("/move", args => Move());
            dispatcher.RegisterCommand("/setbgcolor", args => SetBgColor());

            dispatcher.RegisterCommand("/help", args => WriteHelp());
            dispatcher.RegisterCommand("/exit", args => { Exit(); return null; });
            dispatcher.RegisterCommand("/undo", args => Undo());
            dispatcher.RegisterCommand("/redo", args => Redo());
            dispatcher.RegisterCommand("/cls", args => ClearTerminal());

            dispatcher.RegisterStringCommand("/save", args => Save(args[0]));
            dispatcher.RegisterStringCommand("/load", args => Load(args[0]));
        }

        public ICommand DrawCircle(int x, int y, int r)
        {
            ICommand drawCircleCommand = new DrawCircleCommand(x, y, r);
            return drawCircleCommand;
        }

        public ICommand DrawRectangle(int x, int y, int width, int height)
        {
            ICommand drawRectCommand = new DrawRectangleCommand(x, y, width, height);
            return drawRectCommand;
        }

        public ICommand DrawTriangle(int x, int y, int leftSide, int baseSide, int rightSide)
        {
            ICommand drawTriangeCommand = new DrawTriangleCommand(x, y, leftSide, baseSide, rightSide);
            return drawTriangeCommand;
        }

        public ICommand Erase()
        {
            Point point = new Point(10, 10);

            while (point.x != -1 && point.y != -1)
            {
                point = userInputHandler.ChoosePoint();
                IShape? shape = userInputHandler.ChooseShape(point);

                if (shape != null)
                {
                    ICommand eraseCommand = new EraseShapeCommand(shape);
                    return eraseCommand;
                }
            }

            return null;
        }

        public ICommand Move()
        {
            IShape? shape = null;
            while (shape == null)
            {
                Point point = userInputHandler.ChoosePoint();

                if (point.x == -1 && point.y == -1)
                {
                    return new NoParamCommand();
                }
                shape = userInputHandler.ChooseShape(point);
            }

            StartMove(shape);
            return new NoParamCommand();
        }

        private void StartMove(IShape shape)
        {
            ConsoleKey key;
            ICommand? moveCommand = null;

            do
            {
                var (cursorX, cursorY) = canvasTransformer.GetScaledPoint(shape.GetCenter().x, shape.GetCenter().y);
                Console.SetCursorPosition(cursorX, cursorY);
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        moveCommand = new MoveLeftCommand(shape);
                        break;
                    case ConsoleKey.RightArrow:
                        moveCommand = new MoveRightCommand(shape);
                        break;
                    case ConsoleKey.UpArrow:
                        moveCommand = new MoveUpCommand(shape);
                        break;
                    case ConsoleKey.DownArrow:
                        moveCommand= new MoveDownCommand(shape);
                        break;

                }

                if(moveCommand != null && key != ConsoleKey.Escape)
                {
                    moveCommand.Execute();
                    history.AddToHistory(moveCommand);
                }


            } while (key != ConsoleKey.Escape);

        }

        public ICommand SetBgColor()
        {
            IShape? shape = null;
            while (shape == null)
            {
                Point point = userInputHandler.ChoosePoint();

                if (point.x == -1 && point.y == -1)
                {
                    return new NoParamCommand();
                }
                shape = userInputHandler.ChooseShape(point);
                
            }

            terminal.WriteLine("Введите символ");
            char symbol = terminal.ReadLine()[0];

            return new SetBgColorCommand(shape, symbol);
        }

        public void Exit()
        {
            terminal.WriteLine("Выход из программы...");
            Environment.Exit(0);
        }

        public void ExecuteCommand(string cmd, int[]? args)
        {
            var (command, errorMessage) = dispatcher.GetCommand(cmd, args ?? Array.Empty<int>());

            if (errorMessage != null)
            {
                terminal.WriteLine(errorMessage);
                return;
            }

            if (command == null)
            {

                return;
            }

            try
            {
                command.Execute();
                if (command is not NoParamCommand)
                {
                    history.AddToHistory(command);
                }
                
            }
            catch (Exception ex)
            {
                terminal.WriteLine($"Ошибка при выполнении команды: {ex.Message}");
            }
        }

        public void ExecuteCommand(string cmd, string[]? args)
        {
            var (command, errorMessage) = dispatcher.GetCommand(cmd, args ?? Array.Empty<string>());

            if (errorMessage != null)
            {
                terminal.WriteLine(errorMessage);
                return;
            }

            if (command == null)
            {
                return;
            }

            try
            {
                command.Execute();
                if (command is not NoParamCommand)
                {
                    history.AddToHistory(command);
                }
                
            }
            catch (Exception ex)
            {
                terminal.WriteLine($"Ошибка при выполнении команды: {ex.Message}");
            }
        }

        public ICommand Undo()
        {
            history.Undo();
            return new NoParamCommand();
        }

        public ICommand Redo()
        {
            history.Redo();
            return new NoParamCommand();
        }
        public ICommand ClearTerminal()
        {
            terminal.Clear();
            return new NoParamCommand();
        }

        public ICommand Save(string path)
        {

            var allShapes = canvasManager.GetAllShapes();
            fileService.Save(path, allShapes);
            return new NoParamCommand();
        }

        public ICommand Load(string path)
        {
            var allShapes = fileService.Load(path);

            foreach(var shape in allShapes )
            {
                canvasManager.DetectAndDrawShape(shape);
            }
            return new NoParamCommand();
        }

        public ICommand WriteHelp()
        {
            terminal.WriteLine("\n===================================\n");
            terminal.WriteLine("Команды с параметрами\n");
            terminal.WriteLine("/drawSquare *параметры* : создание квадрата (необходимы точка верхней левой вершины и длина стороны)");
            terminal.WriteLine("/drawTriangle *параметры* : создание треугольника (необходимы точка верхней вершины и длины трёх сторон)");
            terminal.WriteLine("/drawRect *параметры* : создание прямоугольника (необходимы точка верхней левой вершины, длина и ширина)");
            terminal.WriteLine("/drawCircle *параметры* : создание круга (необходимы точка центра и радиус)");
            terminal.WriteLine();
            terminal.WriteLine("\t\tПримеры \n");
            terminal.WriteLine("/drawSquare 10, 10; 3");
            terminal.WriteLine("/drawTriangle 10, 10; 3, 4, 5");
            terminal.WriteLine();
            terminal.WriteLine("\n===================================\n");
            terminal.WriteLine("Команды, управляемые стрелочками\n");
            terminal.WriteLine("/erase : удаление фигуры");
            terminal.WriteLine("/move : перемещение фигуры");
            terminal.WriteLine("/setBgColor : изменение фона фигуры");
            terminal.WriteLine();
            terminal.WriteLine("Для выхода из команды нажмите ESC");

            terminal.WriteLine("\n===================================\n");
            terminal.WriteLine("Команды работы с файлами\n");
            terminal.WriteLine("/save *путь до файла.txt в \"\"*: сохранение фигур в файл");
            terminal.WriteLine("/load *путь до файла.txt в \"\"*: загрузка фигур из файла");
            terminal.WriteLine();
            terminal.WriteLine("\t\tПримеры \n");
            terminal.WriteLine("/save \"example.txt\"");
            terminal.WriteLine("/load \"example.txt\"");

            terminal.WriteLine("\n===================================\n");
            terminal.WriteLine("Команды без параметров\n");
            terminal.WriteLine("/undo : отмена последнего действия");
            terminal.WriteLine("/redo : отмена undo действия");
            terminal.WriteLine("/cls : очистка терминала");
            terminal.WriteLine("/exit : выход из программы");
            terminal.WriteLine("\n===================================");

            return new NoParamCommand();
        }
    }
}
