using FluentValidation;
using LanceUpContactList.Helpers;
using LanceUpContactList.Models;
using LanceUpContactList.Notifications.Interfaces;
using LanceUpContactList.Services.Interfaces;
using LanceUpContactList.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LanceUpContactList.Services
{
	public class ContactService : IContactService<Contact>
	{
		private readonly List<Contact> Contacts;

		public ContactService()
		{
			//Contacts = new List<Contact>()
			//{
			//	new Contact{Id = Guid.NewGuid(), Phone = "9545041300", Name = "Claudinei Monteiro", RegistrationDate = DateTime.Now},
			//	new Contact{Id = Guid.NewGuid(), Phone = "9545051914", Name = "Sora Lub", RegistrationDate = DateTime.Now}
			//};
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
			var teste = JsonHelper.Serializer<IEnumerable<Contact>>(Contacts);
			StorageHelper.WriteFile("ContactList.json", teste);
		}

		public void RestoreFile()
		{
			string contactJson = StorageHelper.ReadFile("ContactList.json");

			var contacts = JsonHelper.UnSerializer<IEnumerable<Contact>>(contactJson);

			Contacts.Clear();
			foreach (var contact in contacts)
			{
				Contacts.Add((Contact)contact);
			}
		}

		private bool IsValid(Contact contact)
		{
			bool isValid = true;

			if (!PerformValidation(new ContactValidation(), contact)) isValid = false;

			if (Contacts.FirstOrDefault(c => c.Phone == contact.Phone) == null)
			{
				//Notify("There is already a Contact with this phone");
				isValid = false;
			}

			return isValid;
		}

		private bool PerformValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
		{
			var validator = validation.Validate(entity);

			if (validator.IsValid) return true;

			//Notify(validator);

			return false;
		}

	}
}
