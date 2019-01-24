using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList.Mobile.Views.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage (User user)
		{
			InitializeComponent ();
            BindingContext = new ProfileViewModel(user);
		}

        private void listItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BucketListItem selectedItem = listItems.SelectedItem as BucketListItem;
            if (selectedItem == null) return;
            Application.Current.MainPage.Navigation.PushAsync(new PublicBucketListItemPage(selectedItem));
            listItems.SelectedItem = null;
        }
    }
}