using BucketList.Entities.Models;
using BucketList.Events.UserEvents;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BucketList.Site.Database
{
    public class BucketListContext : DbContext
    {

        public BucketListContext(): base("MyDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<BucketListItem> Items { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
    }
}