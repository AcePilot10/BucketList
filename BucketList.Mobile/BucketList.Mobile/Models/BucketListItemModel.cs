using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Mobile.Models
{
    public class BucketListItemModel
    {
        public BucketListItem Item { get; set; }
        public bool IsCompleted
        {
            get
            {
                return Item.Status == StatusConstants.COMPLETE;
            }
        }
    }
}