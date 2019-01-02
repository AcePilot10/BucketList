using BucketList.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BucketList.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedView : ContentView
	{
		public FeedView ()
		{
			InitializeComponent ();
            BindingContext = new FeedViewModel();
		}
	}
}