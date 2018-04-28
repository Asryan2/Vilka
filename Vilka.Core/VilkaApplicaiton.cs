using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.Core
{
	public static class VilkaApplicaiton
	{
		public static void Run()
		{
			Betters.Add(new Better888());
			Betters.Add(new BetterVivaro());

		}
	}
}
