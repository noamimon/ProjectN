using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Database.Query;


namespace Contacts_ListView
{
    class DatabaseHelperMsg

    {
        private FirebaseClient fb = new FirebaseClient("https://noaapp-b2b1b-default-rtdb.firebaseio.com/");

        public async Task<List<Msg>> GetAllMsgs(string email)
        {
            string key = await GetMsgKey(email);
            return (await fb.Child("msg").Child(key).OnceAsync<Msg>()).Select(p => p.Object).ToList();
        }
        public async Task UpdateMsg(string ToEmail,string message)
        {
            string key = await GetMsgKey(ToEmail);
            await fb.Child("msg").Child(key).PutAsync(new Msg
            {
                Message = message,
                ToEmail = ToEmail,

            }) ;
        }
        public async Task AddMsg(string message)
        {
            await fb.Child("msg").PostAsync(new Msg
            {
                Message = message

            }); ;
            // post=add, delete, put=update, once=get
        }

        public async Task AddToEmail(string toEmail)
        {
            await fb.Child("msg").PostAsync(new Msg
            {
                ToEmail = toEmail

            }) ; ;
            // post=add, delete, put=update, once=get
        }
        /*public async Task UpdateMsg(string email,string msg)
        {
            string key = await GetMsgKey(email);
            await fb.Child("persons").Child(key).PutAsync(new Msg
            {
                Message = msg

            }); ;
        }*/
        private async Task<string> GetMsgKey(string email)
        {
            return (await fb.Child("msg").OnceAsync<Msg>()).Where(p => p.Object.Email == email).FirstOrDefault().Key;
        }

        
    }
}