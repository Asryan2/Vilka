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
				context.EventSiteDatas.RemoveRange(context.EventSiteDatas.Where((d) => d.Event.PrematchEnd <= DateTime.Now));
				context.Outcomes.RemoveRange(context.Outcomes.Where((d) => d.BetOffer.Event.PrematchEnd <= DateTime.Now));
				context.BetOffers.RemoveRange(context.BetOffers.Where((d) => d.Event.PrematchEnd <= DateTime.Now));
				context.Events.RemoveRange(context.Events.Where((d) => d.PrematchEnd <= DateTime.Now));
				Console.WriteLine(context.SaveChanges() + " rows affected.");
			}
		}
	}
}
