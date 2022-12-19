using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Models
{
    public class Device
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Cookie { get; set; }
        [Required]
        public bool isApp { get; set; }
        [Required]
        public string? RefreshToken { get; set; }
        [Required]
        public string PublicIp { get; set; }
        public User User { get; set; }
        public DateTime? DeleteAt { get; set; } = null;
    }
}
