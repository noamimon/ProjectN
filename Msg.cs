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
    class Msg
    {
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string toEmail;

        public string ToEmail
        {
            get { return toEmail; }
            set { toEmail = value; }
        }



    }
}