using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BucketListSite.Models;
using BucketList.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BucketList.Events.UserEvents;
using Newtonsoft.Json;

namespace BucketListSite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Feed()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            FeedViewModel model = new FeedViewModel();
            model.Events = new List<UserEvent>();
            foreach (var friendId in user.Friends)
            {
                var friend = await _userManager.FindByIdAsync(friendId);
                if (friend.EventsJson != null)
                {
                    var friendEvents = JsonConvert.DeserializeObject<List<UserEvent>>(friend.EventsJson);
                    friendEvents.ForEach(x => model.Events.Add(x));
                }
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}