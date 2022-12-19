using AuthAPI.Commons.Data;
using AuthAPI.Commons.Interfaces.IRepository;
using AuthAPI.Commons.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Core.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthDataContext _context;

        public RoleRepository(AuthDataContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleAsync(int id)
        {
            return _context.Roles.Include(r => r.Permissions).Include(r => r.Users).Where(r => r.DeleteAt == null).FirstOrDefault(r => r.Id == id);
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return _context.Roles.Include(r => r.Permissions).Where(r => r.DeleteAt == null).ToList();
        }

        public async Task<Role> CreateRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRoleAsync(Role role, int id)
        {
            if (role == null)
                return null;
            Role? exist = await _context.Set<Role>().FindAsync(id);
            if (exist != null)
            {
                role.Id = exist.Id;
                _context.Entry(exist).CurrentValues.SetValues(role);


                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<Role> DeleteRoleAsync(int id)
        {
            Role? role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
            return role;
        }
        
        public async Task<Role> AddPermissionAsync(Permission permission, int roleId)
        {
            Role? role = _context.Roles.Include(r => r.Permissions).Where(r => r.DeleteAt == null).FirstOrDefault(r => r.Id == roleId);
            if (role != null)
            {
                role.Permissions.Add(permission);
                _context.Entry(role).CurrentValues.SetValues(role);
                await _context.SaveChangesAsync();
            }
            return role;
        }
    }
}
