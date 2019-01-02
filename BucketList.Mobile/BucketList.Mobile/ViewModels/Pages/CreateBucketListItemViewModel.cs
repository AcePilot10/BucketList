using BucketList.Api.Managers;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class CreateBucketListItemViewModel
    {        
        public ICommand CreateCommand { get; private set; }

        public string Body { get; set; }

        public CreateBucketListItemViewModel()
        {
            CreateCommand = new Command(Create);
        }

        private void Create()
        {
            UserManager.Instance.CreateListItem(Body);
            Application.Current.MainPage = new NavigationPage(new MyBucketListPage());
        }
    }
}