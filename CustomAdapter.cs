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
    public class CustomAdapter : BaseAdapter<Person>
    {
        private Activity activity;
        private List<Person> persons;
        public CustomAdapter(Activity activity, List<Person> persons)
        {
            this.activity = activity;
            this.persons = persons;
        }

        public override Person this[int position] => persons[position];

        public override int Count => persons.Count;
        
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = activity.LayoutInflater.Inflate(Resource.Layout.List_view_data, parent, false);

            var tvName = view.FindViewById<TextView>(Resource.Id.tvName);
            var tvPhone = view.FindViewById<TextView>(Resource.Id.tvPhone);
            var tvEmail = view.FindViewById<TextView>(Resource.Id.tvEmail);

            tvName.Text = persons[position].Name;
            tvPhone.Text = persons[position].Phone;
            tvEmail.Text = persons[position].Email;
            return view;
        }
    }

}