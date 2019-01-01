using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class RegisterPageViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(Register);
            LoginCommand = new Command(x => Application.Current.MainPage.Navigation.PopAsync()); ;
        }

        private async void Register()
        {

        }
    }
}
