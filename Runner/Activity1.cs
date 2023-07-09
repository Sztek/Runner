using AdController;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;
using Android.Widget;

namespace Runner
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.FullUser,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize
    )]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 _game;
        private View _view;
        private FrameLayout _frameLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _game = new Game1();
            //_view = _game.Services.GetService(typeof(View)) as View;
            _frameLayout = new FrameLayout(this);
            _frameLayout.AddView((View)_game.Services.GetService(typeof(View)));

            AdController.AdController.InitBannerAd(_frameLayout);

            SetContentView(_frameLayout);
            _game.Run();
        }
    }
}
