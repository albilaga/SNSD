namespace SNSD.View
{
    using System.Collections.Generic;

    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Support.V7.Widget;

    using SNSD.Data_Service;
    using SNSD.Model;

    [Activity(MainLauncher = true)]
    public class MemberListActivity : Activity
    {
        private MemberListAdapter _Adapter;

        private RecyclerView _ListMembers;

        private List<Member> _Members;

        //Create Dummy List
        private void InitDummy()
        {
            this._Members.Add(
                new Member
                    {
                        Description = " Leader, Main Vocalist",
                        Name = "Taeyeon",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/taeyeon.jpg",
                        FullDescription =
                            "Kim Tae-yeon (born March 9, 1989), better known by the mononym Taeyeon, is a South Korean singer and actress. She debuted in 2007 as a member of South Korean girl group Girls\' Generation under S.M. Entertainment, and has further participated in its subgroup TTS and SM the Ballad. She has also recorded singles for various television dramas and movies as well as participated in a number of variety shows. Taeyeon made her solo debut on October 8, 2015 with her first extended play, I."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Vocalist, Lead Dancer",
                        Name = "Yuri",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/yuri.jpg",
                        FullDescription =
                            "Kwon Yuri (born December 5, 1989) is a South Korean singer and actress. She is a member of South Korean girl group Girls\' Generation. She has also pursued acting, including the dramas Fashion King and Neighborhood Hero and the movie No Breathing."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Vocalist, Lead Dancer, Face of The Group",
                        Name = "Yoona",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/yoona.jpg",
                        FullDescription =
                            "Im Yoona (born May 30, 1990) is a South Korean singer and actress. She is a member of South Korean girl group Girls\' Generation and has participated in various television dramas such as You Are My Destiny (2008), Cinderella Man (2009), Love Rain (2012) and Prime Minister and I (2013)."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Lead Vocalist, Maknae",
                        Name = "Seohyun",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/seohyun.jpg",
                        FullDescription =
                            "Seo Ju-hyun (born June 28, 1991), known professionally as Seohyun, is a South Korean singer and actress. She is a member of South Korean girl group Girls\' Generation and its subgroup TTS."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Lead Vocalist",
                        Name = "Tiffany",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/tiffany.jpg",
                        FullDescription =
                            "Stephanie Young Hwang (born August 1, 1989), known professionally as Tiffany or Tiffany Hwang, is an American singer and actress based in South Korea. She is a member of South Korean girl group Girls\' Generation and its subgroup TTS."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Vocalist, Main Dancer",
                        Name = "Hyoyeon",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/hyoyeon.jpg",
                        FullDescription =
                            "Kim Hyo-yeon (born September 22, 1989), most often credited as Hyoyeon, is a South Korean pop singer. She is a member of South Korean girl group Girls\' Generation."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Vocalist, Lead Dancer",
                        Name = "Sooyoung",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/sooyoung.jpg",
                        FullDescription =
                            "Choi Soo-young (born February 10, 1990), most often credited as Sooyoung, is a South Korean singer and actress. She was a member of Korean-Japanese duo Route θ and is a member of South Korean girl group Girls\' Generation. Besides music activities, Sooyoung has also starred in various television dramas such as The 3rd Hospital (2012), Dating Agency: Cyrano (2013), and My Spring Days (2014)."
                    });
            this._Members.Add(
                new Member
                    {
                        Description = "Lead Vocalist",
                        Name = "Sunny",
                        ImageProfileUrl = "https://sumandu.files.wordpress.com/2009/10/sunny.jpg",
                        FullDescription =
                            "Lee Soon-kyu (born May 15, 1989), known professionally as Sunny, is an American-born South Korean singer and actress. She is a member of South Korean girl group Girls\' Generation. Throughout her career, Sunny has participated in numerous entertainment work including original soundtracks, television variety shows, musical acting and radio hosting."
                    });
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Use Layout for memberlist layout
            this.SetContentView(Resource.Layout.MemberList);

            this._Members = new List<Member>();
            //Create Dummy
            //this.InitDummy();
            var service = new DataService();
            this._Members = await service.SendListMembersRequest();

            //Assign recycler view in layout
            this._ListMembers = this.FindViewById<RecyclerView>(Resource.Id.ListMembers);
            //Set layout for recycler view to stack vertically
            this._ListMembers.SetLayoutManager(new LinearLayoutManager(this));

            this._Adapter = new MemberListAdapter(this._Members, this);

            //Event when item clicked
            this._Adapter.ItemClick += this.OnMemberClicked;

            //Set recycler view to use item view using this adapter
            this._ListMembers.SetAdapter(this._Adapter);
        }

        private void OnMemberClicked(object sender, int e)
        {
            //Create new intent
            var intent = new Intent(this, typeof(MemberDetailActivity));
            var member = this._Members[e];
            //Passing item to intent
            intent.PutExtra("MemberName", member.Name);
            //Start Activity to change to other activity
            this.StartActivity(intent);
            //Resource.Id.bu
        }
    }
}