using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BucketList.Mobile.ViewModels.Pages
{
    public class ProfileViewModel : ViewModel
    {
        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        public ProfileViewModel(User user)
        {
            User = user;
        }
    }
}