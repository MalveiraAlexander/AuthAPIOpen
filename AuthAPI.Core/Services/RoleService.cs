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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await _roleRepository.GetRoleAsync(id);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _roleRepository.GetRolesAsync();
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            return await _roleRepository.CreateRoleAsync(role);
        }

        public async Task<Role> UpdateRoleAsync(Role role, int id)
        {
            return await _roleRepository.UpdateRoleAsync(role, id);
        }

        public async Task<Role> DeleteRoleAsync(int id)
        {
            return await _roleRepository.DeleteRoleAsync(id);
        }

        public async Task<Role> AddPermissionAsync(Permission permission, int roleId)
        {
            return await _roleRepository.AddPermissionAsync(permission, roleId);
        }
    }
}
