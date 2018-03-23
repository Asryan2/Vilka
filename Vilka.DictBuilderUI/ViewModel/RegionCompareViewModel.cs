using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.DictBuilderUI.ViewModel
{
	class RegionCompareViewModel:  INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public RegionCompareViewModel()
		{
			
		}

		public string FirstRegion
		{
			get;
			set;
		} = "123";

		public string SecondRegion
		{
			get;
			set;
		} = "456";

		protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			PropertyChanged?.Invoke(this, eventArgs);
		}
	}
}
