using BucketList.Api.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class ForgotPasswordViewModel
    {
        public ICommand SubmitCommand { get; set; }

        public ForgotPasswordViewModel()
        {
            SubmitCommand = new Command<string>(Submit);
        }

        private async void Submit(string email)
        {
            var result = await ProfileManager.Instance.RequestPasswordChange(email);
            if (result == "Accepted")
            {
                await Application.Current.MainPage.DisplayAlert("Reset Password", "Please check your inbox for an email regarding resetting your password.", "Ok");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No users found with email " + email, "Return");
            }
        }
    }
}