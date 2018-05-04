/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Vilka.CompareUI.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Vilka.CompareUI.Model;

namespace Vilka.CompareUI.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// <para>
	/// See http://www.mvvmlight.net
	/// </para>
	/// </summary>
	public class ViewModelLocator
	{
		static ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
			}
			else
			{
				SimpleIoc.Default.Register<IDataService, DataService>();
			}
			
			SimpleIoc.Default.Register<RegionCompareViewModel>();
			SimpleIoc.Default.Register<LeagueCompareViewModel>();
		}
		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
			"CA1822:MarkMembersAsStatic",
			Justification = "This non-static member is needed for data binding purposes.")]
		public RegionCompareViewModel RegionCompare
		{
			get
			{
				return ServiceLocator.Current.GetInstance<RegionCompareViewModel>();
			}
		}

        public LeagueCompareViewModel LeagueCompare
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LeagueCompareViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
		{
		}
	}
}