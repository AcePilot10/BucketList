using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Events.UserEvents
{
    public class UserEvent
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}
