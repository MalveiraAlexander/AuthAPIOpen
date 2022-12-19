using AuthAPI.Commons.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Requests
{
    public class UserRequest
    {
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public PersonRequest Person { get; set; }
        public List<CharacteristicRequest> Characteristics { get; set; }
    }

    public class PersonRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string DniCuit { get; set; }
        public string Picture { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string CP { get; set; }
        public DateTime YearOfBirth { get; set; }
    }

    public class CharacteristicRequest
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public bool canToken { get; set; }
    }
}
