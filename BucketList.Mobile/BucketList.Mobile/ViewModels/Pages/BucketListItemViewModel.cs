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
        #region Properties
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

        private ImageSource _setStatusImage;
        public ImageSource SetStatusImage
        {
            get
            {
                return _setStatusImage;
            }
            set
            {
                _setStatusImage = value;
                RaisePropertyChanged("SetStatusImage");
            }
        }

        private string _backgroundColor;
        public string BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            private set
            {
                _backgroundColor = value;
                RaisePropertyChanged("BackgroundColor");
            }
        }

        private bool _showCompleted;
        public bool ShowCompleted
        {
            get
            {
                return _showCompleted;
            }
            set
            {
                _showCompleted = value;
                RaisePropertyChanged("ShowCompleted");
            }
        }

        public ICommand SetStatusCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion

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
                BackgroundColor = "#001CFF";
                SetStatusImage = ImageSource.FromFile("checked.png");
                ShowCompleted = false;
            }
            else
            {
                SetStatusText = "Set In Progress";
                StatusText = "Complete";
                BackgroundColor = "#33FF00";
                SetStatusImage = ImageSource.FromFile("goal.png");
                ShowCompleted = true;
            }
        }

        private async void SetStatus()
        {
            if (Item.Status == StatusConstants.IN_PROGRESS)
            {
                Item.Status = StatusConstants.COMPLETE;
                SetStatusText = "Set In Progress";
                StatusText = "Complete";
                BackgroundColor = "#33FF00";
                SetStatusImage = ImageSource.FromFile("goal.png");
                Item.Completed = DateTime.Now;
                ShowCompleted = true;
            }
            else
            {
                Item.Status = StatusConstants.IN_PROGRESS;
                SetStatusText = "Set Complete";
                StatusText = "In Progress";
                BackgroundColor = "#001CFF";
                SetStatusImage = ImageSource.FromFile("checked.png");
                ShowCompleted = false;
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