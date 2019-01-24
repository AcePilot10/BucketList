using BucketList.Entities.Models;
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
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent ();
            BindingContext = new SearchPageViewModel();
		}

        private void listResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listResults.SelectedItem == null) return;
            var selectedItem = listResults.SelectedItem;
            User user = selectedItem as User;
            Application.Current.MainPage.Navigation.PushAsync(new ProfilePage(user));
            listResults.SelectedItem = null;
        }
    }
}