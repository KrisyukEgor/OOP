
namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class Square
    {
        int xCenter;
        int yCenter;
        int length;

        public Square(int xCenter, int yCenter, int length)
        {
            this.xCenter = xCenter;
            this.yCenter = yCenter;
            this.length = length;
        }

        public List<Point> GetVertexPoints()
        {
            List<Point> pointsList = new List<Point> ();

            int topLeftX = xCenter - (int)(length / 2);
            int topLeftY = yCenter - (int)(length / 2);

            pointsList.Add(new Point(xCenter, yCenter));
            pointsList.Add(new Point(topLeftX, topLeftY));
            pointsList.Add(new Point(topLeftX + length, topLeftY));
            pointsList.Add(new Point(topLeftX, topLeftY + length));
            pointsList.Add(new Point(topLeftX + length, topLeftY + length));
            return pointsList;
        }
    }
}
