using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BucketList.Api.Managers.UserManager;

namespace BucketList.Api.Interfaces
{
    public interface IUserManager
    {
        Task<BucketListRegisterResult> RegisterUser(string username, string password, string confirmPassword, string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetUsersWhere(SearchQuery query);
    }
}