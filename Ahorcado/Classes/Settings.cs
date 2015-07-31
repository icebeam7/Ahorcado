using System;
using System.Collections.Generic;

namespace Ahorcado
{
	public static class Settings
	{
		public static Random generator;
		public static int maxTime = 60;
		public static int maxMistakes = 7;
		public static string messageLose = "You lose";
		public static string messageWin = "You win";
		public static string messageStart = "Pick a letter";
		public static List<Question> Questions;
		public static int Points = 0;
		public static string alphabet = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public static int columns = 5;
		public static double size = 42;

	}
}

