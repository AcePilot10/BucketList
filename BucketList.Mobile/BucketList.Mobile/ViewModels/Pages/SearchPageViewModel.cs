﻿using BucketList.Api.Managers;
using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class SearchPageViewModel
    {

        public ObservableCollection<User> SearchResults { get; set; } = new ObservableCollection<User>();

        public ICommand SearchCommand { get; private set; }
        public string Query { get; set; } = "";

        public SearchPageViewModel()
        {
            SearchCommand = new Command(Search);
        }

        private async void Search()
        {
            if (Query == null) return;
            SearchResults.Clear();
            var users = await UserManager.Instance.GetUsersWhere(x =>
            {
                try
                {
                    return x.Username.ToLower().Contains(Query.ToLower());
                }
                catch (Exception)
                { 
                    return false;
                }
            });
            foreach (var user in users)
            {
                if (user == null) continue;
                if (user.ID != App.User.ID)
                {
                    SearchResults.Add(user);
                }
            }
        }
    }
}