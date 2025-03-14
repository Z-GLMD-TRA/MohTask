
namespace MohTask.Presentation
{
    public class InputAndOutputOperations
    {
        /// <summary>
        /// Parses command parameters into a dictionary.
        /// </summary>
        public Dictionary<string, string> ParseParameters(string[] args)
        {
            var parameters = new Dictionary<string, string>();
            for (int i = 1; i < args.Length; i++)
            {
                var parts = args[i].Split(" ", 2);
                if (parts.Length == 2)
                    parameters[parts[0].ToLower()] = parts[1].Trim();
            }
            return parameters;
        }

        /// <summary>
        /// Prints a message with a specific color.
        /// </summary>
        public void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor(); // Reset color after message
        }
    }
}
