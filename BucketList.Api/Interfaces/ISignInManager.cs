using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Interfaces
{
    public interface ISignInManager
    {
        Task<BucketListSignInResult> SignInUser(string username, string password);
        void Signout();
    }
}