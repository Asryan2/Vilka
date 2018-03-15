using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VilkaConsole
{
	interface IBetter
	{
		List<Event> GetAllEvents();
		List<BetOffer> GetEventBetOffers(int eventID);
	}
}
