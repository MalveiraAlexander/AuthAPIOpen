using AuthAPI.Commons.Helpers;
using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Interfaces.IService;
using AuthAPI.Commons.Models;
using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserBusiness(IUserService userService, IMapper mapper, IRoleService roleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<UserResponse> AddUserAsync(UserRequest request)
        {
            User user = _mapper.Map<User>(request);
            return _mapper.Map<UserResponse>(await _userService.CreateUserAsync(user));
        }


        public async Task<bool> AddOrRemoveRoleToUserAsync(int userId, int roleId, bool isRemove = false)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var role = await _roleService.GetRoleAsync(roleId);
            if (user == null || role == null)
            {
                throw new Exception("User or Role not found");
            }
            if (!isRemove)
            {
                user.Roles.Add(role);
                role.Users.Add(user);
            }
            else
            {
                user.Roles.Remove(role);
                role.Users.Remove(user);
            }
            await _userService.UpdateUserAsync(user, roleId);
            await _roleService.UpdateRoleAsync(role, roleId);
            return true;
        }


        public async Task<UserResponse> UpdateUserAsync(UserRequest request, int userId)
        {
            User user = _mapper.Map<User>(request);
            return _mapper.Map<UserResponse>(await _userService.UpdateUserAsync(user, userId));
        }

        public async Task<bool> UpdatePassword(int userId, string password)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found. Error S-001");

            string pass = Crypto.Base64ToString(password);
            await _userService.UpdatePasswordAsync(user.Id, pass);
            return true;
        }

        public async Task<UserResponse> DeleteUserAsync(int userId)
        {
            return _mapper.Map<UserResponse>(await _userService.DeleteUserAsync(userId));
        }

        public async Task<List<UserResponse>> GetAllUsersAsync(int lastId, int maxElements)
        {
            return _mapper.Map<List<UserResponse>>(await _userService.GetUsersAsync(lastId, maxElements));
        }

        public async Task<UserResponse> GetUserAsync(int userId)
        {
            return _mapper.Map<UserResponse>(await _userService.GetUserByIdAsync(userId));
        }
    }
}
