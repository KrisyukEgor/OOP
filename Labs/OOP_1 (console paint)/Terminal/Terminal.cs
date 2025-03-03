namespace OOP_1__console_paint_.TerminalDir
{
    public class Terminal
    {
        private static Terminal? instance = null;
        int cursorY = 25;
        int cursorX = 0;
        public static Terminal getInstance()
        {
            if (instance == null)
            {
                instance = new Terminal();
            }
            return instance;
        }
        private Terminal() { }
        public void Write(string message)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.Write(message);

            cursorX += message.Length;

            int newLines = message.Split('\n').Length - 1;
            if (newLines > 0)
            {
                cursorY += newLines;
                cursorX = 0;
            }
        }

        public void WriteLine(string message)
        {
            Console.SetCursorPosition(cursorX, cursorY);
            Console.WriteLine(message);

            int newLines = message.Split('\n').Length;
            cursorY += newLines;
            cursorX = 0;
        }

        public void WriteLine()
        {
            Console.WriteLine();
            cursorY++;
            cursorX = 0;
        }

        public string? ReadLine()
        {
            Console.SetCursorPosition(cursorX, cursorY);
            cursorY++;
            cursorX = 0;
            return Console.ReadLine();
        }
    }
}
