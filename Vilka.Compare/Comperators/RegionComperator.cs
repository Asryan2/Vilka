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

namespace Vilka.Compare
{
    class RegionComperator : IComparator<Region, RegionsDictionaryOptimizationData>
    {
        //private string _wikiAPIUrl = "https://en.wikipedia.org/w/api.php?action=query&generator=search&prop=info&inprop=url&format=json&gsrsearch=";
        private string _wikiAPIUrl = "https://en.wikipedia.org/w/api.php?action=query&list=search&format=json&srsearch=";
        
        private int GetRegionWikiPageID(Region region)
        {
            List<int> res = new List<int>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_wikiAPIUrl + region.Name);
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



        public bool Compare(Region one, Region two, ref RegionsDictionaryOptimizationData oneOptimization, ref RegionsDictionaryOptimizationData twoOptimization)
        {
            int firstID;
            int secondID;
            if (oneOptimization.WikiPageID.HasValue)
                firstID = oneOptimization.WikiPageID.Value;
            else
            {
                firstID = GetRegionWikiPageID(one);
                oneOptimization.WikiPageID = firstID;
            }
            if (twoOptimization.WikiPageID.HasValue)
                secondID = twoOptimization.WikiPageID.Value;
            else
            {
                secondID = GetRegionWikiPageID(two);
                twoOptimization.WikiPageID = secondID;
            }
            
            return firstID == secondID && firstID != 0;
        }
    }
}
