using Microsoft.Extensions.DependencyInjection;

namespace MohTask.Presentation
{
    class Program
    {
        static async Task Main()
        {
            //add configurations
            var serviceProvider = ServiceConfiguration.ConfigureServices();

            // Resolve dependencies
            var accountManager = serviceProvider.GetRequiredService<AccountManagement>();
            var inputOutputOperations = serviceProvider.GetRequiredService<InputAndOutputOperations>();
            var commandHandler = new CommandHandler(accountManager, inputOutputOperations);

            inputOutputOperations.PrintMessage("Welcome to Mohaymen Task Console App!", ConsoleColor.Cyan);

            while (true)
            {
                Console.Write("\nEnter Command: ");
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                await commandHandler.HandleCommand(input);
            }
        }
    }
}

