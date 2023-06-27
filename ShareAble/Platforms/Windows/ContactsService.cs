using ShareAble.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShareAble.Services
{
    public class ContactsService : IContactsService
    {
        public ContactsService()
        {
        }



        public async Task<List<Contact>> GetAppContacts()
        {
            var appContacts = new List<Contact>();

            var contacts = await Microsoft.Maui.ApplicationModel.Communication.Contacts.GetAllAsync();

            foreach (var contact in contacts)
            {

                appContacts.Add(contact);

            }

            return appContacts;
        }


    }
}
