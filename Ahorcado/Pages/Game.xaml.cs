using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Ahorcado
{
	public partial class Game : ContentPage
	{
		public Round round;
		TimerClass timer;

		public Game ()
		{
			InitializeComponent ();

			InitializeVariables ();
			StartNewGame ();
		}

		void InitializeVariables()
		{
			Resources = new GlobalStyles().Resources;

			for (int i = 0; i < Settings.alphabet.Length / Settings.columns; i++)
				grdButtons.RowDefinitions.Add (new RowDefinition () { Height = Settings.size });

			for (int i = 0; i < Settings.columns; i++) 
				grdButtons.ColumnDefinitions.Add (new ColumnDefinition () { Width = Settings.size });
		}

		void StartNewGame()
		{
			DrawLetters ();
			round = new Round ();
			ChangeResultMessage (Settings.messageStart);
			SetImage ();

			UpdateTime (Settings.maxTime);
			UpdateAnswer ();
			UpdatePoints ();
		}

		void DrawLetters()
		{
			grdButtons.Children.Clear ();

			foreach (char letter in Settings.alphabet)
				DrawButton(letter);
		}

		void StartTimer(){
			if (timer == null)
				timer = new TimerClass (this);
		}

		void DrawButton(char letter){
			Button btn = new Button();

			btn.Style = Resources ["Letter"] as Style;
			btn.Text = letter.ToString();
			btn.Clicked += Btn_Clicked;

			Grid.SetRow(btn, Utils.GetRow(letter));
			Grid.SetColumn(btn, Utils.GetColumn(letter));

			grdButtons.Children.Add(btn);
		}

		public void ChangeResultMessage(string mensaje){
			Color color = (!round.CanPlay) 
				? (round.Result) 
					? Color.Green 
					: Color.Red 
				: Color.FromHex ("#00FFFF");

			lblResult.TextColor = color;
			lblAnswer.TextColor = color;
			lblResult.Text = mensaje;
		}

		void UpdateAnswer(){
			lblAnswer.Text = round.Answer;
		}

		void UpdatePoints(){
			Settings.Points += round.Points;
			lblPoints.Text = Settings.Points.ToString();
		}


		void Btn_Clicked (object sender, EventArgs e)
		{
			if (round.CanPlay) {
				StartTimer ();
				Button btn = (Button)sender;
				char letter = btn.Text [0];
				string answerNew = "";

				if (Utils.IsValidLetter (letter, round.Question.Text, round.Answer, out answerNew)) {
					round.UpdateAnswer(answerNew);
					UpdateAnswer();

					if (round.Result) {
						round.UpdatePoints(timer.GetSeconds());
						StopGame();
						ChangeResultMessage(Settings.messageWin);
						ShowAlert ();
						UpdatePoints ();
					}
				} else {
					UpdateMistakes (1);
				}

				btn.IsVisible = false;
			}
		}

		public void UpdateMistakes(int mistakes){
			if (round.UpdateMistakes(mistakes) == Settings.maxMistakes) {
				StopGame();
				Lose ();
			}

			SetImage ();
		}

		void ShowAlert(){
			DisplayAlert("Congratulations!", 
				String.Format("\t{0:000}\tLetters\n*\t{1:000}\tSeconds remaining\n-\t{2:000}\tMistakes\n\t{4}\n\t{3:000}\tis your score",
					round.Question.Text.Length,
					int.Parse (lblClock.Text),
					round.Mistakes * 10,
					round.Points,
					new string('-', 40)), 
				"OK");
		}


		public void SetImage(){
			string source = String.Format ("ahorcado{0}.png", round.Mistakes);
			image.Source = ImageSource.FromFile (source);
		}

		public void StopGame()
		{
			round.ContinuePlaying(false);
			timer.KillTimer ();
		}

		public void Lose(){
			round.UpdateResult(false);
			ChangeResultMessage(Settings.messageLose);
			UpdateAnswer();
		}

		void btnPlay_Clicked(object sender, EventArgs e){
			timer.KillTimer ();
			timer = null;
			StartNewGame ();
		}

		public void UpdateTime(int seconds){
			lblClock.Text = seconds.ToString ("00");
		}
	}
}