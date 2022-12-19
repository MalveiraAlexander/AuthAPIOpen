using AuthAPI.Commons.Helpers;
using AuthAPI.Commons.Interfaces.IRepository;
using AuthAPI.Commons.Interfaces.IService;
using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<User> GetUserByLoginInputAsync(string input)
        {
            return await _userRepository.GetUserByLoginInputAsync(input);
        }

        public async Task<User> GetByHashAsync(string hash)
        {
            return await _userRepository.GetUserByHashAsync(hash);
        }

        public async Task<User> GetByTokenAsync(string token)
        {
            return await _userRepository.GetUserByTokenAsync(token);
        }


        public async Task<List<User>> GetUsersAsync(int lastId, int maxElements)
        {
            return await _userRepository.GetUsersAsync(lastId, maxElements);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User user, int id)
        {
            return await _userRepository.UpdateUserAsync(user, id);
        }

        public async Task<User> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<User> UpdateLastLoginAsync(int id)
        {
            var user = await _userRepository.UpdateUserLoginAsync(id, DateTime.UtcNow);
            return user;
        }

        public async Task<string> GetTokenRecoveryAsync(string email)
        {
            var user = await _userRepository.GetUserByLoginInputAsync(email);
            if (user != null)
            {
                var token = new Guid();
                var tokenCrypt = Crypto.HashMD5(token.ToString());
                var user1 = await _userRepository.UpdateUserRecoveryAsync(user.Id, tokenCrypt);
                await _emailService.SendRecoveryAsync(user.Email, user.Person.FirstName, user.Person.LastName, $"auth/reset/{user1.TokenRecovery}", "Recuperar contraseña");
                return user1.TokenRecovery;
            }
            else
            {
                throw new Exception("User not found. Error S-001");
            }
        }

        public async Task<User> UpdatePasswordAsync(int id, string password)
        {
            var passwordHash = Crypto.HashMD5(password);
            var user = await _userRepository.UpdateUserPasswordAsync(id, passwordHash);
            return user;
        }
    }
}
