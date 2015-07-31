using System;
using System.Threading;
using Xamarin.Forms;

namespace Ahorcado
{
	public class TimerClass
	{
		TimerCallback delegado;
		Timer timer;
		int seconds;

		public TimerClass (Game game)
		{
			delegado = new TimerCallback(DecreaseOneSecond);
			seconds = Settings.maxTime;
			timer = new Timer(delegado, game, 0, 1000);
		}

		public void KillTimer(){
				timer.Dispose ();
		}

		public int GetSeconds(){
			return seconds;
		}

		private void DecreaseOneSecond(Object obj) {
			seconds--;

			if (seconds >= 0) {
				Device.BeginInvokeOnMainThread (() => {
					((Game)obj).UpdateTime(seconds);
					//((Label)((Game)obj).FindByName<Label>("lblClock")).Text = seconds.ToString ("00");
				});
			}

			if (seconds <= 0) {
				Device.BeginInvokeOnMainThread (() => {
					((Game)obj).UpdateMistakes(Settings.maxMistakes);
				});
			}
		}
	}
}

