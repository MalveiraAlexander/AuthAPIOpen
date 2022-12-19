using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Cookie { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public bool IsApp { get; set; }
        [Required]
        public string PublicIP { get; set; }
    }
}
