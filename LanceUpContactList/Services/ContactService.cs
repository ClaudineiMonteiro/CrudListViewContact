using LanceUpContactList.Helpers;
using LanceUpContactList.Models;
using LanceUpContactList.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanceUpContactList.Services
{
	public class ContactService : IContactService<Contact>
	{
		private readonly List<Contact> Contacts;

		public ContactService()
		{
			Contacts = new List<Contact>();
			RestoreFile();
		}

		public async Task<bool> AddAsync(Contact contact)
		{
			Contacts.Add(contact);
			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateAsync(Contact contact)
		{
			var contactOld = await GetAsync(contact.Phone);

			Contacts.Remove(contactOld);

			Contacts.Add(contact);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteAsync(string phone)
		{
			var contactOld = await GetAsync(phone);

			Contacts.Remove(contactOld);

			return await Task.FromResult(true);
		}

		public async Task<Contact> GetAsync(string phone)
		{
			return await Task.FromResult(Contacts.FirstOrDefault(c => c.Phone == phone));
		}

		public async Task<IEnumerable<Contact>> ListAsync()
		{
			return await Task.FromResult(Contacts);
		}

		public async Task<bool> SaveAsync(Contact contact)
		{
			var contactFound = Contacts.FirstOrDefault(c => c.Phone == contact.Phone);

			if (contactFound == null)
			{
				await AddAsync(contact);
			}
			else
			{
				await UpdateAsync(contact);
			}

			SaveFile();

			return await Task.FromResult(true);
		}

		public void SaveFile()
		{
			var teste = JsonHelper.Serializer(Contacts);
			StorageHelper.WriteFile("ContactList.json", teste);
		}

		public void RestoreFile()
		{
			string contactJson = StorageHelper.ReadFile("ContactList.json");

			if (string.IsNullOrWhiteSpace(contactJson)) return;

			var contacts = JsonHelper.UnSerializer(contactJson);

			Contacts.Clear();
			foreach (var contact in contacts)
			{
				Contacts.Add((Contact)contact);
			}
		}
	}
}
