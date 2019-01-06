﻿using BucketList.Entities.Models;
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
	public partial class FriendsListView : ContentView
	{
        private FriendsListViewModel Model
        {
            get
            {
                return BindingContext as FriendsListViewModel;
            }
        }

		public FriendsListView ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            User selectedUser = e.SelectedItem as User;
            Application.Current.MainPage.Navigation.PushAsync(new Profile(selectedUser));
        }
    }
}