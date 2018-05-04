using System;
using System.Linq;
using Vilka.Core;
using Vilka.DB;

namespace Vilka.CompareUI.Model
{
    public class DataService : IDataService
    {
       
        public void ComperatorGetNextLeague(Action<string, string, string, string, Action<bool>> callback)
        {
            using (VilkaEntities context = new VilkaEntities())
            {
                if (context.CompareLeagues.Count() == 0) callback("Nothing Found", "Nothing Found", "Nothing Found", "Nothing Found", (a) => { });
                CompareLeague l = context.CompareLeagues.First();
                callback(l.First, l.Second, l.Region.Name, l.Sport.Name, (a) => AnswerLeagueCompareRequest(l, a));
            }
            
        }

        private void AnswerLeagueCompareRequest(CompareLeague l, bool answer)
        {
            using (VilkaEntities context = new VilkaEntities())
            {
                if (answer == true)
                {
                    League league = context.Leagues.Where((i) => i.Name == l.First || i.Name == l.Second).FirstOrDefault();
                    if (league == null)
                    {
                        League newLeague = new League();
                        newLeague.Name = l.First;
                        newLeague.SportID = l.SportID;
                        newLeague.RegionID = l.RegionID;
                        league = context.Leagues.Add(newLeague);
                        context.SaveChanges();

                    }
                    Tuple<string, Region, Sport> keyFirst = new Tuple<string, Region, Sport>(l.First, l.Region, l.Sport);
                    Tuple<string, Region, Sport> keySecond = new Tuple<string, Region, Sport>(l.Second, l.Region, l.Sport);
                    if (!Dicts.LeagueDict.ContainsKey(keyFirst))
                        Dicts.LeagueDict.Add(keyFirst, league);
                    if (!Dicts.LeagueDict.ContainsKey(keySecond))
                        Dicts.LeagueDict.Add(keySecond, league);

                }
                context.CompareLeagues.Remove(context.CompareLeagues.Where((i) => i.ID == l.ID).FirstOrDefault());
                context.SaveChanges();
            }
        }
    }
}