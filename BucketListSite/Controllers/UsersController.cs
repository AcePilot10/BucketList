using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using BucketListSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BucketListSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public UsersController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet("GetUserByEmail")]
        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        [HttpGet("GetUserByUsername")]
        public async Task<User> GetUserByUsername(string username)
        {
            var result = await _userManager.FindByNameAsync(username);
            return result;
        }

        [HttpGet("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        [HttpPost("RegisterUser")]
        public async Task<IdentityResult> RegisterUser([FromBody]User user, string password)
        {
            User userFormatted = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = HashPassword(password)
            };
            var result = await _userManager.CreateAsync(userFormatted);
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

        [HttpGet("SignInUser")]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> SignInUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var response = await _signInManager.PasswordSignInAsync(user, password, true, false);
            string json = JsonConvert.SerializeObject(response);
            return response;
        }

        [HttpGet("signout")]
        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpGet("GetUserEvents")]
        public async Task<List<UserEvent>> GetUserEvents(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user.Events;
        }

        [HttpPost("CreateListItem")]
        public async void CreateListItem([FromBody]BucketListItem item, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            //Update list items JSON
            var listItems = user.BucketListItems;
            listItems.Add(item);
            string json = JsonConvert.SerializeObject(listItems);
            user.BucketListItemsJson = json;

            //Update events
            UserCreatedItemEvent createdEvent = new UserCreatedItemEvent()
            {
                ItemId = item.Id,
                Time = DateTime.Now,
                Title = user.UserName + " added to their bucket list!",
                UserId = user.Id
            };
            var eventList = user.Events;
            eventList.Add(createdEvent);
            string eventJson = JsonConvert.SerializeObject(eventList);
            user.EventsJson = eventJson;
            await _userManager.UpdateAsync(user);
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}