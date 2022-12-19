using AuthAPI.Business;
using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Models;
using AuthAPI.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness authBusiness;

        public AuthController(IAuthBusiness authBusiness)
        {
            this.authBusiness = authBusiness;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var x = Request.Headers;
            return Ok(await authBusiness.Login(request));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout(int userId, string cookie, int deviceId = 0)
        {
            var response = new
            {
                Success = await authBusiness.Logout(userId, cookie, deviceId)
            };
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(await authBusiness.RegisterAsync(request));
        }

        [HttpGet("finish")]
        public async Task<IActionResult> FinishRegister(string hash, string password)
        {
            var response = new
            {
                Success = await authBusiness.FinishRegister(hash, password)
            };
            return Ok(response);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> ResfreshToken(int userId, string refreshToken, string cookie)
        {
            return Ok(await authBusiness.RefreshToken(userId, refreshToken, cookie));
        }

        [HttpGet("recoverPassword")]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            await authBusiness.GetRecovery(email);
            var response = new
            {
                Success = true
            };
            return Ok(response);
        }

        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(string tokenRecovery, string password)
        {
            var response = new
            {
                Success = await authBusiness.UpdatePassword(tokenRecovery, password)
            };
            return Ok(response);
        }

        [HttpGet("validateToken")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            var response = new
            {
                IsValid = true
            };
            return Ok(response);
        }
    }
}
