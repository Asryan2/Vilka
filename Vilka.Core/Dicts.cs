using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;

namespace Vilka.Core
{
    public static class Dicts
    {
        public static LeagueDictionary LeagueDict
        {
            get
            {
                return LeagueDictionary.GetInstance();
            }
        }

        public static RegionDictionary RegionDict
        {
            get
            {
                return RegionDictionary.GetInstance();
            }
        }
    }
}
