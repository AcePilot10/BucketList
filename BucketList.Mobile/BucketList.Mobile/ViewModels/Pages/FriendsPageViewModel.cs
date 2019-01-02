using BucketList.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class FriendsPageViewModel
    {
        public ICommand SearchCommand { get; private set; }

        public FriendsPageViewModel()
        {
            SearchCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new SearchPage()));
        }
    }
}