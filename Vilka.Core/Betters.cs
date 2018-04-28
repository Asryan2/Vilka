using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.Infrastructure;

namespace Vilka.Core
{
	public static class Betters
	{
		public static List<IBetter> All = new List<IBetter>();

		public static void Add(IBetter better)
		{
			All.Add(better);
		}

		public static void Remove(IBetter better)
		{
			All.Remove(better);
		}
	}
}
