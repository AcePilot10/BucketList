using BucketList.Api.Http;
using BucketList.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Managers
{
    public class SignInManager
    {
        private static SignInManager _instance;
        public static SignInManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SignInManager();
                }
                return _instance;
            }
        }

        public async Task<BucketListSignInResult> SignInUser(string email, string password)
        {
            var result = await Client.Instance.GetClient.GetAsync("api/users/SignInUser" +
                "?email=" + email + "&password=" + password);
            string response = await result.Content.ReadAsStringAsync();
            var signInResult = JsonConvert.DeserializeObject<BucketListSignInResult>(response);
            return signInResult;
        }

        public async void Signout()
        {
            await Client.Instance.GetClient.GetAsync("api/users/signout");
        }
    }
}