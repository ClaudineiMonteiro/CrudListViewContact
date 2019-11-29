using LanceUpContactList.Services;
using LanceUpContactList.Views;
using System;
using System.IO;
using Xamarin.Forms;

namespace LanceUpContactList
{
	public partial class App : Application
	{
		public static string FolderPath { get; private set; }
		public App()
		{
			InitializeComponent();

			DependencyService.Register<ContactService>();

			FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

			MainPage = new NavigationPage(new ContactListPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
