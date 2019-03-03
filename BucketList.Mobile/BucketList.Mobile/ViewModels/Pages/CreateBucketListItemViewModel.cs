using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.Views.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BucketList.Mobile.ViewModels
{
    public class CreateBucketListItemViewModel
    {        
        public ICommand CreateCommand { get; private set; }

        public string Body { get; set; }
        public DateTime TargetDate { get; set; }

        public CreateBucketListItemViewModel()
        {
            CreateCommand = new Command(Create);
        }

        private async void Create()
        {
            BucketListItem item = new BucketListItem()
            {
                Created = DateTime.Now,
                Item = Body,
                Status = StatusConstants.IN_PROGRESS,
                TargetDate = TargetDate.Date,
                UserId = App.User.ID
            };
            //await ProfileManager.Instance.CreateListItem(Body, App.User.ID);
            await ProfileManager.Instance.CreateListItem(item);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}