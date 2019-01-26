using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class ProfileViewModel : ViewModel
    {
        private User _user;
        public User User
        {
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

        public ObservableCollection<BucketListItem> Items { get; set; } = new ObservableCollection<BucketListItem>();

        private bool _userIsNotFollowing;
        public bool UserIsNotFollowing
        {
            get
            {
                return _userIsNotFollowing;
            }
            set
            {
                _userIsNotFollowing = value;
                RaisePropertyChanged("UserIsNotFollowing");
            }
        }

        public ICommand FollowCommand { get; set; }
        public ICommand UnfollowCommand { get; set; }

        public ProfileViewModel(User user)
        {
            User = user;
            CheckIsFollowing();
            FollowCommand = new Command(Follow);
            UnfollowCommand = new Command(Unfollow);
            LoadItems();
        }

        private void CheckIsFollowing()
        {
            var followers = App.User.GetFollowedUsers();
            if (followers.Contains(User.ID))
                UserIsNotFollowing = false;
            else
                UserIsNotFollowing = true;
        }

        private async void Follow()
        {
            var status = await ProfileManager.Instance.Follow(App.User.ID, User.ID);
            UserIsNotFollowing = false;
            App.UpdateUser();
        }

        private async void Unfollow()
        {
            var followedUsers = App.User.GetFollowedUsers();
            if (followedUsers.Contains(User.ID))
            {
                followedUsers.Remove(User.ID);
                App.User.FollowedUsersJson = JsonConvert.SerializeObject(followedUsers);
                ProfileManager.Instance.SaveUser(App.User);
                App.UpdateUser();
                await Application.Current.MainPage.DisplayAlert("Success", "You have unfollow " + User.Username, "Return");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You don't follow this user!", "Return");
            }
        }

        private async void LoadItems()
        {
            var items = await UserManager.Instance.GetItems(User.ID);
            items.ForEach(x => Items.Add(x));
        }
    }
}