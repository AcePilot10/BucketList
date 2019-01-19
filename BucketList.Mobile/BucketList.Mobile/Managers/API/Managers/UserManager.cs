using BucketList.Api.Http;
using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using BucketList.Mobile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BucketList.Api.Managers
{
    public class UserManager
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

        public async Task<BucketListRegisterResult> RegisterUser(string username, string email, string password, string confirmPassword)
        {
            var user = new
            {
                UserName = username,
                Email = email,
            };

            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await Client.Instance.GetClient.PostAsync("api/users/RegisterUser?password=" + password, content);
            string resultContent = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BucketListRegisterResult>(resultContent);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserByUsername?username=" + username);
            string result = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<User>(result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserByEmail?email=" + email);
            string result = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<User>(result);
            }
            catch(Exception)
            {
                return null;
            }
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

        public async void CreateListItem(string body)
        {
            BucketListItem item = new BucketListItem()
            {
                Created = DateTime.Now,
                Status = "In Progress",
                Item = body,
                Id = Guid.NewGuid().ToString()
            };

            var user = App.User;
            string username = user.UserName;

            var json = JsonConvert.SerializeObject(item);

            StringContent content = new StringContent(json);
            await Client.Instance.GetClient.PostAsync("api/users/CreateListItem?username=" + username, content);
        }

        public async Task<List<UserEvent>> GetUserEvents(string username)
        {
            var response = await Client.Instance.GetClient.GetAsync("api/users/GetUserEvents?username=" + username);
            var result = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<List<UserEvent>>(username);
            }
            catch (Exception)
            {
                return new List<UserEvent>();
            }
        }

        public async Task SaveUser(User user)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user));
            await Client.Instance.GetClient.PostAsync("profile/SaveUser", content);
        }
    }
}