using MohTask.DataTransferObject.Request;
using MohTask.DataTransferObject.Response;
using MohTask.Infrastructure.Users;
using MohTask.Model.Users;


namespace MohTask.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUser(UserCommand command)
        {
            try
            {
                var existingUser = await _userRepository.GetByUsernameAsync(command.UserName);
                if (existingUser != null) return false;

                var newUser = new User { Username = command.UserName, Password = command.Password };
                return await _userRepository.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<UserResult?> AuthenticateUser(UserCommand command)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(command.UserName);
                if (user != null && user.Password == command.Password)
                {
                    return new UserResult { Id = user.Id, Username = user.Username, Status = user.Status };
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> ChangeUserStatus(Guid userId, bool status)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                if (user == null) return false;

                user.Status = status;
                return await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UserResult>> SearchUsers(string usernamePrefix)
        {
            try
            {
                var users = await _userRepository.SearchByUsernamePrefixAsync(usernamePrefix);
                return users.Select(u => new UserResult { Id = u.Id, Username = u.Username, Status = u.Status }).ToList();
            }
            catch (Exception ex)
            {
                return new List<UserResult>();
            }
        }


        public async Task<bool> ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = await _userRepository.GetAsync(userId);
                if (user == null || user.Password != oldPassword) return false;

                user.Password = newPassword;
                return await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
