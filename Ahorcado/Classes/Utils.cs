using System;
using System.Text;

namespace Ahorcado
{
	public static class Utils
	{
		public static string ConvertTo_(string answer){
			return new string ('-', answer.Length);
		}

		public static bool IsValidLetter(char letter, string question, string answerOld, out string answerNew){
			bool isValid = false;
			answerNew = answerOld;

			for (int index = 0; index < answerOld.Length; index++) {
				if (question [index] == letter) {
					isValid = true;
					answerNew = ReplaceChar (letter, index, answerNew);
				}
			}

			return isValid;
		}

		private static string ReplaceChar(char letter, int index, string answer){
			StringBuilder sb = new StringBuilder(answer);
			sb[index] = letter;
			return sb.ToString();
		}

		public static int GetRow(char letra){
			return Settings.alphabet.IndexOf(letra) / Settings.columns;
		}

		public static int GetColumn(char letra){
			return Settings.alphabet.IndexOf(letra) % Settings.columns;
		}
	}
}

