using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BucketList.Entities.Models
{
    public class BucketListItem
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public string Item { get; set; }
        public int Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Completed { get; set; }

        //[NotMapped]
        //[JsonIgnore]
        //public bool IsCompleted
        //{
        //    get
        //    { 
        //        return Status == StatusConstants.COMPLETE;
        //    }
        //}
    }
}