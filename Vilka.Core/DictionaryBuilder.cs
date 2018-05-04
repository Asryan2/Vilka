using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.Compare;
using Vilka.DB;

namespace Vilka.Core
{
	public static class DictionaryBuilder
	{
        private static VilkaEntities context = new VilkaEntities();


        public static void Build()
		{
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + nameof(Compare_Events) + "]");
            //context.SaveChanges();

            //FillCompareTable().Wait();

            //BuildRegionDictionary();
            BuildLeagueDictionary();

        }

        private static void BuildLeagueDictionary()
        { 
            ObjectResult<CEventsGetLeagues_Result> leagues = context.CEventsGetLeagues();
            foreach(CEventsGetLeagues_Result element in leagues.ToList())
            {
                if (!Dicts.RegionDict.ContainsKey(element.Region)) EnsureRegionExists(element.Region);

                Sport sport = context.Sports.Where((s) => s.ID == element.SportID).FirstOrDefault();
                if (sport == null || Dicts.LeagueDict.ContainsKey(new Tuple<string, Region, Sport>(element.League, Dicts.RegionDict[element.Region], sport))){
                    continue;
                }

                bool found = false;
                foreach (League item in context.Leagues.ToList())
                {
                    //item.Region = context.Regions.Where(r => r.ID == item.RegionID).FirstOrDefault();

                    if (item.RegionID == Dicts.RegionDict[element.Region].ID && item.SportID == sport.ID)
                    {
                        LeagueComparisonQueue.Enqueue(new CompareLeague() { RegionID = Dicts.RegionDict[element.Region].ID, First = element.League, Second = item.Name, SportID = sport.ID });
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    League newLeague = new League();
                    newLeague.Name = element.League;
                    newLeague.SportID = sport.ID;
                    newLeague.RegionID = Dicts.RegionDict[element.Region].ID;
                    context.Leagues.Add(newLeague);
                    context.SaveChanges();
                    Dicts.LeagueDict.Add(new Tuple<string,Region, Sport>(element.League, Dicts.RegionDict[element.Region], sport), newLeague);
                }

            }
        }

        private static void EnsureRegionExists(string region)
        {

            if (Dicts.RegionDict.ContainsKey(region))
                return;
            bool found = false;
            foreach (Region item in context.Regions.ToList())
            {
                RegionsDictionaryOptimizationData oneOptimization = item.RegionsDictionaryOptimizationDatas.FirstOrDefault() ?? new RegionsDictionaryOptimizationData() { RegionID = item.ID };
                RegionsDictionaryOptimizationData twoOptimization = new RegionsDictionaryOptimizationData();

                bool equal = Comparator.CompareRegions(item.Name, region, ref oneOptimization, ref twoOptimization);
                
                if (oneOptimization.WikiPageID.HasValue)
                {
                    item.RegionsDictionaryOptimizationDatas.Add(oneOptimization);
                    context.SaveChanges();
                }
                if (equal)
                {
                    Dicts.RegionDict.Add(region, item);
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Region newReg = new Region();
                newReg.Name = region;
                context.Regions.Add(newReg);
                context.SaveChanges();
                Dicts.RegionDict.Add(region, newReg);
            }

        }

        private static void BuildRegionDictionary()
        {

            ObjectResult<string> regions = context.CEventsGetRegions();
            foreach (string region in regions.ToList())
            {
                EnsureRegionExists(region);
            }
        }

        private static void TestRegionComparer()
        {
            //RegionDictionary regionDict = RegionDictionary.GetInstance();

            //List<string> regions = context.CEventsGetRegions().ToList();
            //for (int i = 0; i < regions.Count; i++)
            //{
            //    for (int j = i + 1; j < regions.Count; j++)
            //    {
            //        if (Comparator.CompareRegions(regions[i], regions[j]))
            //        {
            //            Console.WriteLine(regions[i] + " - " + regions[j]);
            //        }
            //    }
            //}
        }

        public async static Task FillCompareTable()
		{
            List<Task> tasks = new List<Task>();
			Betters.All.ForEach(b =>
			{
				tasks.Add(b.FillCompareTableAsync());
			});
            await Task.WhenAll(tasks);


        }
    }
}
