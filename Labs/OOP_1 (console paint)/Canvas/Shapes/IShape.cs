
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public interface IShape
    {
        Point GetCenter();
        List<Point> GetVertexPoints();
        List<Point> GetAllPoints();
        bool IsContainPoint(Point p);
    }
}
