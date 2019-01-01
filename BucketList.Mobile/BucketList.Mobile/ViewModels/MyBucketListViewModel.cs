using BucketList.Api.Managers;
using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class MyBucketListViewModel
    {
        public ObservableCollection<BucketListItem> Items { get; set; }

        public MyBucketListViewModel()
        {
            LoadItems();
        }

        private void LoadItems()
        {
            var user = ((App)Application.Current).User;
            var items = user.BucketListItems;
            Items = new ObservableCollection<BucketListItem>();
            items.ForEach(x => Items.Add(x));
        }
    }
}