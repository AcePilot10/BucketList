using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Entities.Models
{
    public class FollowedUsers
    {
        public int FollowedUserId { get; set; }
        public ICollection<Guid> UserId { get; set; }
    }
}