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
            MainPage = new SplashPage();
            if (Properties.ContainsKey("Email"))
            {
                AutoLoginUser();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            //MainPage = new AdsTest();
        }

        private async void AutoLoginUser()
        {
            string email = Properties["Email"].ToString();
            User user = await UserManager.Instance.GetUserByEmail(email);
            User = user;
            MainPage = new NavigationPage(new HomePage());
        }

        public async static void UpdateUser()
        {
            User = await UserManager.Instance.GetUserByEmail(User.Email);
        }

        public void Signout()
        {
            User = null;
            Properties["Email"] = null;
            Properties["Password"] = null;
            MainPage = new NavigationPage(new LoginPage());
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