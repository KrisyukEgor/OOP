namespace OOP_1__console_paint_.Canvas.Managers
{
    public class CanvasTransformer
    {
        public CanvasTransformer() { }

        public (int, int) GetScaledPoint(int x, int y)
        {
            return (x * 2, y);
        }
        public (int, int) GetUnscaledPoint(int x, int y)
        {
            return (x / 2, y);
        }
    }
}
