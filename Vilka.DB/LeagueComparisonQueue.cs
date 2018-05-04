using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilka.DB
{
    public static class LeagueComparisonQueue
    {
        public static void Enqueue(CompareLeague item)
        {
            using(VilkaEntities context = new VilkaEntities())
            {
                context.CompareLeagues.Add(item);
                context.SaveChanges();
            }
        }
    }
}
