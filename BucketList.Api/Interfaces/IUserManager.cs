using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BucketList.Api.Managers.UserManager;

namespace BucketList.Api.Interfaces
{
    public interface IUserManager
    {
        Task RegisterUser(string username, string password, string confirmPassword, string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetUsersWhere(SearchQuery query);
        Task<List<UserEvent>> GetUserEvents(Guid userId);
        Task<BucketListSignInResult> SignInUser(string email, string password);
    }
}