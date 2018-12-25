using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucketList.Entities.Models;
using BucketListSite.Data;
using BucketListSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucketListSite.Controllers
{
    [AllowAnonymous]
    [Route("Search")]
    public class SearchController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public SearchController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("Index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Keyword")]
        public IActionResult Keyword(string searchTerm)
        {
            var users = from user in _context.Users
                        select user;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                users  = users.Where(s => 
                s.UserName.Contains(searchTerm));
            }

            List<User> userList = users.ToList();
            UserListViewModel viewModel = new UserListViewModel()
            {
                Users = userList
            };

            return View("SearchResult", viewModel);
        }
    }

    //[Route("ViewUser/{id}")]
    //public async Task<IActionResult> ViewUser([FromRoute]string id)
    //{
    //    var user = await _userManager.Find 
    //}
}