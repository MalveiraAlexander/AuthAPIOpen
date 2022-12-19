using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Requests
{
    public class RoleRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<Permission> Permissions { get; set; }
    }

    public class PermissionRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsEnable { get; set; }
    }
}
