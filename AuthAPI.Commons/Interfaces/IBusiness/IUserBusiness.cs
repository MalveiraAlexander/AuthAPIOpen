using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IBusiness
{
    public interface IUserBusiness
    {
        Task<UserResponse> AddUserAsync(UserRequest request);
        Task<bool> AddOrRemoveRoleToUserAsync(int userId, int roleId, bool isRemove = false);
        Task<UserResponse> UpdateUserAsync(UserRequest request, int userId);
        Task<UserResponse> DeleteUserAsync(int userId);
        Task<bool> UpdatePassword(int userId, string password);
        Task<List<UserResponse>> GetAllUsersAsync(int lastId, int maxElements);
        Task<UserResponse> GetUserAsync(int userId);
    }
}
