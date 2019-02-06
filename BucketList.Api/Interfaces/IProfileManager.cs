using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Interfaces
{
    public interface IProfileManager
    {
        void SaveUser(User user);
        Task<BucketListItem> CreateListItem(string body, Guid userId);
        Task<HttpStatusCode> DeleteListItem(Guid userId, Guid itemId);
        Task<HttpStatusCode> SetItemStatus(Guid itemId, int status);
        Task<HttpStatusCode> Follow(Guid userId, Guid userToFollowId);
        Task<HttpStatusCode> SaveItem(BucketListItem item);
        Task<string> RequestPasswordChange(string email);
    }
}