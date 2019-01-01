using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class SearchViewModel
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public ICommand SearchCommand { get; private set; }

        public SearchViewModel()
        {
            SearchCommand = new Command<string>(Search);
        }

        public void Search(string query)
        {
            
        }
    }
}