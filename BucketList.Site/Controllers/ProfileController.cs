using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using BucketList.Site.Database;
using BucketList.Site.Models;
using Newtonsoft.Json;

namespace BucketListSite.Controllers
{
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private BucketListContext _context;

        public ProfileController()
        {
            _context = new BucketListContext();
        }

        [HttpPost]
        [Route("SaveUser")]
        public async void SaveUser([FromBody]User userChanges)
        {
            User user = _context.Users.Single(x => x.ID == userChanges.ID);
            user = userChanges;
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("CreateListItem")]
        public BucketListItem CreateListItem([FromBody]BodyObject body, Guid userId)
        { 
            User user = _context.Users.Single(x => x.ID == userId);
            BucketListItem item = new BucketListItem()
            {
                Created = DateTime.Now,
                Status = StatusConstants.IN_PROGRESS,
                Item = body.Body,
                ID = Guid.NewGuid(),
                UserId = userId
            };

            UserCreatedItemEvent createdEvent = new UserCreatedItemEvent()
            {
                ItemId = item.ID,
                ID = Guid.NewGuid(),
                Time = DateTime.Now,
                Title = user.Username + " added to their bucket list!",
                UserId = user.ID,
            };

            _context.Items.Add(item);
            _context.UserEvents.Add(createdEvent);

            _context.SaveChanges();

            return item;
        }

        [HttpDelete]
        [Route("DeleteListItem")]
        public void DeleteListItem(Guid itemId, Guid userId)
        {
            BucketListItem item = _context.Items.Single(x => x.UserId == userId);
            _context.Items.Remove(item);

            var events = from x in _context.UserEvents
                         where x.UserId == userId
                         select x;

            foreach (var e in events)
            {
                if (e is UserCreatedItemEvent)
                {
                    if (((UserCreatedItemEvent)e).ItemId == itemId)
                    {
                        _context.UserEvents.Remove(e);
                    }
                }
            }

            _context.SaveChanges();
        }

        [HttpPut]
        [Route("SetItemStatus")]
        public void SetItemStatus(Guid userId, Guid itemId, int status)
        {
            var user = _context.Users.Single(x => x.ID == userId);
            var item = _context.Items.Single(x => x.UserId == userId && x.ID == itemId);
            item.Status = status;
            _context.SaveChanges(); 
        }

        [HttpGet]
        [Route("Follow")]
        public void Follow(Guid userId, Guid userToFollowId)
        {
            var user = _context.Users.Single(x => x.ID == userId);
            var followedUsers = user.GetFollowedUsers();
            followedUsers.Add(userToFollowId);
            user.FollowedUsersJson = JsonConvert.SerializeObject(followedUsers);
            _context.SaveChanges();
        }
    }
}