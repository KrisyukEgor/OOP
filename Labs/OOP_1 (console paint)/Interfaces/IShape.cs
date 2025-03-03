using OOP_1__console_paint_.Canvas.Shapes;

namespace OOP_1__console_paint_.Interfaces
{
    public interface IShape
    {
        Point GetCenter();
        List<Point> GetVertexPoints();
        List<Point> GetAllSidesPoints();
        List<Point> GetPointsInside();
        bool IsContainPoint(Point p);
        int[] GetParameters();
        char BackgroundSymbol { get; set; }
    }
}
