using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BucketList.Events.UserEvents
{
    public class UserEvent
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}