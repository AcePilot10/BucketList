using BucketList.Api.Http;
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
        public async Task<SignInResult> SignInUser(string email, string password)
        {
            //Uri uri = new Uri("api/users/SignInUser" +
            //    "?email=" + email + "&password=" + password);
            Uri uri = new Uri("https://localhost:44309/api/users/signinuser?email=codyjg10@gmail.com&password=Airplane10");
            var result = await Client.Instance.GetClient.GetAsync(uri);
            //var result = await Client.Instance.GetClient.GetAsync("api/users/SignInUser" +
            //    "?email=" + email + "&password=" + password);
            string response = await result.Content.ReadAsStringAsync();
            SignInResult signInResult = JsonConvert.DeserializeObject<SignInResult>(response);
            return signInResult;
        }
    }
}