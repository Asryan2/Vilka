using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;
using Vilka.Infrastructure.Interfaces;

namespace Vilka.Compare.Comperators
{
    class LeagueComperator : IComparator<League, LeagueDictionaryOptimizationData>
    {

        private string _wikiAPIUrl = "https://en.wikipedia.org/w/api.php?action=query&list=search&format=json&srsearch=";

        private void PushCompareRequest(League one, League two)
        {
            if (one.SportID != two.SportID) return;
            if (one.Region.ID != two.Region.ID) return;
            //LeagueComparisonQueue.Add(new CompareLeague() { First = one.Name, Second = two.Name, RegionID = one.RegionID, SportID = one.SportID });

        }

        public bool Compare(League one, League two, ref LeagueDictionaryOptimizationData oneOptimization, ref LeagueDictionaryOptimizationData twoOptimization)
        {
            if (one.SportID != two.SportID) return false;
            if (one.Region.ID != two.Region.ID) return false;
            int firstID;
            int secondID;
            if (oneOptimization.WikiPageID.HasValue)
                firstID = oneOptimization.WikiPageID.Value;
            else
            {
                firstID = GetLeagueWikiPageID(one);
                oneOptimization.WikiPageID = firstID;
            }
            if (twoOptimization.WikiPageID.HasValue)
                secondID = twoOptimization.WikiPageID.Value;
            else
            {
                secondID = GetLeagueWikiPageID(two);
                twoOptimization.WikiPageID = secondID;
            }

            return firstID == secondID && firstID != 0;
        }

        private int GetLeagueWikiPageID(League league)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_wikiAPIUrl + league.Name + " " + league.Region.Name);
            request.Method = "GET";
            string json;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                json = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            int added = 0;
            JObject obj;
            try
            {
                obj = JObject.Parse(json);
                var query = obj["query"];
                var search = query["search"][0];
                return search["pageid"].ToObject<int>();
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
    }
}
