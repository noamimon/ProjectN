using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contacts_ListView
{
    [Activity(Label = "Activity_Msgs")]
    public class Activity_Msgs : Activity
    {
        ListView lv;
        List<Person> persons;
        CustomAdapterMsg myAdapter;
        DatabaseHelperMsg helper1 = new DatabaseHelperMsg();
        DatabaseHelper helper2 = new DatabaseHelper();
        Button btSend;
        EditText message;
        List<Msg> msgs;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SendGetMsg);


            // Create your application here
            myAdapter = new CustomAdapterMsg(this, msgs);
            btSend = FindViewById<Button>(Resource.Id.btsend);
            message= FindViewById<EditText>(Resource.Id.etmsg);
            
            lv = FindViewById<ListView>(Resource.Id.listView2);
            //string emailItem = Intent.GetStringExtra("Email");
            btSend.Click += BtSend_Click;
        }
        private async void AddToEmail(string toEmail)
        {
            string emailItem = Intent.GetStringExtra("Email");
            await helper1.AddToEmail(emailItem);
            msgs.Add(new Msg
            {
                ToEmail = toEmail

            }); ;
        }
        private async void BtSend_Click(object sender, EventArgs e)
        {
            string emailItem = Intent.GetStringExtra("Email");
            if (emailItem == null)
            {
                Console.WriteLine("fff");
            }
            AddToEmail(emailItem);
            
            await helper1.UpdateMsg(emailItem, message.Text);
            myAdapter.NotifyDataSetChanged();
            Toast.MakeText(this, "sent", ToastLength.Short).Show();
        }
    }
}