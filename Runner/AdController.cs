using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Interstitial;
using Android.Views;
using Android.Widget;
using Runner;

namespace AdController
{
    public static class AdController
    {
        public static InterstitialAd interstitialHandle = null;
        public static AdView AdView = null;

        public static event EventHandler AdClosed;

        // AD ID's This is a test use yours from Admob
        public static string adID1 = "ca-app-pub-3940256099942544/1033173712"; // standard full page ad
        public static string adID3 = "ca-app-pub-3940256099942544/6300978111"; // banner ad

        public static bool adRegularLoaded = false;
        public static bool adBannerLoaded = false;

        public static void InitBannerAd(FrameLayout fl)
        {
            LinearLayout ll = new LinearLayout((Activity1)Game1.Activity)
            {
                Orientation = Orientation.Vertical
            };

            ll.SetGravity(GravityFlags.CenterVertical | GravityFlags.Bottom);

            MobileAds.Initialize(Game1.Activity);

            AdView = new AdView((Activity1)Game1.Activity)
            {
                AdUnitId = adID3,
                AdSize = AdSize.Banner
            };

            ListeningBanner listening = new ListeningBanner();

            AdView.AdListener = listening;
            AdView.LoadAd(new AdRequest.Builder().Build());
            AdView.Visibility = ViewStates.Visible;

            ll.AddView(AdView);
            fl.AddView(ll);
        }



        private static void Listening_AdClosed(object sender, EventArgs e)
        {
            AdClosed?.Invoke(null, null);
        }

    }

    internal class ListeningBanner : AdListener
    {
        public override void OnAdFailedToLoad(LoadAdError p0)
        {
            base.OnAdFailedToLoad(p0);
        }

        public override void OnAdLoaded()
        {
            AdController.adBannerLoaded = true;
            base.OnAdLoaded();
        }

        public override void OnAdClosed()
        {
            AdController.AdView.LoadAd(new AdRequest.Builder().Build());
            base.OnAdClosed();
        }
    }
}