using System;
namespace ShareAble.Interfaces
{
	public interface IContactsService
	{
        Task<List<Contact>> GetAppContacts();

    }
}

