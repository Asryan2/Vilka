using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VilkaConsole.Helpers
{
	public static class DBExtensions
	{
		public static bool IsLive(this Event ev)
		{
			return ev.PrematchEnd <= DateTime.Now;
		}

		public static Sport DBCopy(this Sport sport, VilkaEntities context)
		{
			if (sport == null) return null;
			return context.Sports.Where((s) => s.ID == sport.ID).FirstOrDefault();
		}

		public static BetType DBCopy(this BetType betType, VilkaEntities context)
		{
			if (betType == null) return null;

			return context.BetTypes.Where((b) => b.ID == betType.ID).FirstOrDefault();
		}
		public static BetTarget DBCopy(this BetTarget betTarget, VilkaEntities context)
		{
			if (betTarget == null) return null;
			return context.BetTargets.Where((b) => b.ID == betTarget.ID).FirstOrDefault();
		}
		public static Event DBCopy(this Event ev, VilkaEntities context)
		{
			if (ev == null) return null;

			return context.Events.Where((e) => e.ID == ev.ID).FirstOrDefault();
		}

		public static OutcomeType DBCopy(this OutcomeType ot, VilkaEntities context)
		{
			if (ot == null) return null;

			return context.OutcomeTypes.Where((o) => o.ID == ot.ID).FirstOrDefault();
		}
	}
}
