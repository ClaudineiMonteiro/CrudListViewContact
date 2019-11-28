using LanceUpContactList.Services;
using LanceUpContactList.Views;
using Xamarin.Forms;

namespace LanceUpContactList
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			DependencyService.Register<ContactService>();

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
