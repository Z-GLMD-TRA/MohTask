
namespace MohTask.Presentation
{
    public class CommandHandler
    {
        private readonly AccountManagement _accountManager;
        private readonly InputAndOutputOperations _inputOutputOperations;

        public CommandHandler(AccountManagement accountManager, InputAndOutputOperations inputOutputOperations)
        {
            _accountManager = accountManager;
            _inputOutputOperations = inputOutputOperations;
        }

        public async Task HandleCommand(string input)
        {
            var args = input.Split(" --", StringSplitOptions.RemoveEmptyEntries);
            var command = args[0].ToLower();
            var parameters = _inputOutputOperations.ParseParameters(args);

            switch (command)
            {
                case "register":
                    await _accountManager.RegisterUser(parameters);
                    break;
                case "login":
                    await _accountManager.LoginUser(parameters);
                    break;
                case "change":
                    await _accountManager.ChangeStatus(parameters);
                    break;
                case "search":
                    await _accountManager.SearchUsers(parameters);
                    break;
                case "changepassword":
                    await _accountManager.ChangePassword(parameters);
                    break;
                case "logout":
                    _accountManager.Logout();
                    break;
                default:
                    _inputOutputOperations.PrintMessage("Invalid command. Try again.", ConsoleColor.Red);
                    _inputOutputOperations.PrintMessage("\nAvailable Commands:\n", ConsoleColor.DarkMagenta);
                    Console.WriteLine("register --username [name] --password [pass]");
                    Console.WriteLine("login --username [name] --password [pass]");
                    Console.WriteLine("change --status [available/not available]");
                    Console.WriteLine("search --username [prefix]");
                    Console.WriteLine("changepassword --old [oldpass] --new [newpass]");
                    Console.WriteLine("logout");
                    break;
            }
        }
    }
}
