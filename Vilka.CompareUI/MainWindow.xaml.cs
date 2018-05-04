using System.Windows;
using Vilka.CompareUI.ViewModel;
using Vilka.Core;

namespace Vilka.CompareUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the MainWindow class.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
            VilkaApplicaiton.Run();
			Closing += (s, e) => ViewModelLocator.Cleanup();
		}
	}
}