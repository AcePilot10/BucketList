﻿using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels;
using BucketList.Mobile.ViewModels.Pages;
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
	public partial class MyBucketListView : ContentView
	{

        private MyBucketListViewModel Model
        {
            get
            {
                return BindingContext as MyBucketListViewModel;
            }
        }

		public MyBucketListView ()
		{
            InitializeComponent();
		}

        private void listItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as BucketListItem;
            Model.SelectItem(item);
            listItems.SelectedItem = null;
        }
    }
}