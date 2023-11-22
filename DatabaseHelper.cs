using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using System.Threading.Tasks;
using Firebase.Database.Query;

namespace Contacts_ListView
{
    class DatabaseHelper
    {
        private FirebaseClient fb = new FirebaseClient("https://noaapp-b2b1b-default-rtdb.firebaseio.com/");
        
        public async Task<List<Person>> GetAllPersons()
        {
            return (await fb.Child("persons").OnceAsync<Person>()).Select(p => p.Object).ToList();
        }
        
        public async Task AddPerson(string name, string email, string phone)
        {
            await fb.Child("persons").PostAsync(new Person
            {
                Name = name,
                Email = email,
                Phone = phone
            });
            // post=add, delete, put=update, once=get
        }

        public async Task<Person> GetPerson(string email)
        {
            List<Person> allPersons = await GetAllPersons();
            return allPersons.Where(a => a.Email == email).FirstOrDefault();
        }

        public async Task UpdatePerson(string name, string email, string phone)
        {
            string key = await GetPersonKey(email);
            await fb.Child("persons").Child(key).PutAsync(new Person
            {
                Name = name,
                Email = email,
                Phone = phone
            });
        }

        public async Task DeletePerson(string email)
        {
            string key = await GetPersonKey(email);
            await fb.Child("persons").Child(key).DeleteAsync();
        }


        private async Task<string> GetPersonKey(string email)
        {
            return (await fb.Child("persons").OnceAsync<Person>()).Where(p => p.Object.Email == email).FirstOrDefault().Key;
        }


    }
}