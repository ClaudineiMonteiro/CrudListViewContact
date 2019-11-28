using LanceUpContactList.Models;
using LanceUpContactList.Services.Interfaces;
using LanceUpContactList.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LanceUpContactList.ViewModels
{
	public class ContactViewModel: BaseViewModel
	{
		public ObservableCollection<Contact> Contacts { get; set; }
		public Command LoadContactsCommand { get; set; }
		
		public ContactViewModel()
		{
			Title = "Contact List";
			Contacts = new ObservableCollection<Contact>();

			LoadContactsCommand = new Command(async () => await ExecuteLoadContactsCommand());

			MessagingCenter.Subscribe<ContactPage, Contact>(this, "SaveContact", async (obj, contact) =>
			{
				await ContactService.SaveAsync((Contact)contact);
			});

			MessagingCenter.Subscribe<ContactPage, Contact>(this, "DeleteContact", async (obj, contact) =>
			{
				if (await Application.Current.MainPage.DisplayAlert("Attention", "Are you sure you want to delete the Contact?", "Yes", "No"))
				{
					await ContactService.DeleteAsync(contact.Phone);
				}
			});
		}

		private async Task ExecuteLoadContactsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var contacts = await ContactService.ListAsync();
				Contacts.Clear();
				foreach (var contactItem in contacts)
				{
					Contacts.Add(contactItem);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
