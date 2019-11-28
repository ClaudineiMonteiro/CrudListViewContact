using LanceUpContactList.Models;
using LanceUpContactList.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LanceUpContactList.Views
{
	[DesignTimeVisible(false)]
	public partial class ContactListPage : ContentPage
	{
		private ContactViewModel contactViewModel;
		public ContactListPage()
		{
			InitializeComponent();

			BindingContext = contactViewModel = new ContactViewModel();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var contact = args.SelectedItem as Contact;

			if (contact == null)
				return;

			await Navigation.PushModalAsync(new NavigationPage(new ContactPage(contact)));

			ContactsListView.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			contactViewModel.LoadContactsCommand.Execute(null);
		}

		private async void Add_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new ContactPage(new Contact())));
		}
	}
}
