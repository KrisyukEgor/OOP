
using OOP_1__console_paint_.Canvas;

namespace OOP_1__console_paint_.Comands
{
    public class ComandManager
    {
        private int comandCursorHeight;
        private int comandCursorWidth;

        Dictionary<string, Action> comandsDictionary;
        CanvasManager canvas;
        public ComandManager()
        {
            comandsDictionary = new Dictionary<string, Action>()
            {
                
            };
            canvas = CanvasManager.getInstance();
            comandCursorHeight = canvas.Height + 1;
            comandCursorWidth = 0;
        }
        public void Start()
        {
            Console.SetCursorPosition(comandCursorWidth, comandCursorHeight);
            Console.WriteLine("Введите операцию (все команды /help)");

            string? str = Console.ReadLine();
            Console.WriteLine(str);
        }
        
    }
}
