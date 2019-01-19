using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class FriendsListViewModel
    {
        public ObservableCollection<User> FriendsList { get; set; } = new ObservableCollection<User>();
        public ICommand SearchCommand { get; private set; }

        public FriendsListViewModel()
        {
            SearchCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new SearchPage()));
            InitFriends();    
        }

        private async void InitFriends()
        {
            var user = App.User;
            foreach (string friendId in user.Friends)  
            {
                var friend = await UserManager.Instance.GetUsersWhere(x => x.Id == friendId);
                if (friend[0] != null)
                {
                    var friendToAdd = friend[0];
                    FriendsList.Add(friendToAdd);
                }
            }
        }
    }
}