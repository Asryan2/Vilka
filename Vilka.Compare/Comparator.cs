using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;

namespace Vilka.Compare
{
    public static class Comparator
    {

        public static bool CompareEvents(Event one, Event two)
        {
            return false;
        }

        public static bool CompareRegions(Region one, Region two)
        {
            return new RegionComperator().Compare(one, two);
        }

        public static bool CompareRegions(string one, string two)
        {
            return new RegionComperator().Compare(new Region { Name = one }, new Region { Name = two });
        }
    }
}
