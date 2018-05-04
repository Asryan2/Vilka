using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using PostSharp.Patterns.Model;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Vilka.CompareUI.Model;

namespace Vilka.CompareUI.ViewModel
{
	/// <summary>
	/// This class contains properties that a View can data bind to.
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class LeagueCompareViewModel : ViewModelBase
    {


		/// <summary>
		/// Initializes a new instance of the RegionCompareViewModel class.
		/// </summary>
		public LeagueCompareViewModel()
		{
			ShowNext();
		}

		private RelayCommand _acceptCommand;
		public RelayCommand AcceptCommand
		{
			get
			{
				if (_acceptCommand == null)
					_acceptCommand = new RelayCommand(new Action(() =>
					{
						this.Accept();
					}));
				return _acceptCommand;
			}
		}

		private RelayCommand _rejectCommand;
		public RelayCommand RejectCommand
		{
			get
			{
				if (_rejectCommand == null)
					_rejectCommand = new RelayCommand(new Action(() =>
					{
						this.Reject();
					}));
				return _rejectCommand;
			}
		}

		private void Accept()
		{
            Answer?.Invoke(true);
            this.ShowNext();
		}

		private void Reject()
		{
            Answer?.Invoke(false);

            this.ShowNext();
		}

		private void ShowNext()
		{
			SimpleIoc.Default.GetInstance<IDataService>().ComperatorGetNextLeague((l1, l2, r, s, a) => NextLeaguesArrived(l1, l2, r, s,a));
		}

		private void NextLeaguesArrived(string l1, string l2, string region, string sport, Action<bool> a)
		{
            LeagueOne = l1;
            LeagueTwo = l2;
            PropertyChanged += (o,e) => { };
            Region = region;
            Sport = sport;
            Answer = a; 
		}

        private Action<bool> Answer;

        

        public string LeagueOne { get; set; }
		public string LeagueTwo { get; set; }
        public string Region { get; set; }
        public string Sport { get; set; }

	}
}