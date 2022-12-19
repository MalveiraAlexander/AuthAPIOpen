using AuthAPI.Commons.Interfaces.IBusiness;
using AuthAPI.Commons.Interfaces.IService;
using AuthAPI.Commons.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness roleBusiness;

        public RoleController(IRoleBusiness roleBusiness)
        {
            this.roleBusiness = roleBusiness;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await roleBusiness.GetRolesAsync());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetRole(int roleId)
        {
            return Ok(await roleBusiness.GetRoleAsync(roleId));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRole(RoleRequest request)
        {
            return Ok(roleBusiness.AddRoleAsync(request));
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateRole(RoleRequest request, [FromQuery] int roleId)
        {
            return Ok(roleBusiness.UpdateRoleAsync(request, roleId));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteRole([FromQuery] int roleId)
        {
            return Ok(roleBusiness.DeleteRoleAsync(roleId));
        }
    }
}
