using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BucketList.Mobile.Ads;
using BucketList.Mobile.Droid.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdControlView),
    typeof(AdViewRenderer))]
namespace BucketList.Mobile.Droid.Ads
{
    public class AdViewRenderer : 
        ViewRenderer
    {

        private string adUnitId = "ca-app-pub-9220085631369979/3416872155";
        private AdSize adSize = AdSize.SmartBanner;
        private AdView adView;

        public AdViewRenderer(Context context) : base(context)
        {
            CreateAdView();
        }

        private AdView CreateAdView()
        {
            if (adView != null) return adView;

            adView = new AdView(Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;
            var adParams = new LinearLayout.LayoutParams(
                LayoutParams.WrapContent, LayoutParams.WrapContent);
            adView.LayoutParameters = adParams;

            var adRequest = new AdRequest
                                .Builder()
                                //.AddTestDevice("E4E02EAD66BB1600BB0A792486C3EF0A")
                                .Build();
            adView.LoadAd(adRequest);
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                CreateAdView();
                SetNativeControl(adView);
            }
        }
    }
}