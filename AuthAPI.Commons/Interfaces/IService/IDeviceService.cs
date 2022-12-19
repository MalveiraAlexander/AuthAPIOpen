using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Interfaces.IService
{
    public interface IDeviceService
    {
        Task<List<Device>> GetDevicesAsync(int userId);
        Task<Device> GetDeviceAsync(int id);
        Task<Device> DeleteDeviceAsync(int id);
        Task<Device> SearchDevice(string cookie, int userId);
        Task<Device> RefreshTokenValidate(int userId, string refreshToken, string cookie);
        Task<Device> SaveDevice(Device device);
        Task<Device> UpdateDevice(Device device);
        Task<Device> UpdateDevice(Device device, int id);
    }
}
