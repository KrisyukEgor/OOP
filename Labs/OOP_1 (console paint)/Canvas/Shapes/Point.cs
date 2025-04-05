namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Point
    {
        int _x, _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int x
        {
            get { return _x; }
            set { _x = value; }
        }

        public int y
        {
            get { return _y; }
            set { _y = value; }
        }

        public override bool Equals(object? obj)
        {
            return obj is Point p && p.x == x && p.y == y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

    }
}
