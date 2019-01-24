using BucketList.Api.Managers;
using BucketList.Mobile.ViewModels.Base;
using BucketList.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class RegisterPageViewModel : ViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; } = "";

        private bool _loading = false;
        public bool Loading
        {
            get
            {
                return _loading;
            }
            set
            {
                _loading = value;
                RaisePropertyChanged("Loading");
            }
        }


        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(Register);
            LoginCommand = new Command(x => Application.Current.MainPage.Navigation.PopAsync()); ;
        }

        private async void Register()
        {
            Loading = true;
            await UserManager.Instance.RegisterUser(Username, Email, Password, ConfirmPassword);
            Loading = false;
            await Application.Current.MainPage.DisplayAlert("Registration Complete", "Succesfully Registered! You may now login.", "Login");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}