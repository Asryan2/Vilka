using System;
using System.Collections.Generic;
using System.Linq;
using Vilka.DB;

namespace VilkaConsole.Mappings
{
	public abstract class BetterMappings
	{
		public BetterMappings()
		{
			EnsureID();

			EnsureSports();
			EnsureBetTypes();
			EnsureBetTargets();
			EnsureOutcomeTypes();
		}

		public Dictionary<string, Sport> SportMappings { get; } = new Dictionary<string, Sport>();
		public Dictionary<string, BetType> BetTypeMappings { get; } = new Dictionary<string, BetType>();
		public Dictionary<string, BetTarget> BetTargetMappings { get; } = new Dictionary<string, BetTarget>();
		public Dictionary<string, OutcomeType> OutcomeTypeMappings { get; } = new Dictionary<string, OutcomeType>();
		public abstract string Name { get; }
		public int ID { get; private set; }

		public void EnsureSports()
		{
			if (ID == 0) throw new Exception("ID should be initialized before SportMappings.");
			using (VilkaEntities context = new VilkaEntities())
			{
				List<SportMapping> mappings = context.SportMappings.Where((mapping) => mapping.SiteID == ID).ToList();
				foreach (SportMapping mapping in mappings)
				{
					SportMappings.Add(mapping.Name, mapping.Sport);
				}
			}

		}
		public void EnsureOutcomeTypes()
		{
			if (ID == 0) throw new Exception("Better ID should be initialized before OutcomeTypes.");
			using (VilkaEntities context = new VilkaEntities())
			{
				List<OutcomeTypeMapping> mappings = context.OutcomeTypeMappings.Where((mapping) => mapping.SiteID == ID).ToList();
				foreach (OutcomeTypeMapping mapping in mappings)
				{
					OutcomeTypeMappings.Add(mapping.Name, mapping.OutcomeType);
				}
			}

		}

		public void EnsureBetTypes()
		{
			if (ID == 0) throw new Exception("Better ID should be initialized before BetTypes.");
			using (VilkaEntities context = new VilkaEntities())
			{
				List<BetTypeMapping> mappings = context.BetTypeMappings.Where((mapping) => mapping.SiteID == ID).ToList();
				foreach (BetTypeMapping mapping in mappings)
				{
					BetTypeMappings.Add(mapping.Name, mapping.BetType);
				}
			}

		}
		public void EnsureBetTargets()
		{
			if (ID == 0) throw new Exception("Better ID should be initialized before BetTargets.");
			using (VilkaEntities context = new VilkaEntities())
			{
				List<BetTargetMapping> mappings = context.BetTargetMappings.Where((mapping) => mapping.SiteID == ID).ToList();
				foreach (BetTargetMapping mapping in mappings)
				{
					BetTargetMappings.Add(mapping.Name, mapping.BetTarget);
				}
			}

		}

		public void EnsureID()
		{
			using (VilkaEntities context = new VilkaEntities())
			{
				Site better = context.Sites.Where((site) => site.Name == Name).FirstOrDefault();
				if (better == null)
				{
					better = new Site();
					better.Name = Name;
					context.Sites.Add(better);
					context.SaveChanges();
				}
				ID = better.ID;
			}
		}
	}
}