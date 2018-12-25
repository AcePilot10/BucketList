using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BucketList.Entities.Models
{
    public class BucketListItem
    {
        [Key]
        public string Id { get; set; }
        public string Item { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}