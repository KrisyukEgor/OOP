using OOP_1__console_paint_.Canvas;

namespace OOP_1__console_paint_.Interfaces
{
    public interface IShape
    {
        Point GetCenter();
        List<Point> GetVertexPoints();
        List<Point> GetAllPoints();
        bool IsContainPoint(Point p);
    }
}
