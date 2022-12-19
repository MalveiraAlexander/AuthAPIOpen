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
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<List<Device>> GetDevicesAsync(int userId)
        {
            return await _deviceRepository.GetDevicesAsync(userId);
        }

        public async Task<Device> GetDeviceAsync(int id)
        {
            return await _deviceRepository.GetDeviceAsync(id);
        }

        public async Task<Device> DeleteDeviceAsync(int id)
        {
            return await _deviceRepository.DeleteDeviceAsync(id);
        }

        public async Task<Device> SearchDevice(string cookie, int userId)
        {
            var result = await _deviceRepository.SearchByName(cookie, userId);

            return result;
        }

        public async Task<Device> RefreshTokenValidate(int userId, string refreshToken, string cookie)
        {
            var result = await _deviceRepository.RefreshTokenValidate(userId, refreshToken, cookie);
            return result;
        }

        public async Task<Device> SaveDevice(Device device)
        {
            var result = await _deviceRepository.Save(device);

            return result;

        }

        public async Task<Device> UpdateDevice(Device device)
        {
            var result = await _deviceRepository.Update(device);

            return result;
        }

        public async Task<Device> UpdateDevice(Device device, int id)
        {
            var result = await _deviceRepository.Update(device, id, null);

            return result;
        }
    }
}
