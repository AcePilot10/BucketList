using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BucketList.Entities.Models
{
    public class User : IdentityUser
    {
        public string Bio { get; set; }
        public string BucketListItemsJson { get; set; }
        public string FriendsJson { get; set; }

        [NotMapped]
        public List<BucketListItem> BucketListItems
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<BucketListItem>>(BucketListItemsJson);
                } 
                catch(Exception)
                {
                    return new List<BucketListItem>();
                }
            }
        }

        [NotMapped]
        public List<string> Friends
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<string>>(FriendsJson);
                }
                catch (Exception)
                {
                    return new List<string>();
                }
            }
        }
    }
}