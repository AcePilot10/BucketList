using BucketList.Api.Managers;
using BucketList.Entities.Models;
using BucketList.Mobile.ViewModels.Pages;
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
	public partial class EditPage : ContentPage
	{
        private bool loaded = false;
        private BucketListItem item;

		public EditPage (BucketListItem item)
		{
            this.item = item;
			InitializeComponent ();
            BindingContext = new EditPageViewModel(item);
        }
    }
}