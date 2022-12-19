using AuthAPI.Commons.Requests;
using AuthAPI.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IBusiness
{
    public interface IRoleBusiness
    {
        Task<List<RoleResponse>> GetRolesAsync();
        Task<RoleResponse> GetRoleAsync(int roleId);
        Task<RoleResponse> DeleteRoleAsync(int roleId);
        Task<RoleResponse> AddRoleAsync(RoleRequest request);
        Task<RoleResponse> UpdateRoleAsync(RoleRequest request, int roleId);
    }
}
