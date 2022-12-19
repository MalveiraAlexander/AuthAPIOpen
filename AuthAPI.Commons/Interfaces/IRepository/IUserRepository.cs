using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User?> GetUserByLoginInputAsync(string input);
        Task<List<User>> GetUsersAsync(int lastId, int maxElements);
        Task<User?> GetUserByHashAsync(string hash);
        Task<User?> GetUserByTokenAsync(string token);
        Task<User> UpdateUserPasswordAsync(int id, string password);
        Task<User> UpdateUserLoginAsync(int id, DateTime last);
        Task<User> UpdateUserRecoveryAsync(int id, string token);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user, int id);
        Task<User> DeleteUserAsync(int id);
    }
}
