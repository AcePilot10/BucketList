﻿using BucketList.Api.Managers;
using BucketList.Mobile.ViewModels.Base;
using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels.Login
{
    public class RegisterPageViewModel : ViewModel
    {
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
        }

        private async void Register()
        {
            Loading = true;
            if (Password == ConfirmPassword)
            {
                if ((await UserManager.Instance.GetUsersWhere(x => x.Email == Email)).Count == 0)
                {
                    await UserManager.Instance.RegisterUser(Username, Email, Password);
                    Email = "";
                    Password = "";
                    ConfirmPassword = "";
                    Username = "";
                    await Application.Current.MainPage.DisplayAlert("Registration Complete", "Succesfully Registered!", "Login");
                    Login();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erorr", "That email is already taken!", "Return");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erorr", "Password and Confirm Password must match!", "Return");
            }
            Loading = false;
        }

        private async void Login()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}