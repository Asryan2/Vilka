using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.Compare.Comperators;
using Vilka.DB;

namespace Vilka.Compare
{
    public static class Comparator
    {

        public static bool CompareEvents(Event one, Event two)
        {
            return false;
        }

        public static bool CompareRegions(Region one, Region two, ref RegionsDictionaryOptimizationData oneOptimization, ref RegionsDictionaryOptimizationData twoOptimization)
        {
            return new RegionComperator().Compare(one, two, ref oneOptimization, ref twoOptimization);
        }

        public static bool CompareRegions(string one, string two, ref RegionsDictionaryOptimizationData oneOptimization, ref RegionsDictionaryOptimizationData twoOptimization)
        {
            return new RegionComperator().Compare(new Region { Name = one }, new Region { Name = two }, ref oneOptimization, ref twoOptimization);
        }

        public static bool CompareLeagues(League one, League two, ref LeagueDictionaryOptimizationData oneOptimization, ref LeagueDictionaryOptimizationData twoOptimization)
        {
            return new LeagueComperator().Compare(one, two, ref oneOptimization, ref twoOptimization);
        }
    }
}
