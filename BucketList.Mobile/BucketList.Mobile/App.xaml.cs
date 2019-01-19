using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.Views;
using BucketList.Mobile.Views.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BucketList.Mobile
{
    public partial class App : Application
    {

        public static User User { get; set; }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            AutoLoginUser();
        }

        private async void AutoLoginUser()
        {
            if (Properties.ContainsKey("Email"))
            {
                string email = Properties["Email"].ToString();
                User user = await UserManager.Instance.GetUserByEmail(email);
                User = user;
                MainPage = new NavigationPage(new HomePage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}