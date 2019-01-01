using BucketList.Api.Managers;
using BucketList.Mobile.Views;
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
            var result = await UserManager.Instance.RegisterUser(Username, Email, Password, ConfirmPassword);
            if (result.Succeeded)
            {
                await Application.Current.MainPage.DisplayAlert("Registration Complete", "Succesfully Registered! You may now login.", "Login");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var error = result.Errors[0];
                await Application.Current.MainPage.DisplayAlert("Registration Failed", "Error: " + error.Code + "/n" + error.Description, "Return");
            }
        }
    }
}
