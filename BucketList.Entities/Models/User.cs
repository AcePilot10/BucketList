using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BucketList.Entities.Models
{
    public class User
    {

        public User() { } 
        
        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string FollowedUsersJson { get; set; }

        public List<Guid> GetFollowedUsers()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Guid>>(FollowedUsersJson);
            }
            catch (Exception)
            {
                return new List<Guid>();
            }
        }
    }
}