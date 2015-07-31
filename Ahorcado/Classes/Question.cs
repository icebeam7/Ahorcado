using System;

using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Ahorcado
{
	public class Question
	{
		public string Code {
			get;
			set;
		}

		public string Text {
			get;
			set;
		}

		public bool Status {
			get;
			set;
		}

		public static void GetQuestions(int option)
		{
			string json = "";

			switch(option){
				case 1:
					string url = "https://api.myjson.com/bins/2ooi6";
					HttpClient httpClient = new HttpClient ();
					json = httpClient.GetStringAsync (url).Result;
				break;
				case 2:
					#if __IOS__
					var resourcePrefix = "Ahorcado.iOS.";
					#endif
					#if __ANDROID__
					var resourcePrefix = "Ahorcado.Droid.";
					#endif
					#if WINDOWS_PHONE
					var resourcePrefix = "Ahorcado.WinPhone.";
					#endif

					Assembly assembly = typeof(Game).GetTypeInfo().Assembly;
					Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "questions.json");
					using (StreamReader reader = new System.IO.StreamReader (stream)) {
						json = reader.ReadToEnd ();
					}
				break;
			}

			List<Question> list = JsonConvert.DeserializeObject<List<Question>> (json);

			foreach (var question in list) {
				question.Text = question.Text.ToUpper ();
			}

			Settings.Questions = list;
		}
	}
}