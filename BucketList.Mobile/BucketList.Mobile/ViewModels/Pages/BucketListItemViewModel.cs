using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class BucketListItemViewModel : ViewModel
    {
        private BucketListItem _item;
        public BucketListItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                RaisePropertyChanged("Item");
            }
        }

        public BucketListItemViewModel(BucketListItem item)
        {
            Item = item;
        }
    }
}