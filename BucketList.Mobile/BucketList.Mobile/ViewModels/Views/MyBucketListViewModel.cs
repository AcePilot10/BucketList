using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class MyBucketListViewModel
    {
        public ObservableCollection<BucketListItem> Items { get; set; } = new ObservableCollection<BucketListItem>();
        public ICommand CreateListItemCommand { get; private set; }

        public MyBucketListViewModel()
        {
            CreateListItemCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new CreateBucketListItemPage()));
        }

        public async void LoadItems()
        {
            Items.Clear();
            User user = App.User;
            var items = await UserManager.Instance.GetItems(user.ID);
            items.ForEach(x => Items.Add(x));
        }
    }
}