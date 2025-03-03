using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;
using System.ComponentModel.Design;
using System.Xml.XPath;

namespace OOP_1__console_paint_.Canvas.Shapes
{
    public class ShapeManager
    {
        List<IShape> allShapes;
        CanvasPainter painter;
        CanvasValidator validator;
        private static ShapeManager? instance = null;

        public static ShapeManager getInstance()
        {
            if (instance == null)
            {
                instance = new ShapeManager();
            }
            return instance;
        }
        private ShapeManager() 
        { 
            painter = new CanvasPainter();
            validator = new CanvasValidator();
            allShapes = new List<IShape>();
        }

        public Triangle CreateTriangeShape(int xTop, int yTop, int leftSideLength, int baseLength, int rightSideLength)
        {
            Triangle triangle = new Triangle(xTop, yTop, leftSideLength, baseLength, rightSideLength);
            allShapes.Add(triangle);
            return triangle;
        }
        public Rectangle CreateRectangle(int xTop, int yTop, int width, int height)
        {
            Rectangle rectangle = new Rectangle(xTop, yTop, width, height);

            allShapes.Add(rectangle);
            return rectangle;
        }
        public Circle CreateCircleShape(int xTop, int yTop, int radius)
        {
            Circle circle = new Circle(xTop, yTop, radius);
            allShapes.Add(circle);
            return circle;
        }

        public IShape CreateShape(int[] parameters, char symbol = ' ')
        {
            IShape shape = new Circle(1, 1, 1);
            if (parameters.Length == 3)
            {
                shape = new Circle(parameters[0], parameters[1], parameters[2]);
            }
            else if (parameters.Length == 4)
            { 
                shape = new Rectangle(parameters[0], parameters[1], parameters[2], parameters[3]);
            }
            else if(parameters.Length == 5)
            {
                shape = new Triangle(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);
            }

            shape.BackgroundSymbol = symbol;
            allShapes.Add(shape);
            return shape;

        }

        public List<IShape> GetShapesWhichContainPoint(Point erasePoint)
        {
            List<IShape> shapeList = new List<IShape>();

            foreach (IShape shape in allShapes)
            {
                if (shape.IsContainPoint(erasePoint))
                {
                    shapeList.Add(shape);
                }
            }

            return shapeList;
        }

        public void Erase(IShape? shape)
        {
            if (shape == null) return;

            List<Point> points = shape.GetPointsInside().ToList();

            points.AddRange(shape.GetAllSidesPoints());

            painter.ClearPoints(points);
            allShapes.Remove(shape);

            RedrawShapesAfterAction(shape);

        }

        private void RedrawShapesAfterAction(IShape shape)
        {
            List<Point> shapesSidesPoints = shape.GetAllSidesPoints();
            HashSet<IShape> shapesToRedraw = new HashSet<IShape>();

            foreach (IShape candidateShape in allShapes)
            {
                if (candidateShape != shape && shapesSidesPoints.Any(p => candidateShape.IsContainPoint(p)))
                {
                    shapesToRedraw.Add(candidateShape);
                }
            }
            foreach (IShape shapeToRedraw in shapesToRedraw)
            {
                DetectAndDrawShape(shapeToRedraw);
            }
        }

        private void DetectAndDrawShape(IShape shape)
        {
            if (shape is Circle circle)
            {
                painter.DrawCircle(circle);
            }
            else if (shape is Rectangle rectangle)
            {
                painter.DrawRectangle(rectangle);
            }
            else if (shape is Triangle triangle)
            {
                painter.DrawTriangle(triangle);
            }
        }

        public IShape MoveRight(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            parameters[0]++;
            char symbol = shape.BackgroundSymbol;
            IShape newShape = CreateShape(parameters, symbol);

            if (validator.CanDraw(newShape))
            { 
                Erase(shape);
                DetectAndDrawShape(newShape);
            }
            else
            {
                allShapes.Remove(newShape);
                return shape;
            }
            return allShapes.Last();
        }

        public IShape MoveLeft(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            parameters[0]--;
            char symbol = shape.BackgroundSymbol;
            IShape newShape = CreateShape(parameters, symbol);

            if (validator.CanDraw(newShape))
            {
                Erase(shape);
                DetectAndDrawShape(newShape);
            }
            else {
                allShapes.Remove(newShape);
                return shape;
            }
            return allShapes.Last();
        }

        public IShape MoveUp(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            parameters[1]--;
            char symbol = shape.BackgroundSymbol;
            IShape newShape = CreateShape(parameters, symbol);

            if (validator.CanDraw(newShape))
            {
                Erase(shape);
                DetectAndDrawShape(newShape);
            }
            else
            {
                allShapes.Remove(newShape);
                return shape;
            }
            return allShapes.Last();
        }

        public IShape MoveDown(IShape shape)
        {
            int[] parameters = shape.GetParameters();
            parameters[1]++;
            char symbol = shape.BackgroundSymbol;
            IShape newShape = CreateShape(parameters, symbol);

            if (validator.CanDraw(newShape))
            {
                Erase(shape);
                DetectAndDrawShape(newShape);
            }
            else
            {
                allShapes.Remove(newShape);
                return shape;
            }
            return allShapes.Last();
        }
    }
}
