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
        }

        private void InitStatusText()
        {
            if (Item.Status == StatusConstants.IN_PROGRESS)
            {
                SetStatusText = "Set Complete";
            }
            else
            {
                SetStatusText = "Set In Progress";
            }
        }

        private void SetStatus()
        {
            if (Item.Status == StatusConstants.IN_PROGRESS)
            {
                Item.Status = StatusConstants.COMPLETE;
                SetStatusText = "Set In Progress";
            }
            else
            {
                Item.Status = StatusConstants.IN_PROGRESS;
                SetStatusText = "Set Complete";
            }
            ProfileManager.Instance.SaveUser(App.User);
        }

        private void Delete()
        {

        }
    }
}