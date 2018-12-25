using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using BucketListSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BucketListSite.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<User> GetUserByEmail([FromRoute]string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        [HttpPost("CreateRole/{roleName}")]
        public async Task<IActionResult> CreateRole([FromRoute]string roleName)
        {
            var role = new IdentityRole()
            {
                Name = roleName
            };
            var result = await _roleManager.CreateAsync(role);
            return Content(result.ToString());
        }

        [HttpPost("AssignUserToRole/{userName}/{roleName}")]
        public async Task<IActionResult> AssignUserRole([FromRoute]string userName, [FromRoute]string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return Content(result.ToString());
        }
    }
}