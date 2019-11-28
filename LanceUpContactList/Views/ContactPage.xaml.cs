using LanceUpContactList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LanceUpContactList.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactPage : ContentPage
	{
		public Contact Contact { get; set; }
		public ContactPage(Contact contact)
		{
			InitializeComponent();

			BindingContext = Contact = contact;
		}

		private async void Cancel_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		private async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "SaveContact", Contact);
			await Navigation.PopModalAsync();
		}

		private async void Delete_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "DeleteContact", Contact);
			await Navigation.PopModalAsync();
		}
	}
}