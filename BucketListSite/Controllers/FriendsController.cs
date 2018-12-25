using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucketListSite.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class FriendsController : Controller
    {
        private UserManager<User> _userManager;

        public FriendsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("index")]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            return View(user);
        }
    }
}