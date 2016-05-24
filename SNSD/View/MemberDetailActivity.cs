namespace SNSD.View
{
    using System.Threading.Tasks;

    using Android.App;
    using Android.OS;
    using Android.Widget;

    using SNSD.Data_Service;
    using SNSD.Helper;

    [Activity]
    public class MemberDetailActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Set view from Resource -> Layout
            this.SetContentView(Resource.Layout.DetailMember);

            //Parse data from activity before
            var memberName = this.Intent.GetStringExtra("MemberName");
            var service = new DataService();
            var result = await service.SendDetailMemberRequest(memberName);
            //validation
            if (result != null)
            {
                this.FindViewById<TextView>(Resource.Id.MemberName).Text = result.Name;
                this.FindViewById<TextView>(Resource.Id.MemberFullDescription).Text = result.FullDescription;

                if (!string.IsNullOrWhiteSpace(result.ImageProfileUrl))
                {
                    //Run download image in background thread
                    Task.Factory.StartNew(
                        () =>
                            {
                                var bmp = ImageHelper.GetImageBitmapFromUrl(result.ImageProfileUrl);
                                //Change image in UI thread
                                this.RunOnUiThread( 
                                    () => this.FindViewById<ImageView>(Resource.Id.ProfileImage).SetImageBitmap(bmp));
                            });
                }
            }
        }
    }
}