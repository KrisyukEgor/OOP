
namespace OOP_1__console_paint_.Canvas
{
    public class Point
    {
       int _x, _y;
        
        public Point(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int x
        {
            get{ return _x; }
            set
            {
                _x = value;
            }
        }

        public int y
        {
            get { return _y; }
            set
            {
                _y = value;
            }
        }

    }
}
