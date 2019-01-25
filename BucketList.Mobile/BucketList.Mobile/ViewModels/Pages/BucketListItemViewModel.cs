using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Base;
using BucketList.Mobile.Views.Pages;
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

        private string _setStatusText;
        public string SetStatusText
        {
            get
            {
                return _setStatusText;
            }
            set
            {
                _setStatusText = value;
                RaisePropertyChanged("SetStatusText");
            }
        }

        private User _user;
        public User User {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                _statusText = value;
                RaisePropertyChanged("StatusText");
            }
           
        }

        public string BackgroundColor
        {
            get
            {
                if (Item.Status == StatusConstants.COMPLETE)
                {
                    return "#33FF00";
                }
                else
                {
                    return "#001CFF";
                }
            }
        }

        public ICommand SetStatusCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public BucketListItemViewModel(BucketListItem item)
        {
            Item = item;
            SetStatusCommand = new Command(SetStatus);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new EditPage(item)));
            InitStatusText();
            LoadUser();
        }

        private async void LoadUser()
        {
            var result = await UserManager.Instance.GetUsersWhere(x => x.ID == Item.UserId);
            User = result[0];
        }

        private void InitStatusText()
        {
            if (Item.Status == StatusConstants.IN_PROGRESS)
            {
                SetStatusText = "Set Complete";
                StatusText = "In Progress";
            }
            else
            {
                SetStatusText = "Set In Progress";
                StatusText = "Complete";
            }
        }

        private async void SetStatus()
        {
            if (Item.Status == StatusConstants.IN_PROGRESS)
            {
                Item.Status = StatusConstants.COMPLETE;
                SetStatusText = "Set In Progress";
                StatusText = "Complete";
            }
            else
            {
                Item.Status = StatusConstants.IN_PROGRESS;
                SetStatusText = "Set Complete";
                StatusText = "In Progress";
            }
            var result = await ProfileManager.Instance.SetItemStatus(Item.ID, Item.Status);
        }

        private async void Delete()
        {
            var result = await ProfileManager.Instance.DeleteListItem(App.User.ID, Item.ID);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}