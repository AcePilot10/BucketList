using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using BucketList.Site.Database;
using Newtonsoft.Json;

namespace BucketListSite.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private BucketListContext _context;

        public UsersController()
        {
            _context = new BucketListContext();
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(x => x.Email == email);
        }

        [HttpGet]
        [Route("GetUserByUsername")]
        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(x => x.Username == username);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        [HttpGet]
        [Route("GetAllItems")]
        public List<BucketListItem> GetItems(Guid userId)
        {
            var items = from item in _context.Items
                        where item.UserId == userId
                        select item;
            return items.ToList();
        }

        [HttpPost]
        [Route("RegisterUser")]
        public void RegisterUser(string email, string username, string password)
        {
            User user = new User()
            {
                ID = Guid.NewGuid(),
                Username = username,
                Email = email,
                PasswordHash = SecurePasswordHasher.Hash(password)
            };

            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }

        [HttpGet]
        [Route("SignInUser")]
        public BucketListSignInResult SignInUser(string email, string password)
        {
            BucketListSignInResult result = new BucketListSignInResult();
            result.Errors = new List<string>();
            User user = _context.Users.Single(x => x.Email == email);
            if (user != null)
            {
                if (SecurePasswordHasher.Verify(password, user.PasswordHash))
                {
                    result.Succeeded = true;
                }
                else
                {
                    result.Errors.Add("Password was incorrect");
                    result.Succeeded = false;
                }
            }
            else
            {
                result.Errors.Add("Couldn't find user");
                result.Succeeded = false;
            }
            return result;
        }

        [HttpGet]
        [Route("GetUserEvents")]
        public List<UserEvent> GetUserEvents(Guid userId)
        {
            var events = from x in _context.UserEvents
                         where x.UserId == userId
                         select x;
            return events.ToList();
        }
    }
}