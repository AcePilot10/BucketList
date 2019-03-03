using System;

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
        public DateTime TargetDate { get; set; }
    }
}