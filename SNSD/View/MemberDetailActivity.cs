namespace SNSD.View
{
    using System.Threading.Tasks;

    using Android.App;
    using Android.OS;
    using Android.Widget;

    using SNSD.Helper;

    [Activity   ]
    public class MemberDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Set view from Resource -> Layout
            this.SetContentView(Resource.Layout.DetailMember);
            
            //Parse data from activity before
            var memberName = this.Intent.GetStringExtra("MemberName");
            var memberFullDescription = this.Intent.GetStringExtra("MemberFullDescription");
            var memberProfile = this.Intent.GetStringExtra("MemberProfile");
            this.FindViewById<TextView>(Resource.Id.MemberName).Text = memberName;
            this.FindViewById<TextView>(Resource.Id.MemberFullDescription).Text = memberFullDescription;

            
            if (!string.IsNullOrWhiteSpace(memberProfile))
            {
                //Run download image in background thread
                Task.Factory.StartNew(
                    () =>
                        {
                            var bmp = ImageHelper.GetImageBitmapFromUrl(memberProfile);
                            //Change image in UI thread
                            this.RunOnUiThread(
                                () => this.FindViewById<ImageView>(Resource.Id.ProfileImage).SetImageBitmap(bmp));
                        });
            }
        }
    }
}