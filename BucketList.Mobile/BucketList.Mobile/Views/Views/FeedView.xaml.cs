using BucketList.Api.Managers;
using BucketList.Events.UserEvents;
using BucketList.Mobile.ViewModels;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedView : ContentView
	{
		public FeedView ()
		{
			InitializeComponent ();
            BindingContext = new FeedViewModel();
		}

        private async void listEvents_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            UserEvent userEvent = listEvents.SelectedItem as UserEvent;
            if (userEvent == null) return;
            var users = await UserManager.Instance.GetUsersWhere(x => x.ID == userEvent.UserId);
            await Application.Current.MainPage.Navigation.PushAsync(new ProfilePage(users[0]));
            listEvents.SelectedItem = null;
        }
    }
}