using System;

using Xamarin.Forms;


namespace Ahorcado
{
	public class App : Application
	{
		public App ()
		{
			Resources = new GlobalStyles().Resources;
			Settings.generator = new Random ();

			MainPage = new NavigationPage (new Start ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

