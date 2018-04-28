using System;
using Vilka.CompareUI.Model;

namespace Vilka.CompareUI.Design
{
	public class DesignDataService : IDataService
	{
		public void ComperatorGetNextRegion(Action<string, string> callback)
		{
			callback("Design", "Design");
		}
	}
}