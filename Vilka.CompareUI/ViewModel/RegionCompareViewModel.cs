using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
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
	public class RegionCompareViewModel : ViewModelBase
	{
		/// <summary>
		/// Initializes a new instance of the RegionCompareViewModel class.
		/// </summary>
		public RegionCompareViewModel()
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
			this.ShowNext();
		}

		private void Reject()
		{
			this.ShowNext();
		}

		private void ShowNext()
		{
			SimpleIoc.Default.GetInstance<IDataService>().ComperatorGetNextRegion((r1, r2) => NextRegionsArrived(r1, r2));
		}

		private void NextRegionsArrived(string r1, string r2)
		{
			RegionOne = r1;
			RegionTwo = r2;
		}

		public string RegionOne { get; set; } = "Inverness Caledonian Thistle";
		public string RegionTwo { get; set; } = "456";


	}
}