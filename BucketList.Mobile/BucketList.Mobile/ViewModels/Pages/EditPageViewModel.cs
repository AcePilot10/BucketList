using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class EditPageViewModel
    {
        public ICommand SaveCommand { get; set; }

        public EditPageViewModel()
        {
            SaveCommand = new Command(Save);
        }

        public async void Save()
        {

        }
    }
}