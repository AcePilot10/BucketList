using BucketList.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class SearchViewModel
    {
        public ICommand SearchCommand { get; private set; }
        
        public string Query { get; set; }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public SearchViewModel()
        {
            SearchCommand = new Command(Search);    
        }

        private void Search()
        {

        }
    }
}