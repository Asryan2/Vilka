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


        public async static Task Build()
		{
            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE [" + nameof(Compare_Events) + "]");
            //context.SaveChanges();

            //FillCompareTable().Wait();

            TestRegionComparer();
            
		}

        private static void BuildRegionDictionary()
        {
            RegionDictionary regionDict = RegionDictionary.GetInstance();

            ObjectResult<string> regions = context.CEventsGetRegions();
            foreach (string region in regions)
            {
                if (regionDict.ContainsKey(region))
                    continue;
                foreach (Region item in context.Regions)
                {
                    if (Comparator.CompareRegions(item, new Region() { Name = region}))//compare item and region
                    {
                        regionDict.Add(region, item);
                    }
                }
            }
        }

        private static void TestRegionComparer()
        {
            RegionDictionary regionDict = RegionDictionary.GetInstance();

            List<string> regions = context.CEventsGetRegions().ToList();
            for(int i = 0; i < regions.Count; i++)
            {
                for (int j = i+1; j < regions.Count; j++)
                {
                    if(Comparator.CompareRegions(regions[i], regions[j]))
                    {
                        Console.WriteLine(regions[i] + " - " + regions[j]);
                    }
                }
            }
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
