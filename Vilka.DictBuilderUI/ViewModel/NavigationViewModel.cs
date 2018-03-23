using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vilka.DictBuilderUI.ViewModel
{
	class NavigationViewModel : INotifyPropertyChanged

	{

		public ICommand NavigateRegionCompareCommand { get; set; }

		private object selectedViewModel;

		public object SelectedViewModel

		{

			get { return selectedViewModel; }

			set { selectedViewModel = value; }

		}



		public NavigationViewModel()

		{

			NavigateRegionCompareCommand = new RelayCommand(NavigateRegionCompare);

		}

		private void NavigateRegionCompare()

		{

			SelectedViewModel = new RegionCompareViewModel();

		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propName)

		{

			if (PropertyChanged != null)

			{

				PropertyChanged(this, new PropertyChangedEventArgs(propName));

			}

		}

	}
}
