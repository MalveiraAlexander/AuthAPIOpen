using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IBusiness
{
    public interface IAuthBusiness
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> Logout(int userId, string? cookie, int deviceId = 0);
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<string> GetRecovery(string email);
        Task<bool> UpdatePassword(string token, string password);
        Task<bool> FinishRegister(string hash, string password);
        Task<LoginResponse> RefreshToken(int userId, string refreshToken, string cookie);
    }
}
