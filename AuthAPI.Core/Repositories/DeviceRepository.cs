using AuthAPI.Commons.Data;
using AuthAPI.Commons.Interfaces.IRepository;
using AuthAPI.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Core.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly AuthDataContext _context;

        public DeviceRepository(AuthDataContext context)
        {
            _context = context;
        }

        public async Task<Device> Save(Device entity)
        {
            _context.Devices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<List<Device>> GetDevicesAsync(int userId)
        {
            return _context.Devices.Include(d => d.User).Where(d => d.User.Id == userId && d.DeleteAt == null).ToList();
        }

        public async Task<Device?> GetDeviceAsync(int id)
        {
            return _context.Devices.Include(d => d.User).Where(d => d.Id == id && d.DeleteAt == null).FirstOrDefault();
        }

        public async Task<Device?> SearchByName(string cookie, int userId)
        {
            var user = _context.Devices.Where(u => u.Cookie == cookie && u.User.Id == userId && u.DeleteAt == null).FirstOrDefault();
            return user;
        }

        public virtual async Task<Device> Update(Device entity, object key, List<string>? ignoreFields)
        {
            if (entity == null)
                return null;
            Device? exist = await _context.Set<Device>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                _context.Entry(exist).Property("Id").IsModified = false;

                if (ignoreFields != null)
                {
                    foreach (var ignoreField in ignoreFields)
                    {
                        _context.Entry(exist).Property(ignoreField).IsModified = false;
                    }

                }


                await _context.SaveChangesAsync();
            }
            return exist;

        }

        public async Task<Device> DeleteDeviceAsync(int id)
        {
            Device? device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
            return device;
        }

        public async Task<Device> Update(Device entity)
        {
            _context.Devices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Device?> RefreshTokenValidate(int id, string refreshToken, string cookie)
        {
            var device = _context.Devices.Where(u => u.User.Id == id && u.RefreshToken == refreshToken && u.Cookie == cookie && u.DeleteAt == null)
                .FirstOrDefault();
            return device;
        }
    }
}
