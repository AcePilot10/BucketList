using BucketList.Api.Managers;
using BucketList.Events.UserEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class FeedViewModel
    {
        public ObservableCollection<UserEvent> Events { get; set; } = new ObservableCollection<UserEvent>();

        public async void UpdateEvents()
        {
            Events.Clear();
            var events = await UserManager.Instance.GetAllEvents();
            foreach (var x in events)
            {
                if (x.UserId != App.User.ID && App.User.GetFollowedUsers().Contains(x.UserId)) 
                {
                    Events.Add(x);
                }
            }
        }
    }
}