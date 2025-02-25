namespace OOP_1__console_paint_.TerminalDir
{
    public class TerminalParser
    {
        public static (string?, int[]?) ParseCommand(string? inputCommand)
        {

            string? command = DetectComand(ref inputCommand);
            int[]? args = GetArgs(inputCommand);

            return (command, args);
        }

        private static string? DetectComand(ref string input)
        {
            string? result = "";
            int slashIndex = input.IndexOf('/');

            if (slashIndex == -1) { return null; }

            int spaceIndex = input.IndexOf(" ");

            if (spaceIndex == -1)
            {
                result = input;
                input = "";
            }
            else
            {
                result = input.Substring(0, spaceIndex);
                input = input.Substring(spaceIndex + 1);
            }
            return result;
        }

        public static int ParseStringToInt(string str)
        {
            int result = -1;

            if (!int.TryParse(str, out result)) { return -1; };

            return result;
        }
        private static int[]? GetArgs(string input)
        {
            int[]? args = null;
            int semicolonIndex = input.IndexOf(";");

            if (semicolonIndex == -1) { return null; }

            string[] parts = input.Split(';');

            if (parts.Length == 2)
            {
                string[] values = parts[1].Trim().Split(",");
                args = new int[2 + values.Length];
                string[] coordinates = parts[0].Split(',');

                if (coordinates.Length == 2)
                {
                    args[0] = ParseStringToInt(coordinates[0]);
                    args[1] = ParseStringToInt(coordinates[1]);

                }
                else
                {
                    return null;
                }

                for (int i = 0; i < values.Length; ++i)
                {
                    args[2 + i] = ParseStringToInt(values[i]);
                }
            }
            else
            {
                return null;
            }

            return args;
        }
    }
}
