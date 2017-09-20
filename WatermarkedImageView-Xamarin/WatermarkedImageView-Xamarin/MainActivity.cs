using Android.App;
using Android.Widget;
using Android.OS;
using WatermarkedImageView_Xamarin.Views;

namespace WatermarkedImageView_Xamarin
{
    [Activity(Label = "WatermarkedImageView_Xamarin", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
        }
    }
}

