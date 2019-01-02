using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class NavBarViewModel
    {
        public ICommand NavigateFeedCommand { get; private set; }
        public ICommand NavigateFriendsCommand { get; private set; }
        public ICommand NavigateMyListCommand { get; private set; }

        public NavBarViewModel()
        {
            NavigateFeedCommand = new Command(x => Application.Current.MainPage = new FeedPage());
            NavigateFriendsCommand = new Command(x => Application.Current.MainPage = new FriendsPage());
            NavigateMyListCommand = new Command(x => Application.Current.MainPage = new MyBucketListPage());
        }
    }
}