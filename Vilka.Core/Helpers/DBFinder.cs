using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.Compare;
using Vilka.DB;

namespace Vilka.Core.Helpers
{
	public static class DBFinder
	{
		public static Event FindEvent(Event ev)
		{
			using(VilkaEntities context  = new VilkaEntities())
			{
				foreach(Event item in context.Events)
				{
					if (!item.IsLive())
					{
						if (Comparator.CompareEvents(item, ev))
							return item;
					}
				}
				return null;
			}
		}


	}
}
