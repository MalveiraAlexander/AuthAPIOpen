using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IService
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByLoginInputAsync(string input);
        Task<List<User>> GetUsersAsync(int lastId, int maxElements);
        Task<User> GetByHashAsync(string hash);
        Task<User> GetByTokenAsync(string token);
        Task<User> UpdateLastLoginAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user, int id);
        Task<User> DeleteUserAsync(int id);
        Task<string> GetTokenRecoveryAsync(string email);
        Task<User> UpdatePasswordAsync(int id, string password);
    }
}
