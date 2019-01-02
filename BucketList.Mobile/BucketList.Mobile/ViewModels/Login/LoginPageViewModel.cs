using BucketList.Api.Managers;
using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Login;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class LoginPageViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(x => Application.Current.MainPage.Navigation.PushAsync(new RegisterPage()));
        }

        private async void Login()
        {
            var user = await UserManager.Instance.GetUserByEmail(Email);
            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login Error", "Could not find user with that email", "Return");
                return;
            }
            var result = await SignInManager.Instance.SignInUser(Email, Password);
            if (result != null && result.Succeeded)
            {
                ((App)Application.Current).User = user;
                Application.Current.MainPage = new NavigationPage(new FeedPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Error", "Invalid email or password", "Return");
                return;
            }           
        }
    }
}