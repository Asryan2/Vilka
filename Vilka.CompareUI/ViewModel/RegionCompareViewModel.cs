using GalaSoft.MvvmLight;

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

		}

		public string RegionOne { get; set; } = "123";
		public string RegionTwo { get; set; } = "456";


	}
}