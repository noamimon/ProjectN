using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;

namespace Contacts_ListView
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText etName;
        EditText etEmail;
        EditText etPhone;

        Button btnAdd;
        Button btnGet;
        Button btnUpd, btDelete;
        ListView lv;
        List<Person> persons;
        CustomAdapter myAdapter;
        DatabaseHelper helper = new DatabaseHelper();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            
            


            btnAdd = FindViewById<Button>(Resource.Id.btAdd);
            btnGet = FindViewById<Button>(Resource.Id.btGet);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etPhone = FindViewById<EditText>(Resource.Id.etPhone);
            btDelete = FindViewById<Button>(Resource.Id.btDelete);
            btnUpd = FindViewById<Button>(Resource.Id.btUpdate);
            lv = FindViewById<ListView>(Resource.Id.listView1);


            btnAdd.Click += BtnAdd_Click;
            btnGet.Click += BtnGet_Click;
            btDelete.Click += BtDelete_Click;
            btnUpd.Click += BtnUpd_Click;
            lv.ItemClick += Lv_Click;


            persons = new List<Person>();
            myAdapter = new CustomAdapter(this, persons);
            lv.Adapter = myAdapter;

            LoadPeople();
        }

        private async void LoadPeople()
        {
            List<Person> allPersons = await helper.GetAllPersons();
            persons.AddRange(allPersons);
            RunOnUiThread(() => myAdapter.NotifyDataSetChanged());
        }

        private void Lv_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            CustomAdapter adapter = new CustomAdapter(this, persons);
            lv.Adapter = adapter;
            Person pSelected =adapter[e.Position];
           Intent intent = new Intent(this, typeof(Activity_Msgs));
            intent.PutExtra("Email", pSelected.Email);
            StartActivity(intent);
            

        }


        private async void BtnUpd_Click(object sender, System.EventArgs e)
        {
            await helper.UpdatePerson(etName.Text, etEmail.Text, etPhone.Text);
            Toast.MakeText(this, "update", ToastLength.Short).Show();
        }

        private async void BtDelete_Click(object sender, System.EventArgs e)
        {
            await helper.DeletePerson(etEmail.Text);
            Toast.MakeText(this, "delete", ToastLength.Short).Show();
        }

        private async void BtnGet_Click(object sender, System.EventArgs e)
        {
            Person person = await helper.GetPerson(etEmail.Text);
            etName.Text = person.Name;
            etPhone.Text = person.Phone;
            etEmail.Text = person.Email;
            Toast.MakeText(this, "got", ToastLength.Short).Show();
        }

        private async void BtnAdd_Click(object sender, System.EventArgs e)
        {
            await helper.AddPerson(etName.Text, etEmail.Text, etPhone.Text);
            persons.Add(new Person
            {
                Name = etName.Text,
                Email = etEmail.Text,
                Phone = etPhone.Text
            });
            myAdapter.NotifyDataSetChanged();
            Toast.MakeText(this, "added", ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}