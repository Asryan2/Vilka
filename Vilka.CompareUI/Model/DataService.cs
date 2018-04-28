using System;
using System.Linq;
using Vilka.DB;

namespace Vilka.CompareUI.Model
{
	public class DataService : IDataService
	{

		public void ComperatorGetNextRegion(Action<string, string> callback)
		{
			// Use this to connect to the actual data service
			using (VilkaEntities context = new VilkaEntities())
			{
				var a = context.Compare_Events.GroupBy(e => new { e.SiteID, e.Region }).ToList();

			}
			callback("1233", "Success!");
		}
	}
}