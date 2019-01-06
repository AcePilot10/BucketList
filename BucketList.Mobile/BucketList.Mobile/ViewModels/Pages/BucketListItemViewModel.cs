using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class BucketListItemViewModel
    {
        public BucketListItem Item { get; set; }

        public BucketListItemViewModel(BucketListItem item)
        {
            Item = item;
        }
    }
}