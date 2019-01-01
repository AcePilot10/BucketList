﻿using BucketList.Api.Http;
using BucketList.Api.Interfaces;
using BucketList.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Managers
{
    public class SignInManager : ISignInManager
    {
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