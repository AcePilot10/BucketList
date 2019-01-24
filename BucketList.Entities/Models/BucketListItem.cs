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

        [NotMapped]
        public string BackgroundColor
        {
            get
            {
                if (Status == StatusConstants.COMPLETE)
                {
                    return "#33FF00";
                }
                else
                {
                    return "#001CFF";
                }
            }
        }

        [NotMapped]
        public string Statustext
        {
            get
            {
                if (Status == StatusConstants.COMPLETE)
                {
                    return "Complete";
                }
                else
                {
                    return "In Progress";
                }
            }
        }
    }
}