using Microsoft.EntityFrameworkCore;
using MohTask.Model.Users;


namespace MohTask.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MohTaskDbContext _dbContext;

        public UserRepository(MohTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(User entity)
        {
            try
            {
                await _dbContext.Users.AddAsync(entity);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<User?> GetAsync(Guid id)
        {
            try
            {
                return await _dbContext.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            try
            {
                return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            try
            {
                _dbContext.Users.Update(entity);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null) return false;
                _dbContext.Users.Remove(user);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
        public async Task<List<User>> SearchByUsernamePrefixAsync(string usernamePrefix)
        {
            try
            {
                return await _dbContext.Users
                    .Where(u => u.Username.StartsWith(usernamePrefix))
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

    }

}
