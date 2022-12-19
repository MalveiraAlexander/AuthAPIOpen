using AuthAPI.Commons.Helpers;
using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Interfaces.IService;
using AuthAPI.Commons.Models;
using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDeviceService _deviceService;

        public AuthBusiness(IUserService userService, IMapper mapper, IDeviceService deviceService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _deviceService = deviceService;
            _mapper = mapper;
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            User user = _mapper.Map<User>(request);
            string pass = Crypto.Base64ToString(request.Password);
            string hash = Crypto.HashMD5(pass);
            user.PasswordHash = hash;
            return _mapper.Map<UserResponse>(await _userService.CreateUserAsync(user));
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            User user = new User();
           
            user = await _userService.GetUserByLoginInputAsync(request.Username);

            if (user == null)
            {
                throw new Exception("User not found. Error S-001");
            }
            string pass = Crypto.Base64ToString(request.Password);
            string hash = Crypto.HashMD5(pass);
            if (user.PasswordHash == null)
            {
                throw new Exception("New user. Error S-003");
            }
            if (user.PasswordHash != hash)
            {
                throw new Exception("Data is incorrect. Error S-002");
            }

            var refreshToken = Crypto.HashMD5(Crypto.RandomString(20));
            var device = new Device
            {
                Name = request.DeviceName,
                Cookie = request.Cookie,
                PublicIp = request.PublicIP,
                isApp = request.IsApp
            };

            try
            {
                if (user != null && device != null)
                {
                    device.RefreshToken = refreshToken;
                    device.User = user;
                    var deviceSaved = new Device();
                    var thisDevice = await _deviceService.SearchDevice(device.Cookie, user.Id);
                    if (thisDevice == null)
                    {
                        deviceSaved = await _deviceService.SaveDevice(device);
                    }
                    else
                    {
                        thisDevice.RefreshToken = refreshToken;
                        deviceSaved = await _deviceService.UpdateDevice(thisDevice);
                    }

                    List<string> roles = new List<string>();
                    foreach (var item in user.Roles)
                    {
                        roles.Add(item.Name);
                    }
                    string rolesString = string.Join(",", roles);
                    string characteristics = string.Empty;
                    if (user.Characteristics != null)
                    {
                        characteristics = JsonConvert.SerializeObject(user.Characteristics.Where(c => c.canToken).ToList());
                    }
                    var _symetricSecurityKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
                        );
                    var _signingCredentials = new SigningCredentials(_symetricSecurityKey, SecurityAlgorithms.HmacSha256);

                    var _Header = new JwtHeader(_signingCredentials);
                    var _Claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        new Claim("firstName", user.Person.FirstName),
                        new Claim("lastName", user.Person.LastName),
                        new Claim("roles", rolesString),
                        new Claim("id", user.Id.ToString()),
                        new Claim("characteristics", characteristics)
                    };
                    var _Payload = new JwtPayload(
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            claims: _Claims,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.AddMinutes(5)
                        );
                    var _Token = new JwtSecurityToken(
                            _Header,
                            _Payload
                        );
                    await _userService.UpdateLastLoginAsync(user.Id);
                    var loginDto = new LoginResponse();
                    var token = new JwtSecurityTokenHandler().WriteToken(_Token);
                    loginDto.Token = token;
                    loginDto.RefreshToken = deviceSaved.RefreshToken!;
                    loginDto.ExpiresIn = DateTime.Now.AddMinutes(5);
                    return loginDto;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> Logout(int userId, string? cookie, int deviceId = 0)
        {
            Device device = new Device();
            if (cookie != null)
            {
                device = await _deviceService.SearchDevice(cookie, userId);
            }
            else if (deviceId != 0)
            {
                device = await _deviceService.GetDeviceAsync(deviceId);
            }
            
            if (device != null)
            {
                await _deviceService.DeleteDeviceAsync(device.Id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdatePassword(string token, string password)
        {
            var user = await _userService.GetByTokenAsync(token);
            if (user == null)
                throw new Exception("User not found. Error S-001");
            string pass = Crypto.Base64ToString(password);
            await _userService.UpdatePasswordAsync(user.Id, pass);
            return true;
        }

        public async Task<string> GetRecovery(string email)
        {
            return await _userService.GetTokenRecoveryAsync(email);
        }

        public async Task<bool> FinishRegister(string hash, string password)
        {
            var user = await _userService.GetByHashAsync(hash);
            if (user != null)
            {
                string pass = Crypto.Base64ToString(password);
                await _userService.UpdatePasswordAsync(user.Id, pass);
                return true;
            }
            else
            {
                throw new Exception("Hash not found. Error S-004");
            }
        }


        public async Task<LoginResponse> RefreshToken(int userId, string refreshToken, string cookie)
        {
            var device = await _deviceService.RefreshTokenValidate(userId, refreshToken, cookie);
            var user = await _userService.GetUserByIdAsync(userId);
            if (device != null)
            {
                var refreshTokenNew = Crypto.HashMD5(Crypto.RandomString(20));
                device.RefreshToken = refreshTokenNew;

                await _deviceService.UpdateDevice(device);
                List<string> roles = new List<string>();
                foreach (var item in user.Roles)
                {
                    roles.Add(item.Name);
                }
                string rolesString = string.Join(",", roles);
                string characteristics = string.Empty;
                if (user.Characteristics != null)
                {
                    characteristics = JsonConvert.SerializeObject(user.Characteristics.Where(c => c.canToken).ToList());
                }
                var _symetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
                    );
                var key = _configuration["JWT:SecretKey"];
                var _signingCredentials = new SigningCredentials(_symetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var _Header = new JwtHeader(_signingCredentials);
                var _Claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        new Claim("firstName", user.Person.FirstName),
                        new Claim("lastName", user.Person.LastName),
                        new Claim("roles", rolesString),
                        new Claim("id", user.Id.ToString()),
                        new Claim("characteristics", characteristics)
                    };
                var _Payload = new JwtPayload(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        claims: _Claims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.UtcNow.AddMinutes(5)
                    );
                var _Token = new JwtSecurityToken(
                        _Header,
                        _Payload
                    );
                await _userService.UpdateLastLoginAsync(user.Id);
                var loginDto = new LoginResponse();
                var token = new JwtSecurityTokenHandler().WriteToken(_Token);
                loginDto.Token = token;
                loginDto.RefreshToken = device.RefreshToken;
                loginDto.ExpiresIn = DateTime.UtcNow.AddMinutes(5);
                return loginDto;

            }
            else
            {
                throw new Exception("El dispositivo y el refreshToken no son validos. Error S-401");
            }
        }
    }
}
