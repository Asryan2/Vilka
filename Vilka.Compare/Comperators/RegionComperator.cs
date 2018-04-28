using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilka.DB;
using Vilka.Infrastructure.Interfaces;

namespace Vilka.Compare
{
    class RegionComperator : IComparator<Region>
    {
        public bool Compare(Region one, Region two)
        {
            return false;
        }
    }
}
