using BucketList.Api.Http;
using BucketList.Entities.Models;
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
                Password = password,
                ConfirmPassword = confirmPassword
            };

            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await Client.Instance.GetClient.PostAsync("api/users/RegisterUser", content);
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
            var user = ((App)Application.Current).User;
            string username = user.UserName;
            await Client.Instance.GetClient.PostAsync("profile/CreateListItem?body=" + body, null);
        }
    }
}