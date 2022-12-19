using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? PasswordHash { get; set; }
        public string? NewUserHash { get; set; }
        public string? TokenRecovery { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public List<Role> Roles { get; set; }
        public List<Device> Devices { get; set; }
        [Required]
        public Person Person { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
        public DateTime? DeleteAt { get; set; } = null;
        public DateTime? LastLogin { get; set; }
    }

    public class Characteristic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool canToken { get; set; }
    }
}
