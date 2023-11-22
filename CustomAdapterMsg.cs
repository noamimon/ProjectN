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
    class CustomAdapterMsg : BaseAdapter<Msg>
    {
        private Activity activity;
        private List<Msg> msgs;
        public CustomAdapterMsg(Activity activity, List<Msg> msgs)
        {
            this.activity = activity;
            this.msgs = msgs;
        }

        public override Msg this[int position] => msgs[position];

        public override int Count => msgs.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = activity.LayoutInflater.Inflate(Resource.Layout.List_view_data, parent, false);

            var messgae = view.FindViewById<TextView>(Resource.Id.etmsg);
            

            messgae.Text = msgs[position].Message;
           
            return view;
        }
    }

}