using Emp.Data.Repositories.IRepository;
using Emp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles ="admin,user")]
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            var userClaims = await _roleService.GetUserRolesAsync(email);
            return Ok(userClaims);
        }

        [Authorize(Roles ="admin")]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var list = await _roleService.GetRolesAsync();
            return Ok(list);
        }

        [Authorize(Roles="admin")]
        [HttpPost("AddRoles")]
        public async Task<IActionResult> AddRoles(string[] roles)
        {
            var userRoles = await _roleService.AddRolesAsync(roles);    
            if (userRoles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(userRoles);
        }

        [Authorize(Roles="admin")]  
        [HttpPost("AddUserRoles")]
        public async Task<IActionResult> AddUserRoles([FromBody] User user)
        {
            var assignUserRoles = await _roleService.AddUserRolesAsync(user.Email, user.Roles);
            if(!assignUserRoles)
            {
                return BadRequest();
            }
            return Ok(assignUserRoles);
        }


    }
}
