using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using BucketListSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BucketListSite.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        private UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return Redirect("~/profile/user/" + User.Identity.Name);
        }

        [Route("User/{username}")]
        public async Task<IActionResult> ViewUser([FromRoute]string username)
        {
            User user = await _userManager.FindByNameAsync(username);
            return View("Profile", user);
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser(User userChanges)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Bio = userChanges.Bio;
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }

        [HttpPost("CreateListItem")]
        public async Task<IActionResult> CreateListItem(string body)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            BucketListItem item = new BucketListItem()
            {
                Created = DateTime.Now,
                Status = "In Progress",
                Item = body,
                Id = Guid.NewGuid().ToString()
            };
            var listItems = user.BucketListItems;
            listItems.Add(item);
            string json = JsonConvert.SerializeObject(listItems);
            user.BucketListItemsJson = json;
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }

        [HttpGet("DeleteListItem")]
        public async Task<IActionResult> DeleteBucketListItem(string itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bucketListItemsList = user.BucketListItems;
            bucketListItemsList.RemoveAll(x => x.Id.Equals(itemId));
            user.BucketListItemsJson = JsonConvert.SerializeObject(bucketListItemsList);
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }

        [HttpGet("SetListItemStatus/{id}/{status}")]
        public async Task<IActionResult> SetListItemStatus([FromRoute]string id, [FromRoute]string status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bucketListItemsList = user.BucketListItems;
            var item = bucketListItemsList.Single(x => x.Id.Equals(id));
            item.Status = status;
            user.BucketListItemsJson = JsonConvert.SerializeObject(bucketListItemsList);
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }
    }
}