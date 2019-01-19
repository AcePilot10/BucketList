using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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

        public string SetStatusText { get; set; }
        public ICommand SetStatusCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public BucketListItemViewModel(BucketListItem item)
        {
            Item = item;
            SetStatusCommand = new Command(SetStatus);
            DeleteCommand = new Command(Delete);
        }

        private async void SetStatus()
        {
            if (Item.Status == "In Progress")
            {
                Item.Status = "Complete";
                SetStatusText = "Set Incomplete";
            }
            else
            {
                Item.Status = "Incomplete";
                SetStatusText = "Set Complete";
            }
            await UserManager.Instance.SaveUser(App.User);
        }

        private void Delete()
        {

        }
    }
}