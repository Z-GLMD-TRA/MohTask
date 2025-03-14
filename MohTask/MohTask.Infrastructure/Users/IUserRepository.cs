using MohTask.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohTask.Infrastructure.Users
{
    //CRUD operation and other method we need in user repository
    public interface IUserRepository
    {
        Task<bool> AddAsync(User entity);
        Task<User?> GetAsync(Guid id);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<List<User>> SearchByUsernamePrefixAsync(string usernamePrefix);
    }
}
