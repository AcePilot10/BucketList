using BucketList.Events.UserEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucketListSite.Models
{
    public class FeedViewModel
    {
        public List<UserEvent> Events { get; set; }
    }
}