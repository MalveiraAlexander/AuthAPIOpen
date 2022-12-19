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
    public class RoleBusiness : IRoleBusiness
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RoleBusiness(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        public async Task<List<RoleResponse>> GetRolesAsync()
        {
            var result = mapper.Map<List<RoleResponse>>(await roleService.GetRolesAsync());
            return result;
        }

        public async Task<RoleResponse> GetRoleAsync(int roleId)
        {
            var result = mapper.Map<RoleResponse>(await roleService.GetRoleAsync(roleId));
            return result;
        }

        public async Task<RoleResponse> DeleteRoleAsync(int roleId)
        {
            var result = mapper.Map<RoleResponse>(await roleService.DeleteRoleAsync(roleId));
            return result;
        }

        public async Task<RoleResponse> AddRoleAsync(RoleRequest request)
        {
            var role = mapper.Map<Role>(request);
            var result = mapper.Map<RoleResponse>(await roleService.CreateRoleAsync(role));
            return result;
        }

        public async Task<RoleResponse> UpdateRoleAsync(RoleRequest request, int roleId)
        {
            var role = mapper.Map<Role>(request);
            var result = mapper.Map<RoleResponse>(await roleService.UpdateRoleAsync(role, roleId));
            return result;
        }


    }
}
