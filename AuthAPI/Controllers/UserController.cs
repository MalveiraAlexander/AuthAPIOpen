using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;
        private readonly IAuthBusiness authBusiness;

        public UserController(IUserBusiness userBusiness, IAuthBusiness authBusiness)
        {
            this.userBusiness = userBusiness;
            this.authBusiness = authBusiness;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser(int userId)
        {
            return Ok(await userBusiness.GetUserAsync(userId));
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers(int lastId, int maxElements = 10)
        {
            return Ok(await userBusiness.GetAllUsersAsync(lastId, maxElements));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest request)
        {
            return Ok(await userBusiness.AddUserAsync(request));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UserRequest request, [FromQuery] int userId)
        {
            return Ok(await userBusiness.UpdateUserAsync(request, userId));
        }

        [HttpPatch("role")]
        [Authorize]
        public async Task<IActionResult> AddRoleOrRemoveToUser(int userId, int roleId, bool isRemove = false)
        {
            var response = new
            {
                Success = await userBusiness.AddOrRemoveRoleToUserAsync(userId, roleId, isRemove)
            };
            return Ok(response);
        }

        [HttpPatch("updatePassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePasswordUser(int userId, string password)
        { 
            var response = new
            {
                Success = await userBusiness.UpdatePassword(userId, password)
            };
            return Ok(response);
        }
    }
}
