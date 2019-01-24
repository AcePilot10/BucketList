using BucketList.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList.Mobile.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ((FeedViewModel)pageFeed.BindingContext).UpdateEvents();
            ((FollowedUsersListViewModel)pageFriends.BindingContext).UpdateFollowedUsers();
            ((MyBucketListViewModel)pageMyList.BindingContext).LoadItems();
        }

        private void btnSignout_Clicked(object sender, EventArgs e)
        {
            ((App)Application.Current).Signout();
        }
    }
}