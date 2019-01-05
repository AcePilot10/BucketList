using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
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
            return Redirect("~/profile/user/" + User.Identity.Name);
        }


        [HttpGet("DeleteListItem")]
        public async Task<IActionResult> DeleteBucketListItem(string itemId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bucketListItemsList = user.BucketListItems;
            BucketListItem item = bucketListItemsList.Single(x => x.Id.Equals(itemId));
            bucketListItemsList.RemoveAll(x => x.Id.Equals(itemId));
            user.BucketListItemsJson = JsonConvert.SerializeObject(bucketListItemsList);
            var events = user.Events;
            var createdEvent = events.SingleOrDefault(x =>
            {
                try
                {
                    UserCreatedItemEvent e = (UserCreatedItemEvent)x;
                    return e.ItemId.Equals(item.Id);
                }
                catch (InvalidCastException) { return false; }
            });

            if (createdEvent != null)
            {
                events.Remove(createdEvent);
            }

            string json = JsonConvert.SerializeObject(events);
            user.EventsJson = json;

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
            var eventList = user.Events;
            if (status.Equals("Complete"))
            {
                var completedEvent = new UserCompletedItemEvent()
                {
                    Time = DateTime.Now,
                    Title = user.UserName + " completed " + item.Item + "!",
                    ItemId = item.Id,
                    UserId = user.Id
                };
                eventList.Add(completedEvent);
            }
            else
            {
                var completedEvent = eventList.SingleOrDefault(x =>
                 {
                     try
                     {
                         UserCompletedItemEvent currrentCompletedEvent = (UserCompletedItemEvent)x;
                         return currrentCompletedEvent.ItemId.Equals(item.Id);
                     }
                     catch (InvalidCastException) { return false;  }
                 });
                if (completedEvent != null)
                {
                    eventList.Remove(completedEvent);
                }
            }
            string json = JsonConvert.SerializeObject(eventList);
            user.EventsJson = json;
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }

        [HttpGet("Follow/{id}")]
        public async Task<IActionResult> Follow([FromRoute]string id)
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var friendsList = user.Friends;
            friendsList.Add(id);
            user.FriendsJson = JsonConvert.SerializeObject(friendsList);
            await _userManager.UpdateAsync(user);
            return Redirect("~/profile/user/" + User.Identity.Name);
        }
    }
}