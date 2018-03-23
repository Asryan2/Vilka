using System;
using Vilka.CompareUI.Model;

namespace Vilka.CompareUI.Design
{
	public class DesignDataService : IDataService
	{
		public void GetData(Action<DataItem, Exception> callback)
		{
			// Use this to create design time data

			var item = new DataItem("Welcome to MVVM Light [design]");
			callback(item, null);
		}
	}
}