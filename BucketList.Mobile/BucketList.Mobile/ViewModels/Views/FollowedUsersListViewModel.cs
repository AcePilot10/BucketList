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
    public class FollowedUsersListViewModel
    {
        public ObservableCollection<User> FriendsList { get; set; } = new ObservableCollection<User>();
        public ICommand SearchCommand { get; private set; }

        public FollowedUsersListViewModel()
        {
            SearchCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new SearchPage()));
        }

        public async void UpdateFollowedUsers()
        {
            FriendsList.Clear();

            var user = App.User;

            foreach (var friendId in user.GetFollowedUsers())
            {
                var friend = await UserManager.Instance.GetUsersWhere(x => x.ID == friendId);
                if (friend[0] != null)
                {
                    var friendToAdd = friend[0];
                    FriendsList.Add(friendToAdd);
                }
            }
        }
    }
}