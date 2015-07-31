using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Ahorcado
{
	public partial class Start : ContentPage
	{
		public Start ()
		{
			InitializeComponent ();

			Question.GetQuestions (2);
		}

		void btnPlay_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync (new Game ());
		}
	}
}

