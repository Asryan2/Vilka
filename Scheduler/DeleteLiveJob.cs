using Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;
using Vilka.Helpers;

namespace Scheduler
{
	class DeleteLiveJob : IJob
	{
		public void Run()
		{
			using (VilkaEntities context = new VilkaEntities())
			{
				IQueryable<EventSiteData> liveEventSiteDatas = context.EventSiteDatas.Where((d) => d.Event.PrematchEnd <= DateTime.Now);
				Console.WriteLine(liveEventSiteDatas.Count() + "Event Datas found.");
				context.EventSiteDatas.RemoveRange(liveEventSiteDatas);
				
				IQueryable<Outcome> liveOutcomes = context.Outcomes.Where((d) => d.BetOffer.Event.PrematchEnd <= DateTime.Now);
				Console.WriteLine(liveOutcomes.Count() + "Outcomes found.");
				context.Outcomes.RemoveRange(liveOutcomes);

				IQueryable<BetOffer> liveBetOffers = context.BetOffers.Where((d) => d.Event.PrematchEnd <= DateTime.Now);
				Console.WriteLine(liveBetOffers.Count() + "Bet Offers found.");
				context.BetOffers.RemoveRange(liveBetOffers);

				IQueryable<Event> liveEvents = context.Events.Where((d) => d.PrematchEnd <= DateTime.Now);
				Console.WriteLine(liveEvents.Count() + "Events found.");
				context.Events.RemoveRange(liveEvents);

				Console.WriteLine("Removing live Events...");
				Console.WriteLine(context.SaveChanges() + " rows affected.");
				Console.WriteLine();
			}
		}
	}
}
