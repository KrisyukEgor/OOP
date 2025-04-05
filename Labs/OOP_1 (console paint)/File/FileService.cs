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

                    string[] parts = line.Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        continue;

                    string shapeType = parts[0].Trim();
                    string rest = parts[1].Trim();

                    int bgIndex = rest.IndexOf("bgColor:");
                    if (bgIndex == -1)
                        continue; 

                    string numericPart = rest.Substring(0, bgIndex).Trim().TrimEnd(',');

                    string bgPart = rest.Substring(bgIndex + "bgColor:".Length).Trim();
                    if (bgPart.EndsWith(";"))
                        bgPart = bgPart.Substring(0, bgPart.Length - 1).Trim();

                    string[] numTokens = numericPart.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numTokens.Length == 0)
                        continue;

                    int[] parameters = numTokens
                        .Select(p => int.TryParse(p.Trim(), out int value) ? value : -1)
                        .ToArray();

                    if (parameters.Contains(-1))
                        continue;
                    char bgColor = string.IsNullOrEmpty(bgPart) ? ' ' : bgPart[0];

                    IShape shape = canvasManager.CreateShape(parameters, bgColor);
                    shapes.Add(shape);
                }
            }

            terminal.WriteLine("Фигуры успешно загружены");
            return shapes;
        }



    }
}
