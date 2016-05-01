namespace SNSD.View
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Runtime;
    using Android.Support.V7.Widget;
    using Android.Views;
    using Android.Widget;

    using SNSD.Helper;
    using SNSD.Model;
    
    /// <summary>
    /// This class need to extend RecyclerView.Adapter so it can be used as recyclerview adapter
    /// </summary>
    public class MemberListAdapter : RecyclerView.Adapter
    {
        private readonly Activity _Activity;

        private readonly List<Member> _Members;

        public MemberListAdapter(List<Member> members, Activity activity)
        {
            //Passing member list to constructor
            this._Members = members;
            this._Activity = activity;
        }

        //Return members count
        public override int ItemCount => this._Members.Count;

        //Event when list item clicked
        public event EventHandler<int> ItemClick;

        private void OnClick(int position)
        {
            this.ItemClick?.Invoke(this, position);
        }

        //Parse item view in here
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            // Get element from your dataset at this position and replace the contents of the view
            // with that element
            if (this._Members.Count > 0)
            {
                // Get element from your dataset at this position and replace the contents of the view
                // with that element
                var vh = holder as ViewHolder;
                if (vh != null)
                {
                    var member = this._Members[position];
                    vh.Name.Text = member.Name;
                    vh.Description.Text = member.Description;
                    if (!string.IsNullOrWhiteSpace(member.ImageProfileUrl))
                    {
                        //Run download image in background thread
                        Task.Factory.StartNew(
                            () =>
                                {
                                    var bmp = ImageHelper.GetImageBitmapFromUrl(member.ImageProfileUrl);
                                    //Set image view in UI Thread
                                    this._Activity.RunOnUiThread(() => { vh.RequestImage.SetImageBitmap(bmp); });
                                });
                    }
                }
            }
        }

        //Define layout to be used as item view
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ItemMemberList, parent, false);
            var vh = new ViewHolder(v, this.OnClick);
            return vh;
        }

        // Holder class to parse element inside layout
        public class ViewHolder : RecyclerView.ViewHolder
        {
            public ViewHolder(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
            {
            }

            public ViewHolder(View itemView, Action<int> listener)
                : base(itemView)
            {
                this.Name = itemView.FindViewById<TextView>(Resource.Id.MemberName);
                this.Description = itemView.FindViewById<TextView>(Resource.Id.MemberDesc);
                this.RequestImage = itemView.FindViewById<ImageView>(Resource.Id.ProfileImage);

                itemView.Click += (sender, e) => listener(this.LayoutPosition);
            }

            public TextView Name { get; }

            public TextView Description { get; }

            public ImageView RequestImage { get; }
        }
    }
}