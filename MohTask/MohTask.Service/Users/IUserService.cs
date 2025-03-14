
using MohTask.DataTransferObject.Request;
using MohTask.DataTransferObject.Response;

namespace MohTask.Service.Users
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserCommand command);
        Task<UserResult?> AuthenticateUser(UserCommand command);
        Task<bool> ChangeUserStatus(Guid userId, bool status);
        Task<List<UserResult>> SearchUsers(string usernamePrefix);
        Task<bool> ChangePassword(Guid userId, string oldPassword, string newPassword);
        Task<bool> Logout();
    }
}
 