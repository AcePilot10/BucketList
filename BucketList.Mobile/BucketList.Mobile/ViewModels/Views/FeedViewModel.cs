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

        public FeedViewModel()
        {
            InitEvents();
        }

        private async void InitEvents()
        {
            var events = await UserManager.Instance.GetUserEvents(App.User.ID);
            events.ForEach(x => Events.Add(x));
        }
    }
}