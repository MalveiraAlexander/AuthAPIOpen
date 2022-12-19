using AuthAPI.Business;
using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Interfaces.IRepository;
using AuthAPI.Commons.Interfaces.IService;
using AuthAPI.Core.Repositories;
using AuthAPI.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AuthAPI
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            #region Repository
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            #endregion
            #region Service
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IEmailService, EmailService>();
            #endregion
            #region Business
            services.AddTransient<IAuthBusiness, AuthBusiness>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IRoleBusiness, RoleBusiness>();
            #endregion

            return services;
        }
    }
}
