using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IService
{
    public interface IRoleService
    {
        Task<Role> GetRoleAsync(int id);
        Task<List<Role>> GetRolesAsync();
        Task<Role> CreateRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role, int id);
        Task<Role> DeleteRoleAsync(int id);
        Task<Role> AddPermissionAsync(Permission permission, int roleId);
    }
}
