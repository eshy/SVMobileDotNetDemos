using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace PicShare.Clients.UI.Droid.Views
{
    [Activity]
    public class SignUpView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignUpView);
        }
    }
}