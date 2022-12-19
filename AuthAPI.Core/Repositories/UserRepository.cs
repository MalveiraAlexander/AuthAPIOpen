using AuthAPI.Commons.Data;
using AuthAPI.Commons.Interfaces.IRepository;
using AuthAPI.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDataContext _context;

        public UserRepository(AuthDataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return _context.Users.Include(u => u.Person).Include(u => u.Roles).ThenInclude(u => u.Permissions).Include(u => u.Devices).Where(u => u.DeleteAt == null).FirstOrDefault(u => u.Id == id);
        }

        public async Task<User?> GetUserByLoginInputAsync(string input)
        {
            return _context.Users.Include(u => u.Person).Include(u => u.Roles).ThenInclude(u => u.Permissions).Include(u => u.Devices).Where(u => u.DeleteAt == null).FirstOrDefault(u => u.Email == input || u.UserName == input || u.Phone == input);
        }


        public async Task<List<User>> GetUsersAsync(int lastId, int maxElements)
        {
            return _context.Users.Include(u => u.Person)
                                 .Include(u => u.Roles)
                                 .ThenInclude(u => u.Permissions)
                                 .Include(u => u.Devices)
                                 .Where(x => x.Id > lastId)
                                 .Take(maxElements).ToList();
        }

        public async Task<User?> GetUserByHashAsync(string hash)
        {
            return _context.Users.Include(u => u.Person).Include(u => u.Roles).Include(u => u.Devices).Where(x => x.NewUserHash == hash && x.DeleteAt == null).FirstOrDefault();
        }

        public async Task<User?> GetUserByTokenAsync(string token)
        {
            return _context.Users.Include(u => u.Person).Include(u => u.Roles).Include(u => u.Devices).Where(x => x.TokenRecovery == token && x.DeleteAt == null).FirstOrDefault();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = user.CreatedAt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user, int id)
        {
            if (user == null)
                return null;
            User? exist = await _context.Set<User>().FindAsync(id);
            if (exist != null)
            {
                user.UpdatedAt = DateTime.UtcNow;
                user.Id = exist.Id;
                user.CreatedAt = exist.CreatedAt;
                _context.Entry(exist).CurrentValues.SetValues(user);


                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            User? exist = await _context.Set<User>().FindAsync(id);
            if (exist != null)
            {
                _context.Users.Remove(exist);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<User> UpdateUserPasswordAsync(int id, string password)
        {
            var user = _context.Users.Where(x => x.Id == id && x.DeleteAt == null).FirstOrDefault();

            user.UpdatedAt = DateTime.UtcNow;
            user.PasswordHash = password;
            user.NewUserHash = null;
            user.TokenRecovery = null;
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> UpdateUserLoginAsync(int id, DateTime last)
        {
            var user = _context.Users.Where(x => x.Id == id && x.DeleteAt == null).FirstOrDefault();

            user.UpdatedAt = DateTime.UtcNow;
            user.LastLogin = last;
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> UpdateUserRecoveryAsync(int id, string token)
        {
            var user = _context.Users.Where(x => x.Id == id && x.DeleteAt == null).FirstOrDefault();

            user.UpdatedAt = DateTime.UtcNow;
            user.TokenRecovery = token;
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

    }
}
