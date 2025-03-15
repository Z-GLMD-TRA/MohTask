using MohTask.DataTransferObject.Request;
using MohTask.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohTask.Presentation
{
    public class AccountManagement
    {
        private readonly IUserService _userService;
        private bool _isLoggedIn = false;
        private Guid _loggedInUserId;
        InputAndOutputOperations inputOutputOperation = new InputAndOutputOperations();


        public AccountManagement(IUserService userService)
        {
            _userService = userService;
        }


        public async Task RegisterUser(Dictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("username") || !parameters.ContainsKey("password"))
            {

                inputOutputOperation.PrintMessage("Register failed! Missing parameters.", ConsoleColor.Yellow);
                return;
            }

            var command = new UserCommand
            {
                UserName = parameters["username"],
                Password = parameters["password"]
            };

            var success = await _userService.RegisterUser(command);
            if (success)
                inputOutputOperation.PrintMessage("Register successful!", ConsoleColor.Green);
            else
                inputOutputOperation.PrintMessage("Register failed! Username already exists.", ConsoleColor.Red);
        }

        public async Task LoginUser(Dictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("username") || !parameters.ContainsKey("password"))
            {
                inputOutputOperation.PrintMessage("Login failed! Missing parameters.", ConsoleColor.Yellow);
                return;
            }

            var command = new UserCommand
            {
                UserName = parameters["username"],
                Password = parameters["password"]
            };

            var user = await _userService.AuthenticateUser(command);
            if (user != null)
            {
                _isLoggedIn = true;
                _loggedInUserId = user.Id;
                inputOutputOperation.PrintMessage($"Login successful! Welcome, {user.Username}", ConsoleColor.Green);
            }
            else
            {
                inputOutputOperation.PrintMessage("Login failed! Invalid credentials.", ConsoleColor.Red);
            }
        }

        public async Task ChangeStatus(Dictionary<string, string> parameters)
        {
            if (!_isLoggedIn)
            {
                inputOutputOperation.PrintMessage("Error! You must log in first.", ConsoleColor.Yellow);
                return;
            }

            if (!parameters.ContainsKey("status"))
            {
                inputOutputOperation.PrintMessage("Change status failed! Missing parameters.", ConsoleColor.Yellow);
                return;
            }

            bool status = parameters["status"].ToLower() == "available";
            var success = await _userService.ChangeUserStatus(_loggedInUserId, status);
            inputOutputOperation.PrintMessage(success ? "Status updated successfully!" : "Failed to update status.", success ? ConsoleColor.Green : ConsoleColor.Red);
        }

        public async Task SearchUsers(Dictionary<string, string> parameters)
        {
            if (!_isLoggedIn)
            {
                inputOutputOperation.PrintMessage("Error! You must log in first.", ConsoleColor.Yellow);
                return;
            }
            if (!parameters.ContainsKey("username"))
            {
                inputOutputOperation.PrintMessage("Search failed! Missing parameters.", ConsoleColor.Yellow);
                return;
            }

            var users = await _userService.SearchUsers(parameters["username"]);
            if (users.Count == 0)
            {
                inputOutputOperation.PrintMessage("No users found.", ConsoleColor.Red);
                return;
            }

            inputOutputOperation.PrintMessage("Search Results:", ConsoleColor.Cyan);
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} | Status: {(user.Status ? "Available" : "Not Available")}");
            }
        }

        public async Task ChangePassword(Dictionary<string, string> parameters)
        {
            if (!_isLoggedIn)
            {
                inputOutputOperation.PrintMessage("Error! You must log in first.", ConsoleColor.Yellow);
                return;
            }

            if (!parameters.ContainsKey("old") || !parameters.ContainsKey("new"))
            {
                inputOutputOperation.PrintMessage("Change password failed! Missing parameters.", ConsoleColor.Yellow);
                return;
            }

            var success = await _userService.ChangePassword(_loggedInUserId, parameters["old"], parameters["new"]);
            inputOutputOperation.PrintMessage(success ? "Password changed successfully!" : "Failed to change password.", success ? ConsoleColor.Green : ConsoleColor.Red);
        }

        public void Logout()
        {
            if (!_isLoggedIn)
            {
                inputOutputOperation.PrintMessage("Error! You are not logged in.", ConsoleColor.Yellow);
                return;
            }

            _isLoggedIn = false;
            _loggedInUserId = Guid.Empty;
            inputOutputOperation.PrintMessage("Logout successful!", ConsoleColor.Green);
        }
    }

}
