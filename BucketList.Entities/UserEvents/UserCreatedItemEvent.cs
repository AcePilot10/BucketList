using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Events.UserEvents
{
    public class UserCreatedItemEvent : UserEvent
    {
        public string ItemId { get; set; }
    }
}