using LanceUpContactList.Models;
using LanceUpContactList.Notifications.Interfaces;
using LanceUpContactList.Services.Interfaces;
using LanceUpContactList.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanceUpContactList.Services
{
	public class ContactService : BaseService, IContactService
	{
		private readonly List<Contact> contacts;

		public ContactService(INotifier notifier) : base(notifier)
		{
			contacts = new List<Contact>();
		}

		public async Task<bool> AddAsync(Contact contact)
		{
			contacts.Add(contact);
			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateAsync(Contact contact)
		{
			var contactOld = await GetAsync(contact.Phone);

			contacts.Remove(contactOld);

			contacts.Add(contact);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteAsync(string phone)
		{
			var contactOld = await GetAsync(phone);

			contacts.Remove(contactOld);

			return await Task.FromResult(true);
		}

		public async Task<Contact> GetAsync(string phone)
		{
			return await Task.FromResult(contacts.FirstOrDefault(c => c.Phone == phone));
		}

		public async Task<IEnumerable<Contact>> ListAsync(bool forceRefresh = false)
		{
			return await Task.FromResult(contacts);
		}

		private bool IsValid(Contact contact)
		{
			bool isValid = true;

			if (!PerformValidation(new ContactValidation(), contact)) isValid = false;

			if (contacts.FirstOrDefault(c => c.Phone == contact.Phone) == null)
			{
				Notify("There is already a Contact with this phone");
				isValid = false;
			}

			return isValid;
		}
	}
}
