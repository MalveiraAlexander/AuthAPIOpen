using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IRepository
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetDevicesAsync(int userId);
        Task<Device?> GetDeviceAsync(int id);
        Task<Device> DeleteDeviceAsync(int id);
        Task<Device> Save(Device entity);
        Task<Device> SearchByName(string name, int userId);
        Task<Device> Update(Device entity, object key, List<string>? ignoreFields);
        Task<Device> Update(Device entity);
        Task<Device> RefreshTokenValidate(int id, string refreshToken, string nameDevice);
    }
}
