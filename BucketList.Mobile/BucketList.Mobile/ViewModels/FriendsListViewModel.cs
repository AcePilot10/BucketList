using BucketList.Api.Managers;
using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class FriendsListViewModel
    {
        public ObservableCollection<User> Friends { get; set; } = new ObservableCollection<User>();

        public FriendsListViewModel()
        {
            InitFriends();    
        }

        private async void InitFriends()
        {
            var user = ((App)Application.Current).User;
            foreach (string friendId in user.Friends)  
            {
                var friend = await UserManager.Instance.GetUsersWhere(x => x.Id == friendId);
                if (friend[0] != null)
                {
                    Friends.Add(friend[0]);
                }
            }
        }
    }
}