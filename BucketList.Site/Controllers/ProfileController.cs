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
        public BucketListItem CreateListItem([FromBody]BucketListItem item)
        {
            item.ID = Guid.NewGuid();
            item.Created = DateTime.Now;
            item.Completed = DateTime.Now;
            var user = _context.Users.SingleOrDefault(x => x.ID == item.UserId);

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
            BucketListItem item = _context.Items.Single(x => x.ID == itemId);
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

        [HttpGet]
        [Route("SetItemStatus")]
        public void SetItemStatus(Guid itemId, int status)
        {
            var item = _context.Items.Single(x => x.ID == itemId);
            item.Status = status;

            string username = _context.Users.Single(x => x.ID == item.UserId).Username;

            if (status == StatusConstants.COMPLETE)
            {
                UserCompletedItemEvent userEvent = new UserCompletedItemEvent()
                {
                    ID = Guid.NewGuid(),
                    ItemId = item.ID,
                    Time = DateTime.Now,
                    Title = username + " completed: " + item.Item,
                    UserId = item.UserId
                };
                _context.UserEvents.Add(userEvent);
                item.Completed = DateTime.Now;
            }
            else
            {
                var userEvents = _context.UserEvents;
                foreach (var userEvent in userEvents)
                {
                    if (userEvent is UserCompletedItemEvent)
                    {
                        var completedEvent = userEvent as UserCompletedItemEvent;
                        if(completedEvent.ItemId == itemId)
                        {
                            _context.UserEvents.Remove(completedEvent);
                        }
                    }
                }
            }

            SaveItem(item);
            _context.SaveChanges(); 
        }

        [HttpGet]
        [Route("Follow")]
        public void Follow(Guid userId, Guid userToFollowId)
        {
            var user = _context.Users.Single(x => x.ID == userId);
            List<Guid> followedUsers;
            try
            {
                followedUsers = JsonConvert.DeserializeObject<List<Guid>>(user.FollowedUsersJson);
            }
            catch (Exception)
            {
                followedUsers = new List<Guid>();
            }
            followedUsers.Add(userToFollowId);
            user.FollowedUsersJson = JsonConvert.SerializeObject(followedUsers);
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("SaveItem")]
        public string SaveItem([FromBody]BucketListItem changedItem)
        {
            var item = _context.Items.Single(x => x.ID == changedItem.ID);
            _context.Entry(item).CurrentValues.SetValues(changedItem);
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return "Item " + item.ID + " has been saved!";
        }
    }
}