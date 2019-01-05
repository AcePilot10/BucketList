using BucketList.Api.Managers;
using BucketList.Mobile.ViewModels.Base;
using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Login;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class LoginPageViewModel : ViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }

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

        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()));
        }

        private async void Login()
        {
            Loading = true;
            var user = await UserManager.Instance.GetUserByEmail(Email);
            if (user == null)
            {
                Loading = false;
                await Application.Current.MainPage.DisplayAlert("Login Error", "Could not find user with that email", "Return");
                return;
            }
            var result = await SignInManager.Instance.SignInUser(Email, Password);
            if (result != null && result.Succeeded)
            {
                Loading = false;
                ((App)Application.Current).User = user;
                Application.Current.MainPage = new NavigationPage(new FeedPage());
            }
            else
            {
                Loading = false;
                await Application.Current.MainPage.DisplayAlert("Login Error", "Invalid email or password", "Return");
                return;
            }
        }
    }
}