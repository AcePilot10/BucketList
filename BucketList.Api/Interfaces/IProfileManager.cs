using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BucketList.Api.Interfaces
{
    public interface IProfileManager
    {
        void SaveUser(User user);
        Task<BucketListItem> CreateListItem(string body, Guid userId);
        void DeleteListItem(Guid userId, Guid itemId);
        void SetItemStatus(Guid userId, Guid itemId, int status);
        void Follow(Guid userId, Guid userToFollowId);
    }
}