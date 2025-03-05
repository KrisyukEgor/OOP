using OOP_1__console_paint_.Canvas.Managers;
using OOP_1__console_paint_.Interfaces;
using OOP_1__console_paint_.TerminalDir;

namespace OOP_1__console_paint_.File
{
    public class FileService
    {
        Terminal terminal;
        CanvasManager canvasManager;
        public FileService() 
        {
            terminal = Terminal.getInstance();
            canvasManager = CanvasManager.getInstance();
        }

        public void Save(string path, List<IShape> allShapes)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var shape in allShapes)
                    {
                        writer.WriteLine(shape.ToString());
                    }
                }
                terminal.WriteLine("Фигуры успешно сохранены");
            }
            catch (Exception ex)
            {
                terminal.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        public List<IShape> Load(string path)
        {
            List<IShape> shapes = new List<IShape>();

            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        continue;

                    string shapeType = parts[0].Trim();
                    string[] tokens = parts[1].Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (tokens.Length < 2)
                        continue;

                    string symbolToken = tokens[tokens.Length - 1].Trim();
                    if (string.IsNullOrEmpty(symbolToken))
                    {
                        symbolToken = " ";
                    }

                    int[] parameters = tokens
                        .Take(tokens.Length - 1)
                        .Select(p => int.TryParse(p.Trim(), out int value) ? value : -1)
                        .ToArray();

                    if (parameters.Contains(-1))
                        continue;

                    IShape shape = canvasManager.CreateShape(parameters, symbolToken[0]);
                    shapes.Add(shape);
                }
            }

            terminal.WriteLine("Фигуры успешно загружены");
            return shapes;
        }


    }
}
