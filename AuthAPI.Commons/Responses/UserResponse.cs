using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<CharacteristicResponse> Characteristics { get; set; }
        public List<RoleResponse> Roles { get; set; }
        public List<DeviceResponse> Devices { get; set; }
        public PersonResponse Person { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime LastLogin { get; set; }
    }
    public class PersonResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DniCuit { get; set; }
        public string Picture { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CP { get; set; }
        public DateTime YearOfBirth { get; set; }
    }

    public class CharacteristicResponse
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool canToken { get; set; }
    }
}
