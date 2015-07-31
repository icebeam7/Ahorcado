using System;

namespace Ahorcado
{
	public class Round
	{
		private int _mistakes, _points;
		public Question _question;
		private string _answer;
		bool _result, _canPlay;

		public int Mistakes{
			get { 
				return _mistakes;
			}
			set { 
				if (_mistakes != value) {
					_mistakes = value;
				}
			}
		}

		public int Points{
			get { 
				return _points;
			}
			set { 
				if (_points != value) {
					_points = value;
					Settings.Points += _points;
				}
			}
		}

		public Question Question{
			get { 
				return _question;
			}
			set { 
				if (_question != value) {
					_question = value;
				}
			}
		}

		public string Answer{
			get { 
				return _answer;
			}
			set { 
				if (_answer != value) {
					_answer = value;
				}
			}
		}

		public bool Result{
			get { 
				return _result;
			}
			set { 
				if (_result != value) {
					_result = value;
				}
			}
		}

		public bool CanPlay{
			get { 
				return _canPlay;
			}
			set { 
				if (_canPlay != value) {
					_canPlay = value;
				}
			}
		}

		public Round ()
		{
			_mistakes = _points = 0;
			_result = false;
			_canPlay = true;
			_question = GetRandomQuestion ();
			_answer = Utils.ConvertTo_(_question.Text);
		}

		private Question GetRandomQuestion()
		{
			//TODO: Validate when there is no more questions remaining
			int num = 0;

			do {
				num = Settings.generator.Next (Settings.Questions.Count);
			} while(Settings.Questions [num].Status);

			return Settings.Questions [num];
		}

		public void UpdatePoints(int seconds){
			_points = _question.Text.Length * seconds - (10 * _mistakes);
			_question.Status = true;
		}

		public int UpdateMistakes(int m){
			_mistakes = (m == 1) ? _mistakes + 1 : m;
			return _mistakes;
		}

		public void ContinuePlaying(bool canPlay){
			this._canPlay = canPlay;
		}

		public void UpdateResult(bool result){
			_result = result;

			if (!_result)
				_answer = _question.Text;
		}

		public void UpdateAnswer(string answer){
			_answer = answer;
			_result = (_answer == _question.Text);
		}
	}
}

