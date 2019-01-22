using BucketList.Api.Http;
using BucketList.Api.Interfaces;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Managers
{
    public class UserManager : IUserManager
    {
        private static UserManager _instance;
        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
        }

        public async Task RegisterUser(string username, string email, string password, string confirmPassword)
        {
            var result = await Client.Instance.GetClient.PostAsync("api/users/RegisterUser?username=" + username
                                                                   + "&password=" + password
                                                                   + "&email=" + confirmPassword, null);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserByUsername?username=" + username);
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(result);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserByUsername?email=" + email);
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(result);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetAllUsers");
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<User>>(result);
        }

        public delegate bool SearchQuery(User user);

        public async Task<List<User>> GetUsersWhere(SearchQuery query)
        {
            List<User> usersSatisfyingCondition = new List<User>();
            var users = await GetAllUsers();
            foreach (User user in users)
            {
                if (query(user))
                {
                    usersSatisfyingCondition.Add(user);
                }
            }
            return usersSatisfyingCondition;
        }

        public async Task<List<UserEvent>> GetUserEvents(Guid userId)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserEvents?userId=" + userId);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserEvent>>(result);
        }

        public async Task<BucketListSignInResult> SignInUser(string email, string password)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/SignInUser"
                                                                    + "?email=" + email
                                                                    + "&password=" + password);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BucketListSignInResult>(result);
        }
    }
}