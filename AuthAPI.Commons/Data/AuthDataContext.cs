using AuthAPI.Commons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Data
{
    public class AuthDataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AuthDataContext(DbContextOptions<AuthDataContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Person> People { get; set; }

        public override EntityEntry Remove(dynamic entity)
        {
            if (configuration["DeleteMode"] == "logical")
            {
                entity.DeleteAt = DateTime.UtcNow;
                return base.Update((object)entity);
            }
            else
            {
                return base.Remove((object)entity);
            }
            
        }
    }
}
